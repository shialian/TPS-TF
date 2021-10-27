using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float force = 1000f;

    [SerializeField]
    private BulletManager bulletManager;

    private void Start()
    {
        bulletManager = GameObject.Find("Enemy Bullet Pool").GetComponent<BulletManager>();
    }

    public void Fire(Vector3 attackDirection)
    {
        Bullet bullet = bulletManager.CreateBullet();
        bullet.transform.position = transform.position;
        bullet.Shoot(force * attackDirection);
    }
}
