using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 force)
    {
        rb.AddForce(force);
    }

    public void ResetToInitial()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
