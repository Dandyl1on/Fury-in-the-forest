using System;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Playermovement Player;
    [Header("Following")]
    public Transform targetToFollow;
    public float followSpeed = 8f;
    public float followDelay = 0.2f;
    private bool isFollowing = false;
    private int pathIndexOffset;

    [Header("Interaction")]
    public string playerTag = "Player";

    public Playermovement Playermovement;

    [Header("Animation")]
    private Animator myAnimator;
    private Animator foxAnimator;

    public Rigidbody2D rb;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isFollowing) return;

        FollowPath();
        SyncAnimations();
        
    }

    // ===============================
    // FOLLOWING THE FOX
    // ===============================
    void FollowPath()
    {
        var path = PathRecorder.Instance.recordedPath;
        int index = Mathf.Min(pathIndexOffset, path.Count - 1);

        if (index >= 0 && index < path.Count)
        {
            Vector2 targetPos = path[index];
            Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
            float distance = Vector2.Distance(transform.position, targetPos);

            if (distance < 0.5f)
            {
                rb.linearVelocity = Vector2.zero;
            }
            else
            {
                rb.linearVelocity = direction * followSpeed;
            }
            
            
            if (Player.transform.localScale.x == 1)
                transform.localScale = Vector3.one;
            else if (Player.transform.localScale.x == -1)
                transform.localScale = new Vector3(-1, 1, 1);
            
            /*transform.position = Vector2.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);

            // Face direction
            if (targetPos.x > transform.position.x)
                transform.localScale = Vector3.one;
            else if (targetPos.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);*/
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
        pathIndexOffset = Mathf.RoundToInt(followDelay / PathRecorder.Instance.recordInterval);
    }

    // ===============================
    // INTERACTION
    // ===============================
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Playermovement.pressE)
        {
            // Add to FollowerManager
            FollowerManager.Instance.AddFollower(gameObject);

            // Start animation syncing
            foxAnimator = other.GetComponent<Animator>();
            StartFollowing();
        }
    }

    // ===============================
    // ANIMATION SYNC
    // ===============================
    void SyncAnimations()
    {
        if (foxAnimator == null) return;

        myAnimator.SetBool("IsMoving", foxAnimator.GetBool("IsMoving"));
        myAnimator.SetBool("IsGround", foxAnimator.GetBool("IsGround"));
        myAnimator.SetBool("Falling", foxAnimator.GetBool("Falling"));

        SyncTrigger("Jump");
        SyncTrigger("Land");
        SyncTrigger("TakingDamage");
        SyncTrigger("Faint");
    }

    void SyncTrigger(string triggerName)
    {
        if (foxAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerName))
        {
            myAnimator.SetTrigger(triggerName);
        }
    }
}
