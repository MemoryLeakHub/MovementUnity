using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject box;
    public GameObject redBox;
    public GameObject blueBox;
    public GameObject boxFacingLine;
    
    public GameObject distanceGroup;
    public GameObject timerGroup;
    public TMP_Text timerText;
    public TMP_Text distanceText;
    private float visualTime = 0.0f;
    private float visualDistance = 0.0f;
    private float timer = 0.0f;
    private bool start = false;
    private int example = 0;
    public void ResetPosition() {
        HideFacingLine();
        box.transform.rotation = Quaternion.identity;
        start = false;
        visualDistance = 0;
        distanceGroup.active = false;
        distanceText.gameObject.active = false;
        timer = 0;
        visualTime = 0;
        timerGroup.active = false;
        redBox.active = false;
        timerText.gameObject.active = false;
        box.transform.position = new Vector2(-5.72f, 1.12f);
        redBox.transform.position = new Vector2(-5.72f, 1.12f);
    }
    public void Trigger() { 
        start = true;
    }
    // Example 1 - transform.position
    // Example 2 - transform.Translate 
    // Example 3 = transform.position onUpdate
    // Example 4 = transform.position onUpdate
    // ** Vector.right
    // Example 5 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector.right 
    // Example 6 = transform.position onUpdate
    // * Show - Red Box, Rotate Current Box
    // ** Vector.right
    // Example 7 = transform.Translate onUpdate
    // * Show - Red Box
    // ** Vector.right
    // Example 8 = transform.Translate onUpdate
    // * Show - Red Box, Rotate Current Box
    // ** Vector.right
    // Example 9 = transform.Translate onUpdate
    // * Show - Red Box, Direction
    // ** Vector.right
    // Example 10 = transform.Translate onUpdate
    // * Show - Red Box, Direction
    // ** Direction based on Magnitude
    // Example 11 = transform.Translate onUpdate
    // * Show - Red Box, Direction
    // ** Direction based on Magnitude
    // ** Change Y destination of end target
    // Example 12 = transform.Translate onUpdate
    // * Show - Red Box, Direction
    // ** Direction based on Magnitude
    // ** Change Y destination of end target
    // ** Set Y of heading as 0 so we move only X

    // Directional Vectors
    // Vector3.right     // (1,  0,  0)
    // Vector3.left      // (-1, 0,  0)
    // Vector3.up        // (0,  1,  0)
    // Vector3.down      // (0, -1,  0)
    // Vector3.forward   // (0,  0,  1)
    // Vector3.back      // (0,  0, -1)

    public void SetExample(int value) {
        example = value;
    }
    // Example 1
    // transform.position - set the position anywhere in the map
    // move x position
    // keep y position the same
    public void SetXPosition(float x) { 
        box.transform.position = new Vector3(x, box.transform.position.y);
    }
    // Example 2
    // change the position based on the current box position
    // add to the box x amount
    public void TranslateX(float x) { 
        box.transform.Translate(x,0,0);
    }
    void Update() { 
        if (!start) {
            return;
        }
        timer += Time.deltaTime;
        if (example == 3) {
            box.transform.position += new Vector3(2 * Time.deltaTime,0);   
        }
        if (example == 4) {
            box.transform.position += Vector3.right * 2 * Time.deltaTime;    
        }
        if (example == 5) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.position += Vector3.right * 2 * Time.deltaTime;    
            ShowTimer();
        }
        if (example == 6) {
            ShowBoxFacingLine();
            RotateBox(10);
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.position += Vector3.right * 2 * Time.deltaTime;  
            ShowTimer();  
        }
        if (example == 7) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.Translate(Vector3.right * 2 * Time.deltaTime);
            ShowTimer();
        }
        if (example == 8) {
            ShowBoxFacingLine();
            RotateBox(10);
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.Translate(Vector3.right * 2 * Time.deltaTime);
            ShowTimer();
        }
        if (example == 9) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            var distance = heading.magnitude;
            SetDistance(distance);

            box.transform.Translate(Vector3.right * 2 * Time.deltaTime);

            ShowTimer();
            ShowDistance();
        }
        if (example == 10) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            box.transform.Translate(direction * 2 * Time.deltaTime);

            SetDistance(distance);

            ShowTimer();
            ShowDistance();
        }
        if (example == 11) {
            ShowRedBox(2, 2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            box.transform.Translate(direction * 2 * Time.deltaTime);

            SetDistance(distance);

            ShowTimer();
            ShowDistance();
        }
        if (example == 12) {
            ShowRedBox(2, 2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            heading.y = 0;
            var distance = heading.magnitude;
            var direction = heading / distance;
            box.transform.Translate(direction * 2 * Time.deltaTime);

            SetDistance(distance);

            ShowTimer();
            ShowDistance();
        }
    }
    private void SetDistance(float dist) { 
        visualDistance = dist;
    }
    private void ShowDistance() { 
        distanceGroup.active = true;
        distanceText.gameObject.active = true;
        distanceText.text = visualDistance.ToString("f2");
    }
    // Show Timer
    private void ShowTimer() { 
        timerGroup.active = true;
        timerText.gameObject.active = true;
        visualTime = timer;
        timerText.text = visualTime.ToString("f2");
    }

    // Show Destination
    private void ShowRedBox(float x) { 
        redBox.active = true;
        redBox.transform.position = new Vector2(x, redBox.transform.position.y);
    }
    // Show Destination
    private void ShowRedBox(float x, float y) { 
        redBox.active = true;
        redBox.transform.position = new Vector2(x, y);
    }
    // Rotate box
    private void RotateBox(float angle) { 
        box.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void ShowBoxFacingLine() { 
        boxFacingLine.active = true;
    }
    
    private void HideFacingLine() {  
        boxFacingLine.active = false;
    }
}
