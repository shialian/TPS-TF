using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager singleton;

    public SupportGun[] gunPrefab;
    private GunPool gunPool = null;

    private void Start()
    {
        singleton = this;
        gunPool = new GunPool(gunPrefab[0]);
    }

    public void CreateGun(Vector3 position, Transform dropBlock)
    {
        int randomIndex = Random.Range(0, gunPrefab.Length);
        SupportGun gun = gunPool.Rent(gunPrefab[randomIndex]);
        gun.transform.SetParent(transform);
        gun.transform.position = position;
        gun.name = gunPrefab[randomIndex].name;
        gun.dropBlock = dropBlock;
    }

    public void ResetGun(SupportGun gun)
    {
        Fighter.singleton.SetBlockAvailable(gun.dropBlock);
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