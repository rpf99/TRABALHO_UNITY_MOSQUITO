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
    private Animator anim;
    
    public Text resultado;
    private bool permitido;
    private int quant;
    private GameObject emissor;
    private GameObject aviso;
    
    //private SpriteRenderer sRenderer;
    //private Sprite[] sprites;
    
    private void Start() {
        permitido = false;
        quant = 1;
        speed = 10f;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        aviso = GameObject.Find("keyboards_24");
        aviso.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Emissor")) {
            this.permitido = true;
            emissor = col.gameObject;
            aviso.SetActive(true);
        }
    }
    
    private void OnCollisionExit2D(){
        this.permitido = false;
        aviso.SetActive(false);
    }
    
    void Update() {
        if (Time.timeScale == 1f) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Velocidade", movement.sqrMagnitude);

            if (movement != Vector2.zero) {
                anim.SetFloat("HorizontalIdle", movement.x);
                anim.SetFloat("VerticalIdle", movement.y);
            }
                   
            if (Input.GetKey(KeyCode.X) & permitido == true) {
                this.quant -= 1;
                Destroy(emissor);
                //changeSprite();
                if (quant == 0) {
                    resultado.text = "Parabéns, Você Venceu";
                    Time.timeScale = 0f;
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
    
    private void FixedUpdate() {
        rb2d.MovePosition(rb2d.position + movement * (speed * Time.fixedDeltaTime));
    }
    
}
