using Unity.Mathematics;
using UnityEngine;


public class Bucketscript : MonoBehaviour
{
    public bool hasWater;
    public bool filableWater;
    public bool isNearTree;
    public Fire currentTree;

    public int bucketCharge;
    public SpriteRenderer Bucket;
    public Sprite Empty;
    public Sprite Half;
    public Sprite Full;
    
    // Update is called once per frame
    void Update()
    {

        if (filableWater && Input.GetKeyDown(KeyCode.E))
        {
            hasWater = true;
            bucketCharge = 2;
            Bucket.sprite = Full;
        }
        
        if (isNearTree && hasWater && Input.GetKeyDown(KeyCode.E))
        {
            currentTree.Extinguish(); 
            bucketCharge -= 1;
            Bucket.sprite = Half;
            
            if (bucketCharge <= 0)
            {
                hasWater = false;
                Bucket.sprite = Empty;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Flames"))
        {
            currentTree = col.GetComponent<Fire>();
            isNearTree = true; 
            
        }

        if (col.CompareTag("Water"))
        {
            filableWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Flames"))
        {
            isNearTree = false;
        }

        if (col.CompareTag("Water"))
        {
            filableWater = false;
        }
    }
}