using Unity.Mathematics;
using UnityEngine;

public class Pointto : MonoBehaviour
{
    public Transform Animal;

    private float HideDistance = 5f;
    // Update is called once per frame
    void Update()
    {
        var dir = Animal.position - transform.position;

        if (dir.magnitude < HideDistance)
        {
            setChildrenActive(false);
        }
        else
        {
            setChildrenActive(true);
            
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void setChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
