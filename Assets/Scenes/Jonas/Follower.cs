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
        FollowPath();
        SyncAnimations(); 
    }

    // ===============================
    // FOLLOWING THE FOX
    // ===============================
    void FollowPath()
    {
        var path = PathRecorder.Instance.recordedPath;
        int index = Mathf.Clamp(path.Count - pathIndexOffset - 1, 0, path.Count - 1);

        if (path.Count == 0 || index < 0) return;

        Vector2 targetPos = path[index];
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPos);
 
        if (distance < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.linearVelocity = direction * followSpeed;
        }
        if (path.Count < pathIndexOffset + 1)
        {
            rb.linearVelocity = Vector2.zero;
            return; // Not enough path yet
        }

        // Maintain orientation
        if (Player.transform.localScale.x == 1)
            transform.localScale = Vector3.one;
        else if (Player.transform.localScale.x == -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void StartFollowing(int orderline)
    {
        isFollowing = true;
        pathIndexOffset = Mathf.RoundToInt((followDelay * orderline) / PathRecorder.Instance.recordInterval);
    }

    // ===============================
    // INTERACTION
    // ===============================
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Player.pressE)
        {
            // Add to FollowerManager
            FollowerManager.Instance.AddFollower(gameObject);

            // Start animation syncing
            foxAnimator = other.GetComponent<Animator>();
            //StartFollowing(3);
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
