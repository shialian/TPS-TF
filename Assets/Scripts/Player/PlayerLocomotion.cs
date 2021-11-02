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
    public float forwardSpeed;
    public float rightSpeed;
    public bool collideWall = false;

    // Rotation
    public float rotateSpeedFactor = 10.0f;

    // Jumping
    public KeyCode jumpingKey = KeyCode.Space;
    public float jumpingForce = 300f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool overJumping = false;
    public bool isJumping = false;

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
        UpdateJumpingVelocity();
        if (Input.GetKey(jumpingKey))
        {
            Jumping();
        }
        else
        {
            isJumping = false;
        }
    }

    private void UpdateInput()
    {
        forwardSpeed = Input.GetAxis(vertical);
        rightSpeed = Input.GetAxis(horizontal);
    }

    private void UpdateJumpingVelocity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(jumpingKey))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Jumping()
    {
        if (rb.velocity.y < 7 && overJumping == false)
        {
            rb.velocity += jumpingForce * Vector3.up * Time.deltaTime;
        }
        else if(rb.velocity.y >= 7)
        {
            overJumping = true;
        }
        isJumping = true;
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
        if (collideWall)
        {
            movingSpeed = 0f;
        }
    }

    private void SetPosition()
    {
        transform.position += movingSpeedFactor * movingSpeed * Time.fixedDeltaTime * movingDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ray downRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(downRay, out hit, 100f))
        {
            if (Mathf.Abs(hit.distance - 1f) <= 0.1f)
            {
                overJumping = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Ray downRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        collideWall = false;
        if (Physics.Raycast(downRay, out hit, 100f))
        {
            foreach (ContactPoint cp in collision.contacts)
            {
                if (cp.otherCollider.tag == "Plane" && cp.otherCollider.transform.position.y != hit.collider.transform.position.y)
                {
                    rb.velocity =  new Vector3(0f, rb.velocity.y, 0f);
                    if (Vector3.Dot(cp.normal, movingDirection) < 0f)
                    {
                        collideWall = true;
                    }
                    break;
                }
            }
        }
    }
}
