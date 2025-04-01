using UnityEngine;

public class Cameracontroller : MonoBehaviour
{

    [SerializeField] private Transform Player;
    [SerializeField] private float distance;
    [SerializeField] private float cameraSpeed;
    public ZiplineScript zipline;
    public bool OnZipline;

    private float lookAHead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnZipline) // Only follow normally if not on a zipline
        {
            transform.position = new Vector3(Player.position.x + lookAHead, Player.position.y, transform.position.z); 
            lookAHead = Mathf.Lerp(lookAHead, (distance * Player.localScale.x), Time.deltaTime * cameraSpeed);
        }
        else
        {
            // Smoothly move camera to match zipline motion
            float targetX = Mathf.Lerp(transform.position.x, Player.position.x + 2f, Time.deltaTime * cameraSpeed);
            float targetY = Mathf.Lerp(transform.position.y, Player.position.y - 1.5f, Time.deltaTime * cameraSpeed);
            transform.position = new Vector3(targetX, targetY, transform.position.z);
            // transform.position = Vector3.Lerp(transform.position, new Vector3(Player.position.x*1.5f, Player.position.y-2, transform.position.z), Time.deltaTime * cameraSpeed);
        }
    }
}
