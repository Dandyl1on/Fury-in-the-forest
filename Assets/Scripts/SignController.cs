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
    public Image W;
    public Image S;
    public Sprite E;
    public Image Space;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sign"))
        {
            toolText.enabled = true;
            A.enabled = true;
            D.enabled = true;
            W.enabled = true;
            S.enabled = true;
            Space.enabled = true;
        }

        if (other.CompareTag("Rescue sign"))
        {
            toolText.enabled = true;
            toolText.text = "Press E to rescue the animals";
            S.enabled = true;
            S.sprite = E;
        }

        if (other.CompareTag("Fire sign"))
        {
            toolText.enabled = true;
            toolText.text = "Find a pool of water and press E to get a full Bucket 'o Water so you can throw it on the fire by standing near it and pressing E";
            S.enabled = true;
            S.sprite = E;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.CompareTag("Sign"))
        {
            toolText.enabled = false;
            A.enabled = false;
            D.enabled = false;
            W.enabled = false;
            S.enabled = false;
            Space.enabled = false;
        }*/

        toolText.enabled = false;
        A.enabled = false;
        D.enabled = false;
        W.enabled = false;
        S.enabled = false;
        Space.enabled = false;
    }
}
