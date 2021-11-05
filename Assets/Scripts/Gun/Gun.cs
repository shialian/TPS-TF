using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletManager bulletManager;
    public Transform muzzle;
    public float force = 1000f;
    [HideInInspector]
    public float colddown;

    public enum AttackType
    {
        Auto = 0,
        Single = 1,
        Charge = 2
    }

    public enum GunType
    {
        Pistol,
        Assault
    }
    public GunType gunType;
    public AttackType attackType;

    private void Start()
    {
        SetColddown(gunType, attackType);
    }

    private void SetColddown(GunType gunType, AttackType attackType)
    {
        if (gunType == GunType.Assault)
        {
            switch (attackType) {
                case AttackType.Auto:
                    colddown = 0.1f;
                    break;
                case AttackType.Single:
                    colddown = 0.5f;
                    break;
                case AttackType.Charge:
                    colddown = 1f;
                    break;
            }
        }
        else
        {
            switch (attackType)
            {
                case AttackType.Auto:
                    colddown = 0.111f;
                    break;
                case AttackType.Single:
                    colddown = 0.5f;
                    break;
            }
        }
    }

    public void ShootBullet(Vector3 shootingPoint)
    {
        Bullet bullet = bulletManager.CreateBullet();
        Vector3 shootingDirection = (shootingPoint - muzzle.position).normalized;

        SetForward(shootingDirection);

        bullet.transform.position = muzzle.position;
        bullet.Shoot(force * shootingDirection);
    }

    private void SetForward(Vector3 shootingDirection)
    {
        Transform player = transform.GetComponentInParent<Player>().transform;
        Vector3 playerForward = shootingDirection;

        playerForward.y = 0f;
        player.forward = playerForward;
        transform.forward = shootingDirection;
    }
}
