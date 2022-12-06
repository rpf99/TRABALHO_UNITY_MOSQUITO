using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    public Animator anim;
    
    private Text resultado, restantes;
    private bool jogando, protegido;
    private int quant;
    private GameObject aux, aviso;
    public PlayerSwatterAttack swatterattack;
    private HealthController hc;
    
    private AudioSource sounds;
    public AudioClip []lt;

    private GameObject backgroundSong;
    
    private void Start() {
        jogando = true;
        quant = GameObject.FindGameObjectsWithTag("Emissor").Length;
        speed = 15f;
        rb2d = GetComponent<Rigidbody2D>();
        
        resultado = GameObject.Find("tempo").GetComponent<Text>();
        aviso = GameObject.Find("keyboards_24");
        aviso.SetActive(false);

        restantes = GameObject.Find("restantes").GetComponent<Text>();
        restantes.text = String.Format("Emissores: {0:0}", quant);

        hc = GameObject.Find("barra_de_vida").GetComponent<HealthController>();
        sounds = GetComponent<AudioSource>();
        sounds.Stop();

        backgroundSong = GameObject.Find("BackgroundSong");
    }
    
    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Emissor") || col.gameObject.CompareTag("Carregador")) {
            aux = col.gameObject;
            aviso.SetActive(true);    
        }
    }
    
    private void OnCollisionExit2D() {
        aux = null;
        aviso.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Raquete") && anim.GetBool("WithSwatter") == false) {
            sounds.PlayOneShot(lt[0]);
            Destroy(col.gameObject);
            swatterattack.AtivarRaquete();
            
        }else if(col.gameObject.CompareTag("Camisa")){
            sounds.PlayOneShot(lt[0]);
            Destroy(col.gameObject);
            anim.SetBool("WithShirt",true);
            protegido = true;
        }
    }
    
    void Update() {
        if (jogando) {
            if (swatterattack.EstaAtacando() == false) {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
                anim.SetFloat("Velocidade", movement.sqrMagnitude);
                
                if (movement != Vector2.zero) {
                    anim.SetFloat("HorizontalIdle", movement.x);
                    anim.SetFloat("VerticalIdle", movement.y);
                }

                if (movement.sqrMagnitude > 0) {
                    if (!sounds.isPlaying) {
                        sounds.Play();
                    }
                }else {
                    sounds.Stop();
                }
                
                rb2d.MovePosition(rb2d.position + movement * (speed * Time.fixedDeltaTime));
            } 
            
            if (Input.GetKey(KeyCode.X) & aviso.activeSelf & movement.sqrMagnitude <= 0) {
                if(aux.CompareTag("Emissor")){
                    changeSprite();
                }else if(aux.CompareTag("Carregador") & anim.GetBool("WithSwatter")){
                    swatterattack.Recarregar();
                }  
            } 
        }
    }
    
    private void changeSprite(){
        sounds.PlayOneShot(lt[4]);
        aux.GetComponent<Spawner>().TrocarSprite();
        quant -= 1;
        restantes.text = String.Format("Emissores: {0:0}", quant);
        
        if (quant == 0) {
            backgroundSong.GetComponent<AudioSource>().Stop();
            sounds.Stop();
            sounds.PlayOneShot(lt[1]);
            resultado.text = "Parabéns, Você Venceu";
            Time.timeScale = 0f;
            jogando = false;
        }
    }
    
    public void ReceberDano(){
        if(anim.GetBool("WithSwatter")){
            swatterattack.Desativar();
        }

        if(protegido){
            anim.SetBool("WithShirt", false);
            protegido = false;
        }else{
            sounds.PlayOneShot(lt[5]);
            speed -= 5; 
            hc.Damage();
            
            if(hc.health == 0){
                jogando = false;
                backgroundSong.GetComponent<AudioSource>().Stop();
                sounds.Stop();
                sounds.PlayOneShot(lt[2]);
                resultado.text = "Game Over";
                Time.timeScale = 0f;
            }
        }
    }

    public bool Perdeu() {
        return jogando == false;
    }
    
}
