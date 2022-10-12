using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rb2d;
    public Vector2 movement;
    public Animator anim;
    
    private Vector3 left;
    private Vector3 right;
    
    void Start(){
        left = transform.localScale;
        right = transform.localScale;
        
        Debug.Log(transform.position);
        
        left.x *= -1;
    }
    
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (movement.x > 0 || movement.y > 0 || movement.x  < 0 || movement.y < 0) {
            anim.SetBool("isRunning",  true);
        }else if (movement.x == 0 || movement.y == 0){
            anim.SetBool("isRunning",  false);
        }
        
        if (movement.x > 0) {
            transform.localScale = right;
        }else if (movement.x < 0) {        
           transform.localScale = left;
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(rb2d.position + "/" + movement + "/" + (rb2d.position + movement)
                  + "/" + transform.localScale);
        rb2d.MovePosition(rb2d.position + movement * (speed * Time.fixedDeltaTime));
    }
    
}
