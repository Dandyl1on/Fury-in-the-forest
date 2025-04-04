using UnityEngine;

public class Fire : MonoBehaviour
{
    public bool isOnFire;
    public GameObject ThisFire;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ThisFire = GetComponent<GameObject>();
        isOnFire = true;
    }

    public void Extinguish()
    {
        if (isOnFire)
        {
            Destroy(ThisFire);
        }
    }
}