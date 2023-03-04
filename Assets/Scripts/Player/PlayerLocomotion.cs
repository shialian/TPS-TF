using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public static PlayerLocomotion singleton;

    private Player player;

    // Camera
    private Transform mainCamera;

    // Running and Walking
    private Vector3 movingDirection;
    public float movingSpeedFactor = 5.0f;
    [HideInInspector]
    public float movingSpeed;
    [HideInInspector]
    public float forwardSpeed;
    [HideInInspector]
    public float rightSpeed;
    private bool collideWall = false;

    // Rotation
    public float rotateSpeedFactor = 10.0f;

    // Jumping
    public KeyCode jumpingKey = KeyCode.Space;
    public float jumpingForce = 300f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [HideInInspector]
    public bool isJumping = false;

    // Jump Refactoring
    public float maxSpeed = 10f;
    public float maxAcceleration = 10f;
    public float jumpHeight = 2.5f;
    [HideInInspector]
    public bool onGround;
    private bool desiredJump;

    // Input
    public string vertical = "Vertical";
    public string horizontal = "Horizontal";

    // Rigidbody
    private Rigidbody rb;

    // Animator
    private PlayerAnimation anim;

    private void Start()
    {
        singleton = this;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        anim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        forwardSpeed = Input.GetAxis(vertical);
        rightSpeed = Input.GetAxis(horizontal);
        desiredJump |= Input.GetKeyDown(jumpingKey);
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        onGround = false;
    }

    private void UpdatePosition()
    {
        SetDirectionAndRotation();
        SetMovingSpeed();
        SetPosition();
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
    }

    public void SetDirectionAndRotation()
    {
        if (forwardSpeed != 0 || rightSpeed != 0)
        {
            float playerYaw = Mathf.Atan2(rightSpeed, forwardSpeed) * Mathf.Rad2Deg;
            float cameraYaw = mainCamera.eulerAngles.y;
            Vector3 playerRotation = new Vector3(0.0f, playerYaw + cameraYaw, 0.0f);
            Quaternion quaPlayerRotation = Quaternion.Euler(playerRotation);
            Quaternion cameraRotation = Quaternion.Euler(new Vector3(0f, cameraYaw, 0f));
            movingDirection = quaPlayerRotation * Vector3.forward;
            movingDirection = RoundVector(movingDirection, -3);
            transform.rotation =  Quaternion.Lerp(transform.rotation, cameraRotation, rotateSpeedFactor * Time.fixedDeltaTime);
        }
    }

    private Vector3 RoundVector(Vector3 vector, int digit)
    {
        float shift = Mathf.Pow(10, -digit);
        vector.x = Mathf.Round(vector.x * shift) / shift;
        vector.y = Mathf.Round(vector.y * shift) / shift;
        vector.z = Mathf.Round(vector.z * shift) / shift;
        return vector;
    }

    private void SetMovingSpeed()
    {
        movingSpeed = Mathf.Sqrt(Mathf.Pow(forwardSpeed, 2.0f) + Mathf.Pow(rightSpeed, 2.0f));
        if (collideWall)
        {
            movingSpeed = 0f;
        }
    }

    private void SetPosition()
    {
        transform.position += movingSpeedFactor * movingSpeed * Time.fixedDeltaTime * movingDirection;
    }

    private void Jump()
    {
        if (onGround)
        {
            Vector3 velocity = rb.velocity;
            velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollisionType(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollisionType(collision);
    }

    private void EvaluateCollisionType(Collision collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
}
