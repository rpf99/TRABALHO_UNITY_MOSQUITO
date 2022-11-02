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

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Velocidade", movement.sqrMagnitude);

        if (movement != Vector2.zero)
        {
            anim.SetFloat("HorizontalIdle", movement.x);
            anim.SetFloat("VerticalIdle", movement.y);
        }
    }
    
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * (speed * Time.fixedDeltaTime));
    }
}
