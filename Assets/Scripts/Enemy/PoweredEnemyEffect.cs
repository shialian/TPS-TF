using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredEnemyEffect : MonoBehaviour
{
    public float speedFactor = 10f;

    private void Update()
    {
        transform.Rotate(0f, speedFactor * Time.deltaTime, 0f);
    }
}
