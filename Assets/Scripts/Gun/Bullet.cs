using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    private BulletManager manager;
    private Rigidbody rb;

    public void Initialize(BulletManager mgr)
    {
        rb = GetComponent<Rigidbody>();
        manager = mgr;
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
        switch(other.tag)
        {
            case "Enemy":
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.Damaged(damage);
                break;
            case "Player":
                Player player = other.GetComponent<Player>();
                player.Damaged(damage);
                break;
        }
        manager.ResetBullet(this);
    }
}
