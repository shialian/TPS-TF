using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager singleton;

    public GunCreator[] gunCreators;

    private void Start()
    {
        singleton = this;
    }

    public void CreateGun(Vector3 position, Transform dropBlock)
    {
        int randomIndex = Random.Range(0, gunCreators.Length);
        SupportGun gun = gunCreators[randomIndex].CreateGun(position);
        gun.dropBlock = dropBlock;
    }

    public void ResetGun(SupportGun gun, GunCreator creator)
    {
        Fighter.singleton.SetBlockAvailable(gun.dropBlock);
        creator.ResetGun(gun);
    }
}