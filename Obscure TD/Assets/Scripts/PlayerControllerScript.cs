﻿/*
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
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;


    // variable control
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode moveForward = KeyCode.W;
    [SerializeField] private KeyCode moveBackward = KeyCode.S;
    [SerializeField] private KeyCode jump = KeyCode.Space;
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
    private const float MAX_Y = 30f;
    private const float MIN_Y = -5f;
    private GameObject pivot;



    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        groundedLayer = 1 << 9;
        cam = Camera.main;
        camTransform = cam.transform;
        pivot = GameObject.FindGameObjectWithTag("pivot");
    }
    

    // This function controls the various ways the player can input control
    private void PlayerInput()
    {
        Movement(isGrounded);
        //StrafeMovement(isGrounded);
        Jump();
        PlayerRotation();
        

    }


    private void PlayerRotation()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, MIN_Y, MAX_Y);
        Quaternion rotation = Quaternion.Euler(0, mouseX, 0);
        transform.rotation = rotation;
    }

    private void CameraRotation()
    {
        Vector3 cameraNewRot = new Vector3(widthOffset, heightOffset, -distanceOffset);
        //Vector3 cameraNewPos = new Vector3(widthOffset, heightOffset, -distanceOffset);
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        cam.transform.position = pivot.transform.position + rotation * cameraNewRot;
        cam.transform.forward = pivot.transform.forward;
        cam.transform.LookAt(pivot.transform);
    }

    private void Movement(bool isGrounded)
    {      
        // cut movement if theyre in air
        if (isGrounded)
        {
            // basic movement
            if (Input.GetKey(moveForward))
            {
                rb.velocity = transform.forward * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(moveLeft))
            {
                rb.velocity = -transform.right * moveSpeed * Time.deltaTime; 
            }
            if (Input.GetKey(moveRight))
            {
                rb.velocity = transform.right * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(moveBackward))
            {
                rb.velocity = -transform.forward * moveSpeed * Time.deltaTime;
            }

        }
    }

    private void Jump()
    {
        // jump
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            //rb.velocity = new Vector3(rb.velocity.x, jumpForce * Time.deltaTime, rb.velocity.z);
            rb.AddForce(new Vector3(0,1,0) * jumpForce);
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

    private void StrafeMovement(bool isGrounded)
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
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerInput();
        isGrounded = CheckGround();
	}

    void LateUpdate()
    {
        CameraRotation();
        pivot.transform.forward = transform.forward;
    }
}
