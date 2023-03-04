using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = GameObject.Find("Third Person Camera").transform;
    }

    private void Update()
    {
        transform.LookAt(cam);
    }
}
