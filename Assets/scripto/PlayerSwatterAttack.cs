using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwatterAttack : MonoBehaviour 
{
    
    public Transform pontoAtaque;
    public Text energia;

    private float raioAtaque;
    public Animator anim;
    
    private int carga;
    private bool atacando;
    
    public LayerMask enemy;
    
    // Start is called before the first frame update
    void Start() {
        raioAtaque = 0.5f;
        carga = 100;
        atacando = false;
        energia.text = "Carga: " + carga;
        energia.gameObject.SetActive(false);
        pontoAtaque.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Raquete")) {
            Destroy(col.gameObject);
            energia.gameObject.SetActive(true);
            anim.SetBool("WithSwatter",true);
            pontoAtaque.gameObject.SetActive(true);
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoAtaque.position, raioAtaque);
    }
    
    // Update is called once per frame
    void Update() {
        if (anim.GetBool("WithSwatter") == true) {
            
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var v2 = new Vector2(horizontal, vertical).normalized;
            
            if (v2 != Vector2.zero) {
                if (v2.x != 0 & v2.y != 0) {
                    pontoAtaque.localPosition = new Vector3(v2.x, 0, 0);
                }else {
                    pontoAtaque.localPosition = v2;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space) & atacando==false & carga > 0) {
                Ataque();            
            }
        }
    }

    private void Ataque() {
        anim.SetTrigger("Attack");
        atacando = true;
        
        Collider2D[] inimigos = Physics2D.OverlapCircleAll(pontoAtaque.position,raioAtaque,enemy);
        
        foreach (Collider2D inimigo in inimigos) {
            if (inimigo != null) {
                Destroy(inimigo.gameObject);
                carga -= 10;
            }
        }

        energia.text = "Carga: " + carga;
    }

    public  bool EstaAtacando() {
        return this.atacando;
    }
    
    public void FimAtaque() {
        this.atacando = false;
    }
    
    public void Recarregar(){
       	if(carga >= 0 & carga <= 90) {
            carga += 10;
            energia.text = "Carga: " + carga;
        }
    }
    
}
