using UnityEngine;
using UnityEngine.InputSystem;

public class Bucketscript : MonoBehaviour
{
    public bool hasWater;
    public bool filableWater;
    public bool isNearTree;
    public TreeFire currentTree; 

    public int bucketCharge;
    
   
   

    // Update is called once per frame
    void Update()
    {

        if (filableWater && Input.GetKeyDown(KeyCode.E))
        {
            hasWater = true;
            bucketCharge = 2;
        }
        
        if (isNearTree && hasWater && Input.GetKeyDown(KeyCode.E))
        {
            currentTree.Extinguish(); 
            bucketCharge -= 1;
            
            if (bucketCharge <= 0)
            {
                hasWater = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Tree"))
        {
            currentTree = col.GetComponent<TreeFire>();
            isNearTree = true; 
            
        }

        if (col.CompareTag("Water"))
        {
            filableWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Tree"))
        {
            isNearTree = false;
        }

        if (col.CompareTag("Water"))
        {
            filableWater = false;
        }
    }
}