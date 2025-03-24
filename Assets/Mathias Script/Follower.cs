using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Follower : MonoBehaviour
{
    public Transform targetToFollow; // ← This will be the player or previous follower
    public float followDistance = 0.6f;
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public float fallMultiplier = 3f;
    public bool isFollowing = false;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;
    public SpriteRenderer sr;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isHopping = false;
    public float hopHeight = 0.5f;
    public float hopDuration = 0.2f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isFollowing || targetToFollow == null) return;

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        Vector2 targetPos = targetToFollow.position;
        Vector2 currentPos = rb.position;
        Vector2 direction = targetPos - currentPos;
        float distance = direction.magnitude;

        // Only move if too far
        if (distance > followDistance)
        {
            // Jump if needed
            if (isGrounded && direction.y > 0.2f && direction.y < 3f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }

            // Faster falling
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
            }

            // Horizontal movement
            rb.linearVelocity = new Vector2(direction.normalized.x * moveSpeed, rb.linearVelocity.y);

            // Flip sprite
            if (sr != null && Mathf.Abs(direction.x) > 0.01f)
                sr.flipX = direction.x < 0;
        }
        else
        {
            // Maintain vertical velocity but stop horizontal jitter
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    IEnumerator ArrivalHop()
    {
        isHopping = true;

        float startY = transform.position.y;
        float peakY = startY + hopHeight;
        float elapsedTime = 0f;

        while (elapsedTime < hopDuration / 2)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(startY, peakY, elapsedTime / (hopDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < hopDuration / 2)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(peakY, startY, elapsedTime / (hopDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector2(transform.position.x, startY);
        isHopping = false;
    }
}
