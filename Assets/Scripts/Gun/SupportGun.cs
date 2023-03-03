using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportGun : MonoBehaviour
{
    public float rotation;

    private void FixedUpdate()
    {
        transform.Rotate(0, rotation, 0);
    }
}
