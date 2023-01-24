using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Transform FollowTarget;
    public float SpaceBetween;
    private float defaultSpeed = 1.5f;
    private float speed = 1.5f;
   
    public void ChangeSpeed(bool isHoldingSpace) { 
        if (isHoldingSpace) {
            speed = defaultSpeed * 2;
        } else {
            speed = defaultSpeed;
        }
    }
    public void Start() { 

    }
    private void Update()
    {
        Vector3 vectorToTarget = FollowTarget.position - transform.position;
        Vector3 rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
        var distanceToHead = (transform.position - FollowTarget.position).magnitude;
        if (distanceToHead > SpaceBetween)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
