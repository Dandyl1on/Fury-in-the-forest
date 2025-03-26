using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Playermovement : MonoBehaviour
{   
    //the horizontal float is for movement either 1 for right or -1 for left and 0 for no movement
    public float horizontal;
    public float speed = 8f;
    public float jumpPower;
    //determines if what way the player is facing, might be used for flipping the sprite
    public bool isFacingRight = true;
    
    //players sprite renderer
    public SpriteRenderer sprite;
    
    
    
    //below all are used for basic player stuff
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets the horizontal RawAxis so its either 1, 0 or -1
        horizontal = Input.GetAxisRaw("Horizontal");
        
        // jumps the player according to its x direction and jump power (is in update to not miss button press)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
    }

    private void FixedUpdate()
    {
        while (horizontal == 1 || horizontal == -1)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
            Flip();
        }
        // handles the movements 
        /*if (horizontal == 1 || horizontal == -1)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
            Flip();
        }*/
    }

    // returns a true or false depending on if the overlap circle is well overlapping with the ground layer integer on the layer list (set this in the editor)
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    // returns a true or false depending on if the overlap circle is well overlapping with the wall layer integer on the layer list (set this in the editor)
   

    

    private void Flip()
    {
        //flips sprite left
        if (isFacingRight && horizontal < 0f)
        {
            sprite.flipX = true;
            isFacingRight = false;
        }

        //flips sprite right
        if (!isFacingRight && horizontal > 0f)
        {
            sprite.flipX = false;
            isFacingRight = true;
        }
    }
}
