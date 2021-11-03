using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private Player player;

    // Camera
    private Transform mainCamera;

    // Running and Walking
    private Vector3 movingDirection;
    public float movingSpeedFactor = 5.0f;
    [HideInInspector]
    public float movingSpeed;
    private float forwardSpeed;
    private float rightSpeed;
    private bool collideWall = false;

    // Rotation
    public float rotateSpeedFactor = 10.0f;

    // Jumping
    public KeyCode jumpingKey = KeyCode.Space;
    public float jumpingForce = 300f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool overJumping = false;

    // Input
    public string vertical = "Vertical";
    public string horizontal = "Horizontal";

    // Rigidbody
    private Rigidbody rb;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        UpdateInput();
        UpdateJumpingVelocity();
        if (Input.GetKey(jumpingKey))
        {
            Jumping();
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
        Ray downRay = new Ray(player.GetRayCastingPosition(), -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(downRay, out hit, 100f, ~(1 << 8)))  // Player的layer是8，不要偵測
        {
            if (Mathf.Abs(hit.distance - 1.1f) <= 0.1f)
            {
                overJumping = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Ray downRay = new Ray(player.GetRayCastingPosition(), -transform.up);
        RaycastHit hit;

        collideWall = false;

        if (Physics.Raycast(downRay, out hit, 100f, ~(1 << 8)))  // Player的layer是8，不要偵測
        {
            foreach (ContactPoint cp in collision.contacts)
            {
                if (cp.otherCollider.tag == "Plane" && cp.otherCollider.transform.position.y != hit.collider.transform.position.y)
                {
                    rb.velocity =  new Vector3(0f, rb.velocity.y, 0f);
                    rb.angularVelocity = Vector3.zero;
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
