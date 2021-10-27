using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public Bullet bulletPrefab;
    private BulletPool bulletPool = null;

    private void Start()
    {
        bulletPool = new BulletPool(bulletPrefab);
    }

    public Bullet CreateBullet()
    {
        Bullet bullet = bulletPool.Rent();
        bullet.Initialize(this);
        bullet.transform.SetParent(transform);
        StartCoroutine(ResetBullet(bullet, 2.0f));

        return bullet;
    }

    IEnumerator ResetBullet(Bullet bullet, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        bulletPool.Return(bullet);
    }

    public void ResetBullet(Bullet bullet)
    {
        bulletPool.Return(bullet);
    }
}

public class BulletPool : PrefabPool<Bullet>
{
    public BulletPool(Bullet bullet) : base(bullet) { }
    protected override void OnBeforeReturn(Bullet instance)
    {
        base.OnBeforeReturn(instance);
        instance.ResetToInitial();
    }
}