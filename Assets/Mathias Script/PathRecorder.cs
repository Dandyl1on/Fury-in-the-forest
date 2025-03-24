using System.Collections.Generic;
using UnityEngine;

public class PathRecorder : MonoBehaviour
{
    public static PathRecorder Instance;

    public float recordInterval = 0.02f;
    public int maxPoints = 5000;

    [HideInInspector] public List<Vector2> recordedPath = new List<Vector2>();

    private float timer = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= recordInterval)
        {
            recordedPath.Insert(0, transform.position); // newest first
            timer = 0f;

            if (recordedPath.Count > maxPoints)
                recordedPath.RemoveAt(recordedPath.Count - 1);
        }
    }
}
