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
    
    private Text resultado;
    private bool jogando;
    private int quant;
    private GameObject aux, aviso;
    
    private Text restantes;
    public PlayerSwatterAttack swatterattack;
    //private SpriteRenderer sRenderer;
    //public Sprite[] sprites;
    
    private void Start() {
        jogando = true;
        quant = GameObject.FindGameObjectsWithTag("Emissor").Length;
        speed = 10f;
        rb2d = GetComponent<Rigidbody2D>();
        
        resultado = GameObject.Find("tempo").GetComponent<Text>();
        aviso = GameObject.Find("keyboards_24");
        aviso.SetActive(false);

        restantes = GameObject.Find("restantes").GetComponent<Text>();
        restantes.text = String.Format("Emissores: {0:0}", quant);
    }
    
    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Emissor")) {
            aux = col.gameObject;
            aviso.SetActive(true);    
        }
        
        if (col.gameObject.CompareTag("Carregador")) {
             aux = col.gameObject;
             aviso.SetActive(true);    
        }
    }
    
    private void OnCollisionExit2D() {
        aux = null;
        aviso.SetActive(false);
    }
    
    void Update() {
        if (jogando == true) {
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

                rb2d.MovePosition(rb2d.position + movement * (speed * Time.fixedDeltaTime));
            } 
            
            if (Input.GetKey(KeyCode.X) & aviso.activeSelf == true) {
                if(aux.CompareTag("Emissor")){
                    changeSprite();
                }else if(aux.CompareTag("Carregador")){
                    swatterattack.Recarregar();
                }  
            } 
        }
    }
    
    private void changeSprite(){
        //sRenderer = aux.GetComponent<SpriteRenderer>();
        //aux.tag = "Limpo"
        
        Destroy(aux);
        quant -= 1;
        restantes.text = String.Format("Emissores: {0:0}", quant);

        if (quant == 0) {
            resultado.text = "Parabéns, Você Venceu";
            Time.timeScale = 0f;
            jogando = false;
        }
    }
    
}
