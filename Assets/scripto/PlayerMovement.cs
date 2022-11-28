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
    private bool jogando;
    private int quant;
    private GameObject aux, aviso;

    public PlayerSwatterAttack swatterattack;
    //private SpriteRenderer sRenderer;
    //public Sprite[] sprites;
    
    //OBS: inicalmente a quantidade de emissores é 1, se limpar 1 já ganha o jogo
    
    private void Start() {
        jogando = true;
        quant = GameObject.FindGameObjectsWithTag("Emissor").Length;
        speed = 10f;
        rb2d = GetComponent<Rigidbody2D>();
        
        resultado = GameObject.Find("tempo").GetComponent<Text>();
        restantes = GameObject.Find("restantes").GetComponent<Text>();
        restantes.text = String.Format("Emissores: {0:0}", quant);

        aviso = GameObject.Find("keyboards_24");
        aviso.SetActive(false);
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
                
                if (aux.CompareTag("Emissor")) {
                    this.quant -= 1;
                    restantes.text = String.Format("Emissores: {0:0}", quant);
                    Destroy(aux);
                    //changeSprite();
                    if (quant == 0) {
                        resultado.text = "Parabéns, Você Venceu";
                        Time.timeScale = 0f;
                        jogando = false;
                    }
                }else if (aux.CompareTag("Carregador")){
                    swatterattack.Recarregar();
                }
            } 
        }
    }
    
    /*
    private void changeSprite(){
        sRenderer = emissor.GetComponent<SpriteRenderer>();
        //emissor.tag = "Limpo"
        if(sRenderer.sprite == sprites[]) {
            sRenderer.sprite = sprites[]
        }
    }*/
    
}
