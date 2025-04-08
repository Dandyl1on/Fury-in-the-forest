using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public static FollowerManager Instance;
    public GameObject player;

    private List<GameObject> followers = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddFollower(GameObject followerObj)
    {
        Follower follower = followerObj.GetComponent<Follower>();

        if (follower != null)
        {
            if (followers.Count == 0)
            {
                follower.targetToFollow = player.transform;
            }
            else
            {
                follower.targetToFollow = followers[followers.Count - 1].transform;
            }

            follower.StartFollowing();

            // ✅ Add this line to sync animations
            followerObj.GetComponent<FollowerAnimatorSync>()?.StartFollowing(player.transform);

            followers.Add(followerObj);
        }
    }



}
