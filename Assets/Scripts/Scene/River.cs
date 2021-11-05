using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public float damage = 5f;
    public float colddown = 1f;

    private Player player;
    private float timer = 0;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            timer = 0f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            timer += Time.fixedDeltaTime;
            if (GameManager.singleton.poisonous && timer >= colddown)
            {
                player.Damaged(damage);
                timer = 0f;
            }
        }
    }
}
