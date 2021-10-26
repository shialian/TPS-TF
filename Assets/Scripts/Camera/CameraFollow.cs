using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    public float lookAtDistance = 2.5f;  // 相機從多遠看玩家
    public float scrollSpeed = 1.0f;  // 相機滑近滑遠的速度

    // lookAtDistance的限制範圍
    public float scrollUpperBound = 3.0f;
    public float scrollLowerBound = 1.0f;

    public float yOffset = 1.0f;  // 相機位置比玩家高多少

    // 限制相機由上往下看跟由下往上看的角度
    public float verticalRotationUpperBound = 270.0f;
    public float verticalRotationLowerBound = 90.0f;

    public float rotationSpeedFactor = 5.0f;  // 相機移動的速度
    private float verticalRotation = 160.0f;
    private float horizontalRotation = 90.0f;

    // input
    public string mouseXInput = "Mouse X";
    public string mouseYInput = "Mouse Y";
    public string mouseScrollWheel = "Mouse ScrollWheel";
    private float mouseX;
    private float mouseY;
    private float mouseScroll;

    private Vector3 offset;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0.0f, yOffset, 0.0f);
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        mouseX = Input.GetAxis(mouseXInput);
        mouseY = Input.GetAxis(mouseYInput);
        mouseScroll = Input.GetAxis(mouseScrollWheel);
    }

    void FixedUpdate()
    {
        offset = new Vector3(0.0f, yOffset, 0.0f);
        SetLookAtDistance();
        SetPosition();
        transform.LookAt(player.position + offset);
    }

    private void SetLookAtDistance()
    {
        lookAtDistance -= mouseScroll * scrollSpeed * Time.fixedDeltaTime;
        lookAtDistance = Limit(lookAtDistance, scrollUpperBound, scrollLowerBound);
    }

    private void SetPosition()
    {
        Vector3 newPosition = CalculateNewPosition();
        transform.position = player.position + offset + newPosition;
    }

    private Vector3 CalculateNewPosition()
    {
        verticalRotation += rotationSpeedFactor * mouseY;
        horizontalRotation += rotationSpeedFactor * -mouseX;
        verticalRotation = Limit(verticalRotation, verticalRotationUpperBound, verticalRotationLowerBound);
        float rotationPhi = verticalRotation * Mathf.Deg2Rad;
        float rotationTheta = horizontalRotation * Mathf.Deg2Rad;
        float pointX = lookAtDistance * Mathf.Cos(rotationPhi) * Mathf.Cos(rotationTheta);
        float pointY = lookAtDistance * Mathf.Sin(rotationPhi);
        float pointZ = lookAtDistance * Mathf.Cos(rotationPhi) * Mathf.Sin(rotationTheta);
        Vector3 newPosition = new Vector3(pointX, pointY, pointZ);
        newPosition = RoundVector3(newPosition, -3);
        return newPosition;
    }

    private Vector3 RoundVector3(Vector3 vector, int digit)
    {
        float shift = Mathf.Pow(10, -digit);
        vector.x = Mathf.Round(vector.x * shift) / shift;
        vector.y = Mathf.Round(vector.y * shift) / shift;
        vector.z = Mathf.Round(vector.z * shift) / shift;

        return vector;
    }

    private float Limit(float value, float upperBound, float lowerBound)
    {
        if (value > upperBound)
            value = upperBound;
        else if (value < lowerBound)
            value = lowerBound;
        return value;
    }
}
