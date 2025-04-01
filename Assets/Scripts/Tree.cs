using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool isOnFire;
    public SpriteRenderer TreeFire;
    public Sprite Fire;
    public Sprite noFire;
    public BoxCollider2D col;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TreeFire = gameObject.GetComponent<SpriteRenderer>();
        TreeFire.sprite = Fire;
        isOnFire = true;
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    public void Extinguish()
    {
        if (isOnFire)
        {
            isOnFire = false;
            TreeFire.sprite = noFire;
            col.enabled = false;
        }
    }
}