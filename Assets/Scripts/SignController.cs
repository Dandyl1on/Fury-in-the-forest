using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public TextMeshProUGUI toolText;

    public Image A;
    public Image D;
    public Image W;
    public Image S;
    public Sprite E;
    public Image Space;
    public Pause sign;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toolText.enabled = false;
        A.enabled = false;
        D.enabled = false;
        W.enabled = false;
        S.enabled = false;
        Space.enabled = false;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sign"))
        {
            sign = other.GetComponent<Pause>();
            sign.stop = true;
            toolText.enabled = true;
            A.enabled = true;
            D.enabled = true;
            W.enabled = true;
            S.enabled = true;
            Space.enabled = true;
            
        }

        if (other.CompareTag("Rescue sign"))
        {
            sign = other.GetComponent<Pause>();
            sign.stop = true;

            toolText.enabled = true;
            toolText.text = "Press E to rescue the animals Press E to continue";
            S.enabled = true;
            S.sprite = E;
        }

        if (other.CompareTag("Fire sign"))
        {
            sign = other.GetComponent<Pause>();
            sign.stop = true;

            toolText.enabled = true;
            toolText.text = "Get water by pressing E in a pond and lower the heat meter to avoid dying from overheating Press E to continue";
            S.enabled = true;
            S.sprite = E;
        }
        
        if (other.CompareTag("ZipSign"))
        {
            sign = other.GetComponent<Pause>();
            sign.stop = true;

            toolText.enabled = true;
            toolText.text = "Jump up to the rope and press E to use zipline. Press E to continue";
            S.enabled = true;
            S.sprite = E;
        }

        if (other.CompareTag("WinSign"))
        {
            sign = other.GetComponent<Pause>();
            sign.stop = true;

            toolText.enabled = true;
            toolText.text = "If you have found all 3 animals you can find the last Zipline to escape the burning forest Press E to continue";
        }

        if (other.CompareTag("GoSign"))
        {
            sign.stop = true;

            toolText.enabled = true;
            toolText.text = "Press E to continue to the 1st level";
            S.enabled = true;
            S.sprite = E;
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("GoSign") )
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(1);    
            }
                
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        toolText.enabled = false;
        A.enabled = false;
        D.enabled = false;
        W.enabled = false;
        S.enabled = false;
        Space.enabled = false;
        Destroy(sign);
    }
}
