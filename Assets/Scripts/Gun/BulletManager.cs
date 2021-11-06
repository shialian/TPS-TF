using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public BulletCreator[] bulletCreators;

    public Bullet CreateBullet(int index)
    {
        Bullet bullet = bulletCreators[index].CreateBullet();
        return bullet;
    }
}