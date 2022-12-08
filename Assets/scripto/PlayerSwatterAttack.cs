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
    public AudioClip eletric_spawn;
    private AudioSource sound;
    
    void Start() {
        raioAtaque = 0.5f;
        atacando = false;
        energia.gameObject.SetActive(false);
        pontoAtaque.gameObject.SetActive(false);
        sound = GetComponent<AudioSource>();
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoAtaque.position, raioAtaque);
    }
    
    void Update() {
        if (anim.GetBool("WithSwatter")) {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var v2 = new Vector2(horizontal, vertical).normalized;
			
			if (carga > 110) {
				carga = 110;
			}
            
            if (v2 != Vector2.zero) {
                if (v2.x != 0 & v2.y != 0) {
                    pontoAtaque.localPosition = new Vector3(v2.x, 0, 0);
                }else {
                    pontoAtaque.localPosition = v2;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space) & atacando==false & carga > 10) {
                Ataque();            
            }
        }
    }

    private void Ataque() {
        sound.PlayOneShot(eletric_spawn);
        anim.SetTrigger("Attack");
        atacando = true;
        Collider2D[] inimigos = Physics2D.OverlapCircleAll(pontoAtaque.position,raioAtaque,enemy);
        
        foreach (Collider2D inimigo in inimigos) {
            if (inimigo != null) {
                inimigo.GetComponent<Enemy>().Morte();
                if (carga > 10) {
                    carga -= 10;
                }
            }
        }
        
        energia.text = "Carga: " + (carga - 10);
    }
    
    public  bool EstaAtacando() {
        return atacando;
    }
    
    public void FimAtaque() {
        atacando = false;
    }
    
    public void Recarregar(){
       	if(carga >= 0 & carga <= 100) {
            carga += 110;
            energia.text = "Carga: " + (carga - 10);
        }
    }
    
    public void AtivarRaquete(){
        carga = 110;
        energia.text = "Carga: " + (carga - 10);
        energia.gameObject.SetActive(true);
        anim.SetBool("WithSwatter",true);
        pontoAtaque.gameObject.SetActive(true);
    }
    
    public void Desativar(){
        energia.gameObject.SetActive(false);
        anim.SetBool("WithSwatter", false);
        pontoAtaque.gameObject.SetActive(false);
    }
    
}
