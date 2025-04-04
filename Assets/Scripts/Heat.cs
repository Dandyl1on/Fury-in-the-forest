using System;
using UnityEngine;
using UnityEngine.UI;

public class Heat : MonoBehaviour
{
    public Slider HeatSlider;
    public float IncreaseHeat;
    public float DecreaseHeat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HeatSlider.GetComponent<Slider>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Flames"))
        {
            HeatSlider.value+= Time.deltaTime * IncreaseHeat;
        }

        if (other.CompareTag("Water"))
        {
            HeatSlider.value-= Time.deltaTime * DecreaseHeat;
        }
    }
}
