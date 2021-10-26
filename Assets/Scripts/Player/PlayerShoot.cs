using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Player
{
    public float colddown = 0.1f;
    public float timer = 0f;
    public Transform crosshair;

    private Transform cam;
    private PlayerLocomotion locomoation;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        locomoation = GetComponent<PlayerLocomotion>();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (Input.GetButton("FireOnlyMouse") && holdingGun != null)
        {
            locomoation.SetDirectionAndRotation();
            if (timer >= colddown)
            {
                RaycastHit hit;
                if(Physics.Raycast(cam.position, cam.forward, out hit, 1000f))
                {
                    holdingGun.ShootBullet(hit.point);
                }
                else
                {
                    holdingGun.ShootBullet(cam.position + 10000f * cam.forward);
                }
                timer = 0f;
            }
        }
    }
}
