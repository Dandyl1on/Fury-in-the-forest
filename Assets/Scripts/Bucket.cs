using UnityEngine;
using UnityEngine.InputSystem;

public class Bucketscript : MonoBehaviour
{
    public bool hasWater;
    public bool filableWater;
    public bool isNearTree;
    public TreeFire currentTree; 

    public int bucketCharge;
    
    // redundant movement delete after use
    public float speed = 5f; 
    private Rigidbody2D rb; 
    private float moveInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y); // Move left/right
    }
    
    // redundant movement delete after use

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");  // redundant movement delete after use

        if (filableWater && Input.GetKeyDown(KeyCode.Space))
        {
            hasWater = true;
            bucketCharge = 2;
        }
        
        if (isNearTree && hasWater && Input.GetKeyDown(KeyCode.Space))
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