using System;
using UnityEngine;


public class Firepit : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FirePit"))
        {
            transform.position = respawnPoint.position;
        }
    }
}