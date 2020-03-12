using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToFollow;
    [SerializeField]
    private float xOffset, yOffset, zOffset;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x + xOffset, objectToFollow.transform.position.y + yOffset, objectToFollow.transform.position.z + zOffset);
    }//End Update
}