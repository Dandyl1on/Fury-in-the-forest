using System;
using System.Collections;
using UnityEngine;


public class Firepit : MonoBehaviour
{
    
    public Transform respawnPoint;
    public AudioClip[] DeathSound;
    private AudioSource Audio;
    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FirePit"))
        {
            playsound();
            StartCoroutine(Death());
        }
    }
    void playsound()
    {
        int index = UnityEngine.Random.Range(0, DeathSound.Length);
        AudioClip death = DeathSound[index];
        Audio.clip = death;
        Audio.Play();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = respawnPoint.position;
    }
    
}