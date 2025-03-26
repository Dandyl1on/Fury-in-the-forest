using UnityEngine;

public class Cameracontroller : MonoBehaviour
{

    [SerializeField] private Transform Player;
    [SerializeField] private float distance;
    [SerializeField] private float cameraSpeed;

    private float lookAHead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x + lookAHead, Player.position.y+1.5f, transform.position.z);
        lookAHead = Mathf.Lerp(lookAHead, (distance * Player.localScale.x), Time.deltaTime*cameraSpeed);
    }
}
