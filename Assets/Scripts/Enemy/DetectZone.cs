using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    public float colddown = 0.1f;

    private Enemy self;
    private float timer = 0;

    private void Start()
    {
        self = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        timer = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
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
