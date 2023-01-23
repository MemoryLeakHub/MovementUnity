using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private bool isCollected;
    private float timeElapsed = 0;
    public bool isCollecting = false;
    private Transform character;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (isCollecting) {
            var speed = 2; // seconds
            var step = speed * Time.deltaTime;
            var target = character.transform.position;
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target, step);
            if (Vector3.Distance(gameObject.transform.position, target) < 0.001f)
            {
                isCollecting = false;
                Destroy(gameObject);
            }
        }
    }

    public void StartCollecting(Transform target) { 
        timeElapsed = 0;
        isCollecting = true;
        character = target;
    }
}
