using System.Collections.Generic;
using Scenes.Jonas;
using UnityEngine;

namespace Mathias_Script
{
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
            if (followers.Contains(followerObj)) return; // Avoid duplicates

            Follower follower = followerObj.GetComponent<Follower>();

            if (follower != null)
            {
                Transform followTarget = followers.Count == 0
                    ? player.transform
                    : followers[followers.Count - 1].transform;

                follower.StartFollowing(followTarget, followers.Count + 1);
                followers.Add(followerObj);
            }
        }
    }
}
