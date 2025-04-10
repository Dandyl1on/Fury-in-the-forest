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
    }

    

    private void Update()
    {
        HeatSlider.value += Time.deltaTime * 0.5f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Flames"))
        {
            HeatSlider.value+= Time.deltaTime * IncreaseHeat;
            if (HeatSlider.value == HeatSlider.maxValue)
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
