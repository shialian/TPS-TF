using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    // Camera
    private Transform mainCamera;

    // Running and Walking
    public float movingSpeedFactor = 5.0f;  // 每秒最快5公尺，就是100公尺20秒的速度
    private Vector3 movingDirection;
    private float movingSpeed;
    private float forwardSpeed;
    private float rightSpeed;

    // Rotation
    public float rotateSpeedFactor = 10.0f;

    // Jumping
    public KeyCode jumpingKey = KeyCode.Space;
    public float jumpingForce = 1.0f;
    private bool isJumping = false;

    // Input
    public string vertical = "Vertical";
    public string horizontal = "Horizontal";

    // Rigidbody
    private Rigidbody rb;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateInput();
        if (Input.GetKeyDown(jumpingKey))
        {
            Jumping();
        }
    }

    private void UpdateInput()
    {
        forwardSpeed = Input.GetAxis(vertical);
        rightSpeed = Input.GetAxis(horizontal);
    }

    private void Jumping()
    {
        if(isJumping == false)
        {
            rb.AddForce(Vector3.up * jumpingForce);
            isJumping = true;
        }
    }
    
    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        SetDirectionAndRotation();
        SetMovingSpeed();
        SetPosition();
    }

    public void SetDirectionAndRotation()
    {
        if (forwardSpeed != 0 || rightSpeed != 0)
        {
            float playerYaw = Mathf.Atan2(rightSpeed, forwardSpeed) * Mathf.Rad2Deg;
            float cameraYaw = mainCamera.eulerAngles.y;
            Vector3 playerRotation = new Vector3(0.0f, playerYaw + cameraYaw, 0.0f);
            Quaternion quaPlayerRotation = Quaternion.Euler(playerRotation);
            movingDirection = quaPlayerRotation * Vector3.forward;
            movingDirection = RoundVector(movingDirection, -3);
            transform.rotation =  Quaternion.Lerp(transform.rotation, quaPlayerRotation, rotateSpeedFactor * Time.fixedDeltaTime);
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
        CalculateMovingSpeed();
    }

    private void CalculateMovingSpeed()
    {
        movingSpeed = Mathf.Sqrt(Mathf.Pow(forwardSpeed, 2.0f) + Mathf.Pow(rightSpeed, 2.0f));  // 控制在0~1之間
    }

    private void SetPosition()
    {
        transform.position += movingSpeedFactor * movingSpeed * Time.fixedDeltaTime * movingDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }
}
