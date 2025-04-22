using System;
using Unity.Mathematics;
using UnityEngine;

public class Pointto : MonoBehaviour
{
    public Transform animal;

    private float HideDistance = 5f;

    public Transform player;

    public Transform icon;
    public float orbitRadius = 2f;
    
    Vector3 faces = new Vector3(1, 1, 1);
    // Update is called once per frame
    void Update()
    {

        // Get direction from player to animal
        Vector3 direction = (animal.position - player.position).normalized;

        // Position the arrow on the orbit circle around the player
        transform.position = player.position + direction * orbitRadius;

        // Make the arrow point at the animal
        Vector3 lookDirection = animal.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Keep the icon upright (unrotate it)
        icon.rotation = Quaternion.identity;
        
        if (player.transform.localScale == faces)
        {
            transform.localScale = Vector3.one;
            Debug.Log("vector one");
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("-1 x localscale");
        }

        /*var dir = Animal.position - transform.position;

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


        
        
        icon.localScale = new Vector3(
            1f / transform.localScale.x, 
            1f / transform.localScale.y, 
            1f / transform.localScale.z);*/
    }

    void setChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
