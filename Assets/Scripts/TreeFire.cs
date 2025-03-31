using UnityEngine;

public class TreeFire : MonoBehaviour
{
    public bool isOnFire;
    public SpriteRenderer Tree;
    public Sprite Fire;
    public Sprite noFire;
    public BoxCollider2D col;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tree = gameObject.GetComponent<SpriteRenderer>();
        Tree.sprite = Fire;
        isOnFire = true;
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    public void Extinguish()
    {
        if (isOnFire)
        {
            isOnFire = false;
            Tree.sprite = noFire;
            col.enabled = false;
        }
    }
}