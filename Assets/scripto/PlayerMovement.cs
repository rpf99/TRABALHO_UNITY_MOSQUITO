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
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        anim.SetFloat("horizontal", movement.x);
        anim.SetFloat("vertical", movement.y);
        anim.SetFloat("velocidade", movement.sqrMagnitude);

        if (movement != Vector2.zero)
        {
            anim.SetFloat("horizontalIdle", movement.x);
            anim.SetFloat("verticalIdle", movement.y);

        }
    }
    
    private void FixedUpdate()
    {
        //Debug.Log(rb2d.position + "/" + movement + "/" + (rb2d.position + movement)
          //        + "/" + transform.localScale);
        rb2d.MovePosition(rb2d.position + movement * (speed * Time.fixedDeltaTime));
    }
    
}
