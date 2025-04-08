using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int doubleJump;

    public float delayjump = 0.2f;
    public bool hasJumped;
    
    public float idle2chance = 0.2f;
    public float checkinterval = 1f;

    public Animator Animation;
    private Transform Player;

    public GameObject ZiplineText;

    //below all are used for basic player stuff
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        Animation = GetComponent<Animator>();
        Player = GetComponent<Transform>();
        InvokeRepeating(nameof(checkforIdle2), checkinterval, checkinterval);
        ZiplineText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput*speed, rb.linearVelocity.y);
        
        if (grounded)
        {
            hasJumped = false;
        }
        else
        {
            delayjump += Time.deltaTime;
        }

        // jumps the player according to its x direction and jump power (is in update to not miss button press)
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !Falls)
        {
            Jump();
            
            hasJumped = false;
        }
        
        // delayed jump off cliff
        if (Input.GetKeyDown(KeyCode.Space) && delayjump < 0.2f && Falls)
        {
            Jump();
            
        }
        // double jump
        if (Input.GetKeyDown(KeyCode.Space) && Falls && !hasJumped && delayjump > 0.2f) 
        {
            Jump();
            hasJumped = true;
            
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
            delayjump = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
            if (Animation.GetBool("ZipLine")==false)
            {
                Falls = true;    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zipline"))
        {
            ZiplineText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Zipline"))
        {
            ZiplineText.SetActive(false);
        }
    }
}
