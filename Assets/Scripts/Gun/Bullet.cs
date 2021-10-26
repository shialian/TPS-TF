using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    private Rigidbody rb;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Damaged(damage);
        }
    }
}
