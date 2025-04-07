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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HeatSlider.GetComponent<Slider>();
        HeatSlider.maxValue = 5f;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Flames"))
        {
            HeatSlider.value+= Time.deltaTime * IncreaseHeat;
            if (HeatSlider.value == 5)
            {
                transform.position = respawn.position;
                HeatSlider.value = 0f;
            }
        }

        if (other.CompareTag("Water"))
        {
            HeatSlider.value-= Time.deltaTime * DecreaseHeat;
        }
    }
}
