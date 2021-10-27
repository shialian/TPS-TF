using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float colddown = 0.1f;
    public float timer = 0f;
    public Transform crosshair;

    private Player player;
    private Transform cam;
    private PlayerLocomotion locomoation;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        locomoation = GetComponent<PlayerLocomotion>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (Input.GetButton("FireOnlyMouse") && player.holdingGun != null)
        {
            locomoation.SetDirectionAndRotation();
            if (timer >= colddown)
            {
                RaycastHit hit;
                if(Physics.Raycast(cam.position, cam.forward, out hit, 1000f))
                {
                    player.holdingGun.ShootBullet(hit.point);
                }
                else
                {
                    player.holdingGun.ShootBullet(cam.position + 10000f * cam.forward);
                }
                timer = 0f;
            }
        }
    }
}
