using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 3f;
    public Rigidbody2D rb2d;
    public Vector2 movement;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement  * (speed * Time.fixedDeltaTime));
    }
}
