using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCreator : MonoBehaviour
{
    public static GunCreator singleton;

    public SupportGun gunPrefab;
    private GunPool gunPool = null;

    private void Start()
    {
        singleton = this;
        gunPool = new GunPool(gunPrefab);
    }

    public SupportGun CreateGun(Vector3 position)
    {
        SupportGun gun = gunPool.Rent();
        gun.transform.SetParent(transform);
        gun.transform.position = position;
        gun.transform.name = gunPrefab.name;
        gun.Initialize(this);
        return gun;
    }

    public void ResetGun(SupportGun gun)
    {
        gunPool.Return(gun);
    }
}

public class GunPool : PrefabPool<SupportGun>
{
    public GunPool(SupportGun gun) : base(gun) { }
    protected override void OnBeforeReturn(SupportGun instance)
    {
        base.OnBeforeReturn(instance);
    }
}