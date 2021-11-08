using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform firstActiveEnemy = null;
    public Transform bombVFX;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (firstActiveEnemy != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(firstActiveEnemy.position);
            transform.Rotate(90f, 0f, 0f);
            rb.AddForce(1500f * transform.up);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyManager enemyManager = GameObject.Find("Enemy Pool").GetComponent<EnemyManager>();
        Instantiate(bombVFX, transform.position, transform.rotation);
        enemyManager.GetBomb(transform.position, 8f);
        SoundManager.singleton.SetBomb();
        Destroy(this.gameObject);
    }
}
