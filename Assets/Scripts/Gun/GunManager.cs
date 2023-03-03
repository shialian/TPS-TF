using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Transform[] gunPrefab;
    private GunPool gunPool = null;

    private void Start()
    {
        gunPool = new GunPool(gunPrefab[0]);
    }

    public void CreateGun(Vector3 position)
    {
        int randomIndex = Random.Range(0, gunPrefab.Length);
        Transform gun = gunPool.Rent(gunPrefab[randomIndex]);
        gun.SetParent(transform);
        gun.position = position;
        gun.name = gunPrefab[randomIndex].name;
    }

    IEnumerator ResetBullet(Transform gun, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gunPool.Return(gun);
    }

    public void ResetBullet(Transform gun)
    {
        gunPool.Return(gun);
    }
}

public class GunPool : PrefabPool<Transform>
{
    public GunPool(Transform gun) : base(gun) { }
    protected override void OnBeforeReturn(Transform instance)
    {
        base.OnBeforeReturn(instance);
    }
}