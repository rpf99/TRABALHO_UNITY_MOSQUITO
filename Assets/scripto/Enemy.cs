using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;
    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;
    
    private PlayerMovement pl;
    private AudioSource Asource;
    private float intervalo_ataque;
    private bool ativo;
    
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        pl = target.GetComponent<PlayerMovement>();
        Asource = GetComponent<AudioSource>();
        Asource.volume = 8f;
        Asource.Stop();
        intervalo_ataque = 1f;
        ativo = true;
    }
    
    private void Update() {
        if (!Asource.isPlaying && !pl.Perdeu()) {
            Asource.Play();
        }
        
        animator.SetBool("isFlying", isInChaseRange);
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);
        
        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;
        
        if(shouldRotate) {
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
        }
    }
    
    private void FixedUpdate() {
        if (pl.Perdeu()) {
            Asource.Stop();
        }else {
            if (isInChaseRange && !isInAttackRange) {
                MoveCharacter(movement);
            }
            
            if (isInAttackRange) {
                rb.velocity = Vector2.zero;
                intervalo_ataque -= Time.deltaTime;
                if (intervalo_ataque <= 0) {
                    Asource.Stop();
                    pl.ReceberDano();
                    intervalo_ataque = 1f;
                }
            }
        }
    }
    
    public void Morte() {     
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        animator.SetTrigger("Death");
        ativo = false;
        Invoke("DESTRUIR",1f);
    }

    private void DESTRUIR() {
        Destroy(gameObject);
    }
    
    private void MoveCharacter(Vector2 dir) {
        if (ativo) {
            rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
        }
    }
}