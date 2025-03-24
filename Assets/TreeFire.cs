using UnityEngine;

public class TreeFire : MonoBehaviour
{
    public bool isOnFire;
    private SpriteRenderer Tree;
    public Sprite Fire;
    public Sprite noFire;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tree = gameObject.GetComponent<SpriteRenderer>();
        Tree.sprite = Fire;
        isOnFire = true;
    }

    public void Extinguish()
    {
        if (isOnFire)
        {
            isOnFire = false;
            Tree.sprite = noFire;
        }
    }
}