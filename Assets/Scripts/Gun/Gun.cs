using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletManager bulletManager;
    public Transform muzzle;
    public float force = 1000f;

    private Vector3 forceDirection;

    public void ShootBullet(Vector3 shootingPoint)
    {
        Bullet bullet = bulletManager.CreateBullet();
        Vector3 shootingDirection = (shootingPoint - muzzle.position).normalized;
        transform.forward = shootingDirection;
        bullet.transform.position = muzzle.position;
        bullet.Shoot(force * shootingDirection);
    }
}
