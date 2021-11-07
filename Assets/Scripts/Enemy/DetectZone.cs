using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    public float colddown = 0.1f;

    private Enemy self;
    private float timer;

    private void Awake()
    {
        timer = colddown;
    }

    private void Start()
    {
        self = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        timer = colddown - Time.fixedDeltaTime * 5f;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player.isDead == false)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= colddown)
                {
                    self.Fire(other.transform);
                    timer = 0;
                }
            }
        }
    }
}
