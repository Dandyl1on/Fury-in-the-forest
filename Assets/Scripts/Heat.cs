using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Heat : MonoBehaviour
{
    public Slider HeatSlider;
    public float IncreaseHeat;
    public float DecreaseHeat;

    public Transform respawn;
    public bool beginHeat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HeatSlider.GetComponent<Slider>();
    }

    

    private void Update()
    {
        if (beginHeat)
        {
            HeatSlider.value += 4.5f * Time.deltaTime;
        }
        
        if (HeatSlider.value == HeatSlider.maxValue)
        {
            transform.position = respawn.position;
            beginHeat = false;
            HeatSlider.value = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Flames"))
        {
            HeatSlider.value += IncreaseHeat * Time.deltaTime;
            if (HeatSlider.value == HeatSlider.maxValue)
            {
                transform.position = respawn.position;
                beginHeat = false;
                HeatSlider.value = 0f;
            }
        }

        if (other.CompareTag("Water"))
        {
            HeatSlider.value-= Time.deltaTime * DecreaseHeat;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Heat begin"))
        {
            beginHeat = true;
        }
    }
}
