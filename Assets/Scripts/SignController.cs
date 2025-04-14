using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public TextMeshProUGUI toolText;

    public Image A;

    public Image D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toolText.enabled = false;
        A.enabled = false;
        D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            toolText.text = ("Use A and D keys to move left and right");
            toolText.enabled = true;
            A.enabled = true;
            D.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            toolText.enabled = false;
            A.enabled = false;
            D.enabled = false;
        }
    }
}
