using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // From Part 1
    // public float speed = 10f;
    // public Rigidbody2D rb;
    // void Start()
    // {
    //     rb.velocity = transform.right * speed;
    // }

    // Part 2
    public Rigidbody2D rb;
    public TrailRenderer trailRenderer;
    public void Shoot(float speed) { 
        rb.velocity = transform.right * speed;
    }

    public void ShowTrail() { 
        trailRenderer.enabled = true;
    }
}
