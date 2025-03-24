using UnityEngine;
using UnityEngine.InputSystem;

public class Bucketscript : MonoBehaviour
{
    public bool hasWater;
    public bool filableWater;
    public bool isNearTree;
    private TreeFire currentTree; // This will hold the TreeFire component of the current tree

    public int bucketCharge;

    // Update is called once per frame
    void Update()
    {
        if (filableWater && Input.GetKeyDown(KeyCode.Space))
        {
            hasWater = true;
            bucketCharge = 2;
        }
        
        if (currentTree != null && isNearTree && hasWater && Input.GetKeyDown(KeyCode.Space))
        {
            currentTree.Extinguish(); // Call Extinguish on the current tree
            bucketCharge -= 1;
            
            if (bucketCharge <= 0)
            {
                hasWater = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the player is near a tree and store the TreeFire component
        if (col.CompareTag("Tree"))
        {
            currentTree = col.GetComponent<TreeFire>();
            if (currentTree != null && currentTree.isOnFire)
            {
                isNearTree = true; // Mark that we are near a tree
            }
        }

        // Check if the player is near water to refill the bucket
        if (col.CompareTag("Water"))
        {
            filableWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Tree"))
        {
            isNearTree = false; // Exit when we are no longer near the tree
            currentTree = null; // Remove reference to the tree
        }

        if (col.CompareTag("Water"))
        {
            filableWater = false; // Stop refilling when leaving water
        }
    }
}