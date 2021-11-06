using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public enum Attribute
    {
        General,
        Fire,
        Thunder,
        Poison
    };
    public Attribute attribute;

    private BulletCreator creator;
    private Rigidbody rb;

    public void Initialize(BulletCreator ctr)
    {
        rb = GetComponent<Rigidbody>();
        creator = ctr;
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
                player.SetState(attribute);
                break;
        }
        creator.ResetBullet(this);
    }
}
