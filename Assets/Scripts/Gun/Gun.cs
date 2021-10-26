using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletCreator bulletCreator;
    public Transform muzzle;
    public float force = 1000f;

    private Vector3 forceDirection;

    public void ShootBullet()
    {
        Bullet bullet = bulletCreator.CreateBullet();
        bullet.transform.position = muzzle.position;
        forceDirection = transform.forward;
        bullet.Shoot(force * forceDirection);
    }
}
