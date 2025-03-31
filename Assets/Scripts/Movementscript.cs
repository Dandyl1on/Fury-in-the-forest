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
    public bool grounded;
    public bool Falls;
    private bool DoubleJump;
    private int doubleJump = 0;
    
    public float idle2chance = 0.2f;
    public float checkinterval = 1f;

    public Animator Animation;
    private Transform Player;

    //below all are used for basic player stuff
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        Animation = GetComponent<Animator>();
        Player = GetComponent<Transform>();
        InvokeRepeating(nameof(checkforIdle2), checkinterval, checkinterval);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput*speed, rb.linearVelocity.y);
        
        // jumps the player according to its x direction and jump power (is in update to not miss button press)
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //Debug.Log(DoubleJump);
            Jump();
            doubleJump = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump == 1)
        {
            Jump();
            doubleJump = 2;
        }
        Animation.SetBool("IsMoving", horizontalInput !=0);
        Animation.SetBool("IsGround", grounded);
        Animation.SetBool("Falling", Falls);
        
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void checkforIdle2()
    {
        if (UnityEngine.Random.value < idle2chance)
        {
            Animation.SetTrigger("Idle2");
        }
    }

    
    void Jump()
    {
        Animation.SetTrigger("Jump");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        grounded = false;
    }

    // returns a true or false depending on if the overlap circle is well overlapping with the ground layer integer on the layer list (set this in the editor)
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
            Falls = false;
            doubleJump = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (grounded && Animation.GetBool("ZipLine")==false)
            {
                Falls = true;    
            }
            
        }
    }
}
