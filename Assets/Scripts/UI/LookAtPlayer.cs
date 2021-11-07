using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
    }

    private void Update()
    {
        transform.LookAt(cam);
    }
}
