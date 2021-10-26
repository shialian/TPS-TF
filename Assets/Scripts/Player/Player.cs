using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Gun holdingGun;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Gun":
                GunAttached(other.transform);
                break;
        }
    }

    private void GunAttached(Transform gun)
    {
        gun.parent = this.transform;
        gun.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
        gun.localRotation = Quaternion.identity;
        holdingGun = gun.GetComponent<Gun>();
    }
}
