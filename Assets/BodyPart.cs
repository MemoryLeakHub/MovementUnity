using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Transform FollowTarget;
    public float  SpaceBetween;
    public float  Speed;
    private void Update()
    {
        Vector3 vectorToTarget = FollowTarget.position - transform.position;
        Vector3 rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
        var distanceToHead = (transform.position - FollowTarget.position).magnitude;
        if (distanceToHead > SpaceBetween)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }
}
