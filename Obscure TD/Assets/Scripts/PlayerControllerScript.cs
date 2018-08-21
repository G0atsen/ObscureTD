/*
    This is the player controller
    It contains all you'd expect to move the player
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerScript : MonoBehaviour
{
    private Rigidbody rb;

    // move, jump stuff
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float runSpeed = 20f;
    [SerializeField] private float jumpForce = 5.0f;
    private float intialMoveSpeed;


    // variable control
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode moveForward = KeyCode.W;
    [SerializeField] private KeyCode moveBackward = KeyCode.S;
    [SerializeField] private KeyCode jump = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    private Vector3 moveDirection;
    private float moveHorizontal;
    private float moveVertical;

    // raycast for ground stuff
    private int groundedLayer;
    [SerializeField] private float groundRayLength = 0.7f; //length of ray going to ground
    [SerializeField] private bool isGrounded = false;

    // third person camera stuff
    [SerializeField] private float distanceOffset = 5.0f;
    [SerializeField] private float heightOffset = 5.0f;
    [SerializeField] private float widthOffset = 5.0f;
    private float mouseX = 0.0f;
    private float mouseY = 0.0f;
    //private float rbY = 0.0f;
    private float camY;
    private Camera cam;
    private Transform camTransform;
    [SerializeField] private float MAX_Y = 30f;
    [SerializeField] private float MIN_Y = -5f;
    private GameObject pivot;

    // UI visualisation things
    [SerializeField] private float cameraRayLength = 1f;
    [SerializeField] private GameObject crosshair;

    // Beginning the implementation of the objects
    [SerializeField] private bool canBuild;
    private itemPlacer ip;



    // Use this for initialization
    void Start ()
    {
        canBuild = true;
        intialMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        groundedLayer = 1 << 9;
        cam = Camera.main;
        camTransform = cam.transform;
        pivot = GameObject.FindGameObjectWithTag("pivot");
        ip = FindObjectOfType<itemPlacer>();
        crosshair = GameObject.Find("Cross Hair");
    }
    

    // This function controls the various ways the player can input control
    private void PlayerInput()
    {
        Movement(isGrounded);
        Jump();
        PlayerRotation();
        Build(canBuild);
        Cursor.visible = false;
        

    }


    private void PlayerRotation()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, MIN_Y, MAX_Y);
        Quaternion rotation = Quaternion.Euler(0, mouseX, 0);
        transform.rotation = rotation;
    }

    // Needs commenting and cleaning up*
    private void CameraRotation()
    {
        Vector3 cameraNewRot = new Vector3(widthOffset, heightOffset, -distanceOffset);
        //Vector3 cameraNewPos = new Vector3(widthOffset, heightOffset, -distanceOffset);
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        cam.transform.position = pivot.transform.position + rotation * cameraNewRot;
        cam.transform.forward = pivot.transform.forward;
        cam.transform.LookAt(pivot.transform);
        pivot.transform.forward = transform.forward;

        //
        Debug.DrawRay(cam.transform.position, cam.transform.forward * cameraRayLength, Color.red);

    }

    private void Movement(bool isGrounded)
    {      
        // cut movement if theyre in air
        if (isGrounded)
        {
            // basic movement
            if (Input.GetKey(moveForward))
            {
                rb.velocity += transform.forward * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(moveLeft))
            {
                rb.velocity += -transform.right * moveSpeed * Time.deltaTime; 
            }
            if (Input.GetKey(moveRight))
            {
                rb.velocity += transform.right * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(moveBackward))
            {
                rb.velocity += -transform.forward * moveSpeed * Time.deltaTime;
            }

            // running

            if (Input.GetKey(moveForward) && Input.GetKey(sprintKey))
            {
                moveSpeed = runSpeed;
            }
            else
            {
                moveSpeed = intialMoveSpeed;
            }

        }
    }

    private void Jump()
    {
        // jump
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce * Time.deltaTime, rb.velocity.z);
            //rb.AddForce(new Vector3(0,1,0) * jumpForce);
            Debug.Log("Jumped");
        }
        
    }

    private bool CheckGround()
    {
        Debug.DrawRay(transform.position - new Vector3(0, 0.5f, 0), -transform.up * groundRayLength, Color.cyan);
        if(Physics.Raycast(transform.position - new Vector3 (0,0.5f,0) , -transform.up, groundRayLength, groundedLayer))
        {
            return true;
        }
        return false;
    }

    private void ToggleCrossHair(bool inRange)
    {
        if (inRange)
        {
            crosshair.SetActive(inRange);
        }
        else
        {
            crosshair.SetActive(inRange);
        }
    }

    private void Build(bool canBuild)
    {
        if (canBuild)
        {
            RaycastHit hitInfo;

            print("click");
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hitInfo, cameraRayLength, groundedLayer))
            {
                ToggleCrossHair(true);
                if (Input.GetMouseButtonDown(0))
                {
                    ip.PlaceCubeNear(hitInfo.point);
                    print(hitInfo.point);
                    ip.tilesCreated += 1;
                }
            }
            else
            {
                ToggleCrossHair(false);
            }
            
        }
    }
    /// <summary>
    /// Legacy function, entirely obsolete keeping it for what NOT to do
    /// </summary>
    /// <param name="isGrounded"></param>
    /*private void StrafeMovement(bool isGrounded)
    {   
        // cut movement if theyre in air
        if (isGrounded)
        {
            // allows for strafe
            if (Input.GetKey(moveLeft) && Input.GetKey(moveForward))
            {
                rb.velocity = new Vector3(-1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime, rb.velocity.y, 1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime);
            }
            if (Input.GetKey(moveRight) && Input.GetKey(moveForward))
            {
                rb.velocity = new Vector3(1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime, rb.velocity.y, 1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime);
            }
            if (Input.GetKey(moveLeft) && Input.GetKey(moveBackward))
            {
                rb.velocity = new Vector3(-1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime, rb.velocity.y, -1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime);
            }
            if (Input.GetKey(moveRight) && Input.GetKey(moveBackward))
            {
                rb.velocity = new Vector3(1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime, rb.velocity.y, -1 * (moveSpeed - moveSpeed / 2) * Time.deltaTime);
            }
        }
    }*/
	
	// Update is called once per frame
	void Update ()
    {
        PlayerInput();
        isGrounded = CheckGround();
        // quick test in build
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    void LateUpdate()
    {
        CameraRotation();
        
    }
}
