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
    
    private Text resultado, restantes, defesa;
    private bool jogando, protegido;
    private int quant, cont_escudo;
    private GameObject aux, aviso;
    public PlayerSwatterAttack swatterattack;
    private HealthController hc;
    
    private AudioSource sounds;
    public AudioClip []lt;
    private GameObject backgroundSong;

    public Camera MiniMap;
    public RawImage MinMapImage;
    
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

        cont_escudo = 5;
        defesa = GameObject.Find("defesa").GetComponent<Text>();
        defesa.gameObject.SetActive(false);
        backgroundSong = GameObject.Find("BackgroundSong");
        
        MinMapImage.gameObject.SetActive(false);
        MiniMap.gameObject.SetActive(false);
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
            cont_escudo = 5;
            sounds.PlayOneShot(lt[0]);
            Destroy(col.gameObject);
            anim.SetBool("WithShirt",true);
            protegido = true;
            defesa.text = "Defesa: " + cont_escudo;
            defesa.gameObject.SetActive(true);
        }
    }
    
    void Update() {
        if (jogando) {
            if (hc.health == 5 || quant <= 4) {
                AtivarMiniMapa();
            }
            
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
        aux.GetComponent<Spawner>().TrocarSprite();
        quant -= 1;
        restantes.text = "Emissores: " + quant;
        
        if (quant == 0) {
            backgroundSong.GetComponent<AudioSource>().Stop();
            sounds.Stop();
            sounds.PlayOneShot(lt[1]);
            resultado.text = "Parabéns, Você Venceu";
            Time.timeScale = 0f;
            jogando = false;
        }
    }
    
    public void ReceberDano() {
        sounds.PlayOneShot(lt[5]);
        
        if(anim.GetBool("WithSwatter")) {
            swatterattack.Desativar();
        }
        
        if(protegido){
            cont_escudo -= 1;
            defesa.text = "Defesa: " + cont_escudo;

            if (cont_escudo == 0) {
                anim.SetBool("WithShirt", false);
                protegido = false;
                cont_escudo = 0;
                defesa.gameObject.SetActive(false);
            }
     
        }else{
            speed -= 5; 
            hc.Damage();
            if(hc.health == 0) {
                GAMEOVER();
            }
        }
    }

    private void Encerramento() {
        resultado.text = "Game Over";
        sounds.Stop();
        sounds.PlayOneShot(lt[2]);
        Time.timeScale = 0f;
    }

    public void GAMEOVER() {
        DesativarMiniMapa();
        jogando = false;
        backgroundSong.GetComponent<AudioSource>().Stop();
        anim.SetTrigger("Death");
        Invoke("Encerramento",1f);
    }

    public bool Perdeu() {
        return jogando == false;
    }
    
    public void AtivarMiniMapa(){
        MinMapImage.gameObject.SetActive(true);
        MiniMap.gameObject.SetActive(true);
    }

    public void DesativarMiniMapa() {
        MinMapImage.gameObject.SetActive(false);
        MiniMap.gameObject.SetActive(false);
    }
    
}
