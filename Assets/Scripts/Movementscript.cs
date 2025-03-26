using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Playermovement : MonoBehaviour
{   
    //the horizontal float is for movement either 1 for right or -1 for left and 0 for no movement
    public float horizontalInput;
    public float speed = 8f;
    public float jumpPower;

    //below all are used for basic player stuff
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput*speed, rb.linearVelocity.y);
        
        // jumps the player according to its x direction and jump power (is in update to not miss button press)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Debug.Log("jumps");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // returns a true or false depending on if the overlap circle is well overlapping with the ground layer integer on the layer list (set this in the editor)
    private bool isGrounded()
    {
        
        Debug.Log("is grounded");
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }
}
