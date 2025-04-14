using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Header("Following")]
    public Transform targetToFollow;
    public float followSpeed = 8f;
    public float followDelay = 0.5f;
    private bool isFollowing = false;
    private int pathIndexOffset;

    [Header("Interaction")]
    public string playerTag = "Player";
    private bool isSaved = false;

    [Header("Animation")]
    private Animator myAnimator;
    private Animator foxAnimator;

    // Idle2
    private float idle2Chance = 0.2f;
    private float idle2CheckInterval = 3f;
    

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        InvokeRepeating(nameof(CheckIdle2), idle2CheckInterval, idle2CheckInterval);
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
            transform.position = Vector2.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);

            // Face direction
            if (targetPos.x > transform.position.x)
                transform.localScale = Vector3.one;
            else if (targetPos.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isSaved && other.gameObject.CompareTag("Player"))
        {
            isSaved = true;

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

    // ===============================
    // IDLE2 SUPPORT
    // ===============================
    void CheckIdle2()
    {
        if (!isFollowing && Random.value < idle2Chance)
        {
            myAnimator.SetTrigger("Idle2");
        }
    }
}
