using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private float timer = 0f;
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
            if (timer >= player.holdingGun.colddown)
            {
                RaycastHit hit;
                if(Physics.Raycast(cam.position, cam.forward, out hit, 1000f))
                {
                    player.holdingGun.ShootBullet(hit.point);
                }
                else
                {
                    player.holdingGun.ShootBullet(cam.position + player.holdingGun.force * cam.forward);
                }
                timer = 0f;
            }
        }
    }
}
