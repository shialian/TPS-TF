using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Player
{
    public float colddown = 0.1f;

    public float timer = 0f;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (Input.GetButton("FireOnlyMouse") && timer >= colddown)
        {
            if (holdingGun != null)
            {
                holdingGun.ShootBullet();
            }
            timer = 0f;
        }
    }
}
