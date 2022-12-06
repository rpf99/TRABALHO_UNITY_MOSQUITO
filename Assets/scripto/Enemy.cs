using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public int picadas;

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
    
    private void Start() 
    {   
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        pl = target.GetComponent<PlayerMovement>();
        Asource = GetComponent<AudioSource>();
        Asource.Stop();
        picadas = 0;
    }
    
    
    private void Update()
    {
        animator.SetBool("isFlying", isInChaseRange);
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);
        
        if (!Asource.isPlaying) {
            Asource.Play();
        }else if (pl.Perdeu()) {
            Asource.Stop();
        }
        
        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;
        
        if(shouldRotate) {
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
        }
    }
    
    private void FixedUpdate() {
        if(isInChaseRange && isInAttackRange) {
            MoveCharacter(movement);
        }
        
        if(isInAttackRange) {
            rb.velocity = Vector2.zero;
            if (picadas < 1){
                Asource.Stop();
                pl.ReceberDano();
                picadas++;
            }
        }
    }

    private void MoveCharacter(Vector2 dir) {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }
}