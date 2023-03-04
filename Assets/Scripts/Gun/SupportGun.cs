using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportGun : MonoBehaviour
{
    public float rotation;
    public Transform dropBlock;

    private GunCreator creator;

    public void Initialize(GunCreator ctr)
    {
        creator = ctr;
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotation, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            GunManager.singleton.ResetGun(this, creator);
        }
    }
}
