using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject box;
    public Rigidbody2D boxPhysicsRb;
    public GameObject redBox;
    public GameObject blueBox;
    public GameObject boxFacingLine;
    public GameObject redBoxFacingLine;
    public GameObject emptyCircle;
    public Rigidbody2D bulletPref;
    public Rigidbody2D bulletBouncyPref;

    public SpriteRenderer backgroundSR;
    public GameObject keys;
    public GameObject topKey;
    public GameObject rightKey;
    public GameObject bottomKey;
    public GameObject leftKey;
    public GameObject camera;

    public GameObject physicsBox;
    public Rigidbody2D physicsBoxRb;
    public GameObject physicsRedBox;
    public GameObject physicsGround;
    public GameObject physicsRedPlatform;
    public Rigidbody2D physicsRedPlatformRb;
    
    public GameObject rotationGroup;
    public GameObject distanceGroup;
    public GameObject timerGroup;
    public TMP_Text timerText;
    public TMP_Text distanceText;
    public TMP_Text rotationText;
    public TMP_Text exampleValue;
    private float visualTime = 0.0f;
    private float visualDistance = 0.0f;
    private float timeElapsed = 0.0f;
    private bool start = false;
    private int example = 0;
    private Vector3 boxStartPosition = new Vector3(0,0,0);
    private Vector2 movementDirection;
    public GameObject exampleWrapper;
    private List<GameObject> planets = new List<GameObject>();
    private Vector3 cameraStartPosition;
    public GameObject shootGO;
    public Rigidbody2D shootGORb;
    private float gravity;
    private float physicsTime;

    public LineRenderer boxLineRenderer;
    public PhysicsMaterial2D bulletPhysicsMaterial2D;
    public float bulletForce = 10f;
    private float previousBoxRotation = -1f; 
    public Material spriteDefault;
    public Material dottedMaterial;
    void Start() {
        cameraStartPosition = camera.transform.position;
    }
    public void ResetPosition() {
        SetExample(0);
        planets.Clear();
        foreach (Transform child in exampleWrapper.transform) {
            GameObject.Destroy(child.gameObject);
        }
        HideRedBoxFacingLine();
        HideFacingLine();
        camera.transform.position = cameraStartPosition;
        physicsBox.active = false;
        physicsGround.active = false;
        physicsRedBox.active = false;
        physicsRedBox.transform.position = new Vector2(-5.72f, 0.46f);
        physicsRedPlatform.active = false;
        physicsRedPlatform.transform.position = new Vector2(-5.72f, 0.46f);
        
        box.active = false;
        // Set no rotation to our object
        box.transform.rotation = Quaternion.identity;
        start = false;
        visualDistance = 0;
        distanceGroup.active = false;
        distanceText.gameObject.active = false;
        rotationGroup.active = false;
        rotationText.gameObject.active = false;
        timeElapsed = 0;
        visualTime = 0;
        timerGroup.active = false;
        redBox.active = false;
        timerText.gameObject.active = false;
        box.transform.position = new Vector2(-5.72f, 1.12f);
        physicsBox.transform.position = new Vector2(-5.72f, 1.12f);
        redBox.transform.position = new Vector2(-5.72f, 1.12f);
        keys.active = false;
        
        // 40+
        boxLineRenderer.positionCount = 0;
        boxLineRenderer.material = spriteDefault;
        previousBoxRotation = -1f;
    }
    public void Trigger() { 
        boxStartPosition = box.transform.position;
        start = true;

        // if (example == 1) {
        //     Example1();
        // } else if (example == 2) {
        //     Example2();
        // }
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
    // * Show - Red Box, Distance
    // ** Vector.right
    // Example 10 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // Example 11 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // ** Change Y destination of end target
    // Example 12 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // ** Change Y destination of end target
    // ** Set Y of heading as 0 so we move only X
    // Example 13 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // ** Get starting box position on Trigger
    // ** Move for X Seconds to Destination
    // ** Calculate Speed based on Distance/Time
    // Example 14 = transform.position onUpdate
    // * Show - Red Box
    // ** Direction based on Vector3.right
    // ** MoveTowards ensures we never go beyond the target
    // Example 15 = transform.position onUpdate
    // * Show - Red Box
    // ** Direction based on Vector3.right
    // ** MoveTowards ensures we never go beyond the target
    // ** Vector3.Distance stop Timer
    // Example 16 = transform.position onUpdate
    // * Show - Red Box
    // ** Mathf.Lerp - returns float, change X value to move 
    // Example 17 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // Example 18 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // ** EaseIn - changes the time value
    // Example 19 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // ** EaseOut - changes the time value
    // Example 20 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // ** BounceOut - changes the time value
    // Example 21 = transform.Translate onUpdate
    // * Show - Red Box, Keys
    // ** MovementDirection
    // ** Doesn't Interact with objects
    // Example 22 = FixedUpdate
    // * Show - Red Box Physics, Keys
    // ** MovementDirection AddForce
    // ** Interacts with objects
    // Example 23 = FixedUpdate
    // * Show - Red Box Physics, Keys
    // ** MovementDirection Velocity
    // ** Interacts with objects
    // Example 24 = FixedUpdate
    // * Show - Red Box Physics, Keys
    // ** MovementDirection Velocity
    // ** Interacts with objects
    // ** No accelaration we just add speed
    // Example 25 = FixedUpdate
    // * Show - Red Box Physics, Keys
    // ** MovementDirection MovePosition
    // ** Interacts with objects
    // ** Explanation - https://www.reddit.com/r/Unity3D/comments/ph75yy/rigidbody_moveposition_or_addforce/
    // Example 26 = FixedUpdate
    // * Show - Red Platform Physics, Keys
    // ** MovementDirection MovePosition for Platform, Kinetic platform
    // ** Velocity for Box
    // ** Interacts with objects
    // ** Explanation - https://www.reddit.com/r/Unity3D/comments/ph75yy/rigidbody_moveposition_or_addforce/
    // ** Explanation 2 - https://youtu.be/fcKGqxUuENk?t=1198
    // Quaternion  : https://answers.unity.com/questions/765683/when-to-use-quaternion-vs-euler-angles.html
    // https://forum.unity.com/threads/rotating-a-2d-object.483830/
    // https://www.youtube.com/watch?v=hd1QzLf4ZH8&list=LL&index=1
    // Example 27 = transform.Rotation onUpdate
    // * Show - Red Box
    // ** Quaternion.Euler
    // Example 28 = transform.Rotation onUpdate
    // * Show - Red Box Vertical Animation, ShowRotation
    // ** Quaternion.LookRotation
    // ** Direction - Vector3.Up
    // Example 29 = transform.Rotation onUpdate
    // * Show - Red Box Vertical Animation, ShowRotation
    // ** Quaternion.FromToRotation
    // Example 30 = Angle onUpdate
    // * Show - Red Box, ShowRotation
    // ** Quaternion.AngleAxis
    // Example 31 = RotateAround onUpdate
    // * Show - Red Box, ShowRotation
    // Example 32 = RotateAround onUpdate
    // * Show - Red Box, ShowRotation
    // ** Rotate Clockwise with Vector.back
    // ** RedBox look at object
    // Example 33 = RotateAround onUpdate
    // * Show - Red Box, ShowRotation
    // ** Rotate Clockwise with Vector.back
    // ** RedBox look at object
    // ** Middle Box looks at RedBox 
    // Example 34 = RotateAround onUpdate
    // * Show - Red Box, ShowRotation
    // ** Planets rotation
    // Example 35 = Camera position onUpdate
    // * Show - Keys
    // ** Camera follow player
    // Example 36 = Camera position onUpdate
    // * Show - Keys
    // ** Camera smooth follow player
    // Example 37 = Camera position onUpdate
    // * Show - Keys
    // ** Camera smooth follow player
    // ** Follow after player reaches some point
    // Example 38 = Camera position onUpdate
    // * Show - Keys
    // ** Camera smooth follow player
    // ** Camera bounds
    // Example 39 = Rotate with Keys onUpdate
    // * Show - Keys, Rotation
    // ** Rotate with Keys
    // Example 40 = Rotate and Shoot onUpdate
    // * Show - Keys, Rotation
    // ** Rotate with Keys
    // ** Shoot bullets

    // Directional Vectors
    // Vector3.right     // (1,  0,  0)
    // Vector3.left      // (-1, 0,  0)
    // Vector3.up        // (0,  1,  0)
    // Vector3.down      // (0, -1,  0)
    // Vector3.forward   // (0,  0,  1)
    // Vector3.back      // (0,  0, -1)

    public void SetExample(int value) {
        example = value;
        exampleValue.text = example.ToString();

        var physicsExamples_1 = new List<int> {22,23,24,25};
        var physicsExamples_2 = new List<int> {26};
        var rotationExamples_1 = new List<int> {27,28,29,30};
        if (example >= 0 && example <= 21 || rotationExamples_1.Contains(example)) { 
            ShowBox();
        } else if (physicsExamples_1.Contains(example)) {
            ShowPhysicsBox();
            ShowPhysicsGround();
            ShowRedBoxPhysics(2);
        } else if (physicsExamples_2.Contains(example)) {
            ShowPhysicsBox();
            ShowPhysicsGround();
            ShowRedPlatformPhysics(2);
        } else if (example == 31 || example == 32 || example == 33) {
            ShowBox();
            ShowRedBox(0, 3f);
            BoxScale(0.6f);
            RedBoxScale(0.4f);
            ShowRedBoxFacingLine();
            box.transform.position = new Vector2(0, 1.12f);
        } else if (example == 34) {
            var startDistance = 1.3f;
            var yDistance = 0.9f;
            var sun = CreatePlanet(new Vector3(0, startDistance, 0), 0.8f, new Color32(230, 195, 132, 255));
            var mercury = CreatePlanet(new Vector3(0, startDistance + yDistance, 0), 0.5f, new Color32(114, 113, 105, 255));
            var venus = CreatePlanet(new Vector3(0, startDistance + (yDistance*2), 0), 0.55f, new Color32(255, 195, 93, 98));
            var earth = CreatePlanet(new Vector3(0, startDistance + (yDistance*3), 0), 0.65f, new Color32(152, 187, 108, 255));
            var mars = CreatePlanet(new Vector3(0, startDistance + (yDistance*4), 0), 0.7f, new Color32(255, 160, 102, 255));

            planets.Add(sun);
            planets.Add(mercury);
            planets.Add(venus);
            planets.Add(earth);
            planets.Add(mars);
        } else if (example == 35 || example == 36 || example == 37 || example == 38) {
            ShowPhysicsBox();
            ShowPhysicsGround();
        } else if (example == 39 || example == 40) {
            ShowBox();
            ShowBoxFacingLine();
        } else if (example == 41) {
            ShowBox();
            ShowBoxFacingLine();
            boxLineRenderer.positionCount = 50;
            ShowPhysicsGround();
        } else if (example == 42) {
            ShowBox();
            ShowBoxFacingLine();
            boxLineRenderer.positionCount = 50;
            ShowPhysicsGround();
        } else if (example == 43) {
            ShowBox();
            ShowBoxFacingLine();
            boxLineRenderer.positionCount = 50;
            boxLineRenderer.material = dottedMaterial;
            ShowPhysicsGround();
        }
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
    private void Example1()  { 
        SetXPosition(2);
    }
    private void Example2()  { 
        TranslateX(2);
    }
    void Update() { 
        if (!start) {
            return;
        }
        // 0 - neutral
        // -1 to 1 values
        // Horizontal: Left = -1, Right = 1
        // Vertical: Bottom = -1, Top = 1
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        timeElapsed += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("GetKeyDown");
            // if (example == 23) {
            //     Example_Jump();
            // } else 
            if (example == 40) {
                Example_Shoot();
            } else if (example == 41) {
                Example_Shoot_2();
            } else if (example == 42) {
                Example_Shoot_3_Bouncy();
            } else if (example == 43) {
                Example_Shoot_3_Bouncy();
            }
        }
        // if (example == 3) {
        //     box.transform.position += new Vector3(2 * Time.deltaTime,0);   
        // } else if (example == 4) {
        //     box.transform.position += Vector3.right * 2 * Time.deltaTime;    
        // } else if (example == 5) {
        //     ShowRedBox(2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }
        //     box.transform.position += Vector3.right * 2 * Time.deltaTime;    
        //     ShowTimer();
        // } else if (example == 6) {
        //     ShowRedBox(2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }
        //     box.transform.Translate(Vector3.right * 2 * Time.deltaTime);
        //     ShowTimer();
        // } else if (example == 7) {
        //     ShowBoxFacingLine();
        //     RotateBox(10);
        //     ShowRedBox(2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }
        //     box.transform.position += Vector3.right * 2 * Time.deltaTime;  
        //     ShowTimer();  
        // } else if (example == 8) {
        //     ShowBoxFacingLine();
        //     RotateBox(10);
        //     ShowRedBox(2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }
        //     box.transform.Translate(Vector3.right * 2 * Time.deltaTime);
        //     ShowTimer();
        // } else if (example == 9) {
        //     ShowRedBox(2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }

        //     Vector3 heading = redBox.transform.position - box.transform.position;
        //     var distance = heading.magnitude;
        //     SetDistance(distance);

        //     box.transform.Translate(Vector3.right * 2 * Time.deltaTime);

        //     ShowTimer();
        //     ShowDistance();
        // } else if (example == 10) {
        //     ShowRedBox(2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }

        //     Vector3 heading = redBox.transform.position - box.transform.position;
        //     var distance = heading.magnitude;
        //     var direction = heading.normalized;
        //     box.transform.Translate(direction * 2 * Time.deltaTime);

        //     SetDistance(distance);

        //     ShowTimer();
        //     ShowDistance();
        // } else if (example == 11) {
        //     ShowRedBox(2, 2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }

        //     Vector3 heading = redBox.transform.position - box.transform.position;
        //     var distance = heading.magnitude;
        //     var direction = heading.normalized;
        //     box.transform.Translate(direction * 2 * Time.deltaTime);

        //     SetDistance(distance);

        //     ShowTimer();
        //     ShowDistance();
        // } else if (example == 12) {
        //     ShowRedBox(2, 2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }

        //     Vector3 heading = redBox.transform.position - box.transform.position;
        //     heading.y = 0;
        //     var distance = heading.magnitude;
        //     var direction = heading.normalized;
        //     box.transform.Translate(direction * 2 * Time.deltaTime);

        //     SetDistance(distance);

        //     ShowTimer();
        //     ShowDistance();
        // } else if (example == 13) {
        //     ShowRedBox(2);
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }

        //     Vector3 heading = redBox.transform.position - boxStartPosition;
        //     var distance = heading.magnitude;
        //     var direction = heading.normalized;
        //     var totalTime = 1.5f; // seconds
        //     var speed = distance / totalTime;
        //     box.transform.Translate(direction * speed * Time.deltaTime);
        //     SetDistance(distance);

        //     ShowTimer();
        //     ShowDistance();
        // } else if (example == 14) {
        //     ShowRedBox(2);

        //     var speed = 2; // seconds
        //     var step = speed * Time.deltaTime;
        //     var target = redBox.transform.position;
        //     box.transform.position = Vector2.MoveTowards(box.transform.position, target, step);

        //     ShowTimer();
        // } else if (example == 15) {
        //     ShowRedBox(2);

        //     var speed = 2; // seconds
        //     var step = speed * Time.deltaTime;
        //     var target = redBox.transform.position;
        //     box.transform.position = Vector2.MoveTowards(box.transform.position, target, step);
        //     if (Vector3.Distance(box.transform.position, target) < 0.001f)
        //     {
        //         return;
        //     }

        //     ShowTimer();
        // } else if (example == 16) {
        //     ShowRedBox(2);

        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }

        //     var target = redBox.transform.position;
        //     var totalTime = 1.5f;
        //     var x = Mathf.Lerp(boxStartPosition.x, target.x, timeElapsed / totalTime);
        //     SetXPosition(x);
            
        //     ShowTimer();
        // } else if (example == 17) {
        //     ShowRedBox(2);

        //     var target = redBox.transform.position;
        //     var totalTime = 1.5f;
        //     box.transform.position = Vector3.Lerp(boxStartPosition, target, timeElapsed / totalTime);
            
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }

        //     ShowTimer();
        // } else if (example == 18) {
        //     ShowRedBox(2);

        //     var target = redBox.transform.position;
        //     var totalTime = 1.5f;
        //     var time = timeElapsed / totalTime;
        //     box.transform.position = Vector3.Lerp(boxStartPosition, target, EaseIn(time));
            
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }
            
        //     ShowTimer();
        // } else if (example == 19) {
        //     ShowRedBox(2);

        //     var target = redBox.transform.position;
        //     var totalTime = 1.5f;
        //     var time = timeElapsed / totalTime;
        //     box.transform.position = Vector3.Lerp(boxStartPosition, target, EaseOut(time));
            
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }
        //     ShowTimer();
        // } else if (example == 20) {
        //     ShowRedBox(2);

        //     var target = redBox.transform.position;
        //     var totalTime = 1.5f;
        //     var time = timeElapsed / totalTime;
        //     box.transform.position = Vector3.Lerp(boxStartPosition, target, BounceOut(time));
            
        //     if (box.transform.position.x >= redBox.transform.position.x) {
        //         return;
        //     }
        //     ShowTimer();
        // } else if (example == 21) {
        //     ShowRedBox(2);
        //     ShowKeys();
            
        //     box.transform.Translate(movementDirection * 2 * Time.deltaTime);
        // } else if (example == 27) {
        //     ShowBoxFacingLine();
        //     // rotating the z axis because we are in 2D
        //     var degrees = 30;
        //     box.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
        //     ShowRotation(degrees);
        // } else if (example == 28) {
        //     ShowBoxFacingLine();
        //     MoveRedBoxVertical(2f, 0.1f);

        //     Vector3 vectorToTarget = redBox.transform.position - boxStartPosition;
        //     // When rotated by 90 degress we define our Upwards direction
        //     // At 0 degrees the object points at the right side
        //     Vector3 rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        //     box.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);

        //     ShowRotation(box.transform.rotation.eulerAngles.z);
        // } else if (example == 29) {
        //     ShowBoxFacingLine();
        //     MoveRedBoxVertical(2f, 0.1f);

        //     Vector3 vectorToTarget = redBox.transform.position - boxStartPosition;
        //     // The axis that we specify follows the target
        //     // In our case Vector.right is X axis so we want it to follow the Target
        //     box.transform.rotation = Quaternion.FromToRotation(Vector3.right, vectorToTarget);
        //     ShowRotation(box.transform.rotation.eulerAngles.z);
        // } else if (example == 30) {

        //     var degrees = 30;
        //     box.transform.rotation = Quaternion.AngleAxis(degrees, Vector3.forward);

        //     ShowRotation(box.transform.rotation.eulerAngles.z);
        // } else if (example == 31) {
        //     ShowBoxFacingLine();

        //     var rotationSpeed = 30;
        //     redBox.transform.RotateAround(
        //         box.transform.position, 
        //         Vector3.forward, 
        //         rotationSpeed * Time.deltaTime
        //     );

        //     ShowRotation(redBox.transform.rotation.eulerAngles.z);
        // } else if (example == 32) {
        //     ShowBoxFacingLine();

        //     var rotationSpeed = 30;
        //     Vector3 vectorToTarget = box.transform.position - redBox.transform.position;
        //     Vector3 rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        //     redBox.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
        //     redBox.transform.RotateAround(box.transform.position, Vector3.back, rotationSpeed * Time.deltaTime);

        //     ShowRotation(redBox.transform.rotation.eulerAngles.z);
        // } else if (example == 33) {
        //     ShowBoxFacingLine();

        //     var rotationSpeed = 30;
        //     Vector3 vectorToTarget = box.transform.position - redBox.transform.position;
        //     Vector3 rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        //     redBox.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
        //     box.transform.rotation = Quaternion.LookRotation(Vector3.back, rotateVectorToTarget);
        //     redBox.transform.RotateAround(box.transform.position, Vector3.back, rotationSpeed * Time.deltaTime);

        //     ShowRotation(redBox.transform.rotation.eulerAngles.z);
        // } else if (example == 34) {
        //     OrbitAround(planets[1], planets[0], 40);
        //     OrbitAround(planets[2], planets[0], 50);
        //     OrbitAround(planets[3], planets[0], 80);
        //     OrbitAround(planets[4], planets[0], 20);
        // } else if (example == 39) {
        //     var rotationSpeed = 20;
        //     box.transform.Rotate(Vector3.forward * -movementDirection.x * rotationSpeed * Time.deltaTime);

        //     ShowRotation(box.transform.rotation.eulerAngles.z);
        //     ShowKeys();
        // } else 
        if (example == 40) {
            var rotationSpeed = 20;
            box.transform.Rotate(Vector3.forward * -movementDirection.x * rotationSpeed * Time.deltaTime);

            ShowRotation(box.transform.rotation.eulerAngles.z);
            ShowKeys();
        } 
    }
    void FixedUpdate() { 
        if (!start) {
            return;
        } 
         if (example == 41) {
            var rotationSpeed = 20;
            box.transform.Rotate(Vector3.forward * -movementDirection.x * rotationSpeed * Time.deltaTime);
            
            if (previousBoxRotation != box.transform.rotation.eulerAngles.z) {
                ShowTrajectory();
                previousBoxRotation = box.transform.rotation.eulerAngles.z;
            }

            ShowRotation(box.transform.rotation.eulerAngles.z);
            ShowKeys();
        } else if (example == 42) {
            var rotationSpeed = 20;
            box.transform.Rotate(Vector3.forward * -movementDirection.x * rotationSpeed * Time.deltaTime);
            
            if (previousBoxRotation != box.transform.rotation.eulerAngles.z) {
                ShowTrajectoryBouncy();
                previousBoxRotation = box.transform.rotation.eulerAngles.z;
            }

            ShowRotation(box.transform.rotation.eulerAngles.z);
            ShowKeys();
        } else if (example == 43) {
            var rotationSpeed = 20;
            box.transform.Rotate(Vector3.forward * -movementDirection.x * rotationSpeed * Time.deltaTime);
            
            if (previousBoxRotation != box.transform.rotation.eulerAngles.z) {
                ShowTrajectoryBouncy();
                previousBoxRotation = box.transform.rotation.eulerAngles.z;
            }

            ShowRotation(box.transform.rotation.eulerAngles.z);
            ShowKeys();
        }
    //     else if (example == 22) {
    //         ShowKeys();
    //         var speed = 10f;
    //         boxPhysicsRb.AddForce(movementDirection * speed);
    //     } else if (example == 23) {
    //         ShowKeys();
    //     } else if (example == 24) {
    //         ShowKeys();
            
    //         var speed = 10f;
    //         boxPhysicsRb.velocity = movementDirection * speed;
    //     } else if (example == 25) {
    //         ShowKeys();
            
    //         var speed = 10f;
    //         boxPhysicsRb.MovePosition(
    //             (Vector2)physicsBox.transform.position + 
    //             (movementDirection * speed * Time.deltaTime)
    //         );
    //     } else if (example == 26) {
    //         ShowKeys();
            
    //         var speed = 2f;
    //         float time = Mathf.PingPong(timeElapsed*speed, 1);
    //         Vector3 startPosition = new Vector2(1,0);
    //         Vector3 endPosition = new Vector2(3,0);
    //         Vector3 position = Vector3.Lerp(startPosition, endPosition, time);
    //         physicsRedPlatformRb.MovePosition(position);
            
    //         var boxSpeed = 10f;
    //         boxPhysicsRb.velocity = movementDirection * boxSpeed;
    //    } else if (example == 35) {
    //         ShowKeys();

    //         var speed = 10f;
    //         boxPhysicsRb.MovePosition(
    //             (Vector2)physicsBox.transform.position + 
    //             (movementDirection * speed * Time.deltaTime));
    //         camera.transform.position = new Vector3(
    //             physicsBox.transform.position.x, 
    //             physicsBox.transform.position.y, 0); 
    //     } else if (example == 36) {
    //         ShowKeys();

    //         var speed = 10f;
    //         boxPhysicsRb.MovePosition(
    //             (Vector2)physicsBox.transform.position + 
    //             (movementDirection * speed * Time.deltaTime)
    //         );
    //     } else if (example == 37) {
    //         ShowKeys();

    //         var speed = 10f;
    //         boxPhysicsRb.MovePosition(
    //             (Vector2)physicsBox.transform.position + 
    //             (movementDirection * speed * Time.deltaTime)
    //         );
    //     } else if (example == 38) {
    //         ShowKeys();

    //         var speed = 10f;
    //         boxPhysicsRb.MovePosition((Vector2)physicsBox.transform.position + (movementDirection * speed * Time.deltaTime));
    //     } 
    }
    private void Awake()
    {
        gravity = Physics.gravity.magnitude;
        physicsTime = Time.fixedDeltaTime;
    }
    void LateUpdate()
    {
        // if (example == 36) {
        //     Vector3 velocity = Vector3.zero;
        //     camera.transform.position = Vector3.SmoothDamp(
        //         camera.transform.position, 
        //         boxPhysicsRb.gameObject.transform.position, 
        //         ref velocity, 0.06f
        //     );
        // } else if (example == 37) {
        //     Vector3 velocity = Vector3.zero;
        //     var distance = Vector3.Distance(
        //         camera.transform.position, 
        //         boxPhysicsRb.gameObject.transform.position);
        //     if (distance > 2f) {
        //         camera.transform.position = Vector3.SmoothDamp(
        //             camera.transform.position, 
        //             boxPhysicsRb.gameObject.transform.position, ref velocity, 0.06f);
        //     }
        // } else if (example == 38) {
        //     Vector3 velocity = Vector3.zero;
        //     Vector3 bounds = new Vector3(
        //         Mathf.Clamp(boxPhysicsRb.gameObject.transform.position.x, -4f, 4f),
        //         Mathf.Clamp(boxPhysicsRb.gameObject.transform.position.y, -4f, 4f),
        //         boxPhysicsRb.gameObject.transform.position.z
        //     );
        //     camera.transform.position = Vector3.SmoothDamp(
        //         camera.transform.position, bounds, ref velocity, 0.06f);
        // } 
    }

    public void ShowTrajectory()
    {
        var bullet = Instantiate(bulletPref, shootGO.transform.position, box.transform.rotation);
        bullet.GetComponent<Bullet>().Shoot(bulletForce);

        Physics2D.simulationMode = SimulationMode2D.Script;
        Vector3[] points = new Vector3[boxLineRenderer.positionCount];
        boxLineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            Physics2D.Simulate(Time.fixedDeltaTime);
            points[i] = bullet.transform.position;
        }
        boxLineRenderer.SetPositions(points);

        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        Destroy(bullet.gameObject);
    }
    
    public void ShowTrajectoryBouncy()
    {
        var bullet = Instantiate(bulletBouncyPref, shootGO.transform.position, box.transform.rotation);
        bullet.GetComponent<Bullet>().Shoot(bulletForce);

        Physics2D.simulationMode = SimulationMode2D.Script;
        Vector3[] points = new Vector3[boxLineRenderer.positionCount];
        boxLineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            Physics2D.Simulate(Time.fixedDeltaTime);
            points[i] = bullet.transform.position;
        }
        boxLineRenderer.SetPositions(points);

        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        Destroy(bullet.gameObject);
    }
    // private void ShowTrajectory2(Vector3 shootVelocity) { 
    //  float velocity = Mathf.Sqrt((shootVelocity.x * shootVelocity.x) + (shootVelocity.y * shootVelocity.y));
	// 	float angle = Mathf.Rad2Deg*(Mathf.Atan2(shootVelocity.y , shootVelocity.x));
    //  float pointSpacing = 0;
	// 	for (int i = 0 ; i < boxLineRenderer.positionCount ; i++)
	// 	{
	// 		float dx = velocity * pointSpacing * Mathf.Cos(angle * Mathf.Deg2Rad);
	// 		float dy = velocity * pointSpacing * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * pointSpacing * pointSpacing / 2.0f);
	// 		Vector3 pos = new Vector3(shootGO.transform.position.x + dx , shootGO.transform.position.y + dy);
	// 		boxLineRenderer.SetPosition(i, pos);
	// 		pointSpacing += 0.05f;
	// 	}
    // }
    // private void ShowTrajectory3(Vector3 forceDirection, float drag) { 
    //     float pointSpacing = 2f;
    //     float time = Time.fixedDeltaTime;
    //     Vector3 velocity = forceDirection * time;
    //     Vector3 gravity = Physics.gravity * time * time;
    //     float stepDrag = 1 - drag * time;
    //     Vector3 shootStartPosition = shootGO.transform.position;
    //     for (int i = 0 ; i < boxLineRenderer.positionCount; i++) {
    //         velocity += gravity;
    //         velocity *= stepDrag;
    //         shootStartPosition += velocity;
    //         boxLineRenderer.SetPosition(i, shootStartPosition);
    //     }
    // }
    private void Example_Jump() {
        var amount = 6f;
        physicsBoxRb.AddForce(Vector2.up * amount, ForceMode2D.Impulse);
    }
    private void Example_Shoot() {
        // Part 1
        // Instantiate(bulletPref, shootGO.transform.position, box.transform.rotation);

        // Part 2
        // Changing the code here so the example works
        GameObject bulletGO = (GameObject) Instantiate(bulletPref.gameObject, shootGO.transform.position, box.transform.rotation);
        Bullet bulletComponent = bulletGO.GetComponent<Bullet>();
        bulletComponent.Shoot(bulletForce);
    }
    private void Example_Shoot_2() { 
        GameObject bulletGO = (GameObject) Instantiate(bulletPref.gameObject, shootGO.transform.position, box.transform.rotation);
        Bullet bulletComponent = bulletGO.GetComponent<Bullet>();
        bulletComponent.Shoot(bulletForce);
    }
    private void Example_Shoot_3_Bouncy() { 
        GameObject bulletGO = (GameObject) Instantiate(bulletBouncyPref.gameObject, shootGO.transform.position, box.transform.rotation);
        Bullet bulletComponent = bulletGO.GetComponent<Bullet>();
        bulletComponent.Shoot(bulletForce);
    }
    private void BoxScale(float size) { 
        box.transform.localScale = new Vector3(size,size,size);
    }
    private void RedBoxScale(float size) { 
        redBox.transform.localScale = new Vector3(size,size,size);
    }
    private void SetDistance(float dist) { 
        visualDistance = dist;
    }
    private void ShowRotation(float degrees) { 
        rotationGroup.active = true;
        rotationText.gameObject.active = true;
        rotationText.text = ((int)degrees).ToString();
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
        visualTime = timeElapsed;
        timerText.text = visualTime.ToString("f2");
    }

    private void MoveRedBoxVertical(float start, float end) { 
        var speed = 2f;
        float time = Mathf.PingPong(timeElapsed*speed, 1);
        Vector3 startPosition = new Vector2(0,start);
        Vector3 endPosition = new Vector2(0,end);
        Vector3 position = Vector3.Lerp(startPosition, endPosition, time);
        ShowRedBox(position);
    }
    private void ShowBox() { 
        box.active = true;
    }
    private void ShowPhysicsBox() { 
        physicsBox.active = true;
    }
    private void ShowPhysicsGround() { 
        physicsGround.active = true;
    }
    private void ShowRedPlatformPhysics(float x) { 
        physicsRedPlatform.active = true;
        physicsRedPlatform.transform.position = new Vector2(x, physicsRedBox.transform.position.y);
    }
    private void ShowRedPlatformPhysics(float x, float y) { 
        physicsRedPlatform.active = true;
        physicsRedPlatform.transform.position = new Vector2(x, y);
    }
    private void ShowRedBoxPhysics(float x) { 
        physicsRedBox.active = true;
        physicsRedBox.transform.position = new Vector2(x, physicsRedBox.transform.position.y);
    }
    private void ShowRedBoxPhysics(float x, float y) { 
        physicsRedBox.active = true;
        physicsRedBox.transform.position = new Vector2(x, y);
    }
    // Show Destination
    private void ShowRedBox(Vector3 vector) { 
        redBox.active = true;
        redBox.transform.position = vector;
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

    private void ShowRedBoxFacingLine() { 
        redBoxFacingLine.active = true;
    }
    
    private void HideRedBoxFacingLine() {  
        redBoxFacingLine.active = false;
    }
    // Easing Functions: https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf
    private float EaseIn(float k) {
        return k*k*k;
    }
    private float EaseOut(float k) {
        return 1f + ((k -= 1f)*k*k);
    }
    private float BounceIn (float k) {
        return 1f - BounceOut(1f - k);
    }
    private float BounceOut (float k) {			
        if (k < (1f/2.75f)) {
            return 7.5625f*k*k;				
        }
        else if (k < (2f/2.75f)) {
            return 7.5625f*(k -= (1.5f/2.75f))*k + 0.75f;
        }
        else if (k < (2.5f/2.75f)) {
            return 7.5625f *(k -= (2.25f/2.75f))*k + 0.9375f;
        }
        else {
            return 7.5625f*(k -= (2.625f/2.75f))*k + 0.984375f;
        }
    }
    private float BounceInOut (float k) {
        if (k < 0.5f) return BounceIn(k*2f)*0.5f;
        return BounceOut(k*2f - 1f)*0.5f + 0.5f;
    }

    private void ShowKeys() { 
        keys.active = true;

        bool showTop = (movementDirection.y > 0);
        SetKeyColor(topKey, showTop);
        bool showBottom = (movementDirection.y < 0);
        SetKeyColor(bottomKey, showBottom);
        bool showLeft = (movementDirection.x < 0);
        SetKeyColor(leftKey, showLeft);
        bool showRight = (movementDirection.x > 0);
        SetKeyColor(rightKey, showRight);
    }
    private void SetKeyColor(GameObject keyImage, bool select) { 
        if (select) {
            keyImage.GetComponent<Image>().color = new Color32(152,187,108,255);
        } else {
            keyImage.GetComponent<Image>().color = new Color32(255,255,225,255);
        }
    }

    private void SetSpriteRendererColor(GameObject gameObject, Color32 color) { 
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
    private void ScaleObject(GameObject gameObject,float size) { 
        gameObject.transform.localScale = new Vector3(size, size, size);
    }

    private GameObject CreatePlanet(Vector3 position, float size, Color32 color) { 
        var planet = Instantiate(emptyCircle, position, Quaternion.identity);
        planet.transform.parent = exampleWrapper.transform;
        ScaleObject(planet, size);
        SetSpriteRendererColor(planet, color);
        planet.active = true;
        return planet;
    }

    private void OrbitAround(GameObject planet, GameObject planetToOrbitAround, float rotationSpeed) { 
        Vector3 vectorToTarget = planetToOrbitAround.transform.position - planet.transform.position;
        Vector3 rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        planet.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
        planet.transform.RotateAround(
            planetToOrbitAround.transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
    }
}
