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


    public float delayjump = 0.2f;
    public bool doubleJump;
    
    public float idle2chance = 0.2f;
    public float checkinterval = 1f;

    public Animator Animation;
    private Transform Player;

    public GameObject ZiplineText;

    public bool lookDown;

    public AudioClip jumpSound;
    private AudioSource Audio;

    //below all are used for basic player stuff
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        Animation = GetComponent<Animator>();
        Player = GetComponent<Transform>();
        InvokeRepeating(nameof(checkforIdle2), checkinterval, checkinterval);
        ZiplineText.SetActive(false);
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        
        Animation.SetBool("IsMoving", horizontalInput != 0);
        Animation.SetBool("IsGround", grounded);
        Falls = rb.linearVelocity.y < -0.1f && !grounded;
        Animation.SetBool("Falling", Falls);
        
        
        if (grounded)
        {
            doubleJump = false;
        }
        else
        {
            delayjump += Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !Falls)
        {
            Jump();
            doubleJump = false;
        }

        // delayed jump off cliff
        if (Input.GetKeyDown(KeyCode.Space) && delayjump < 0.2f && Falls)
        {
            Jump();
        }

        // double jump
        if (Input.GetKeyDown(KeyCode.Space) && !doubleJump && delayjump > 0.2f)
        {
            Jump();
            doubleJump = true;

        }
        //Rotates the sprite correctly
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //looks down on key press
        if (Input.GetKeyDown(KeyCode.S))
        {
            lookDown = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            lookDown = false;
        }
        
    }

    void Jump()
    {
        Animation.SetTrigger("Jump");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        Audio.clip = jumpSound;
        Audio.Play();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
            Falls = false;
            delayjump = 0f;
        }

        if (col.gameObject.tag == "Zipline")
        {
            ZiplineText.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (other.gameObject.tag== "Zipline")
        {
            ZiplineText.SetActive(false);
        }
    }

    void checkforIdle2()
    {
        if (UnityEngine.Random.value < idle2chance)
        {
            Animation.SetTrigger("Idle2");
        }
    }
}
