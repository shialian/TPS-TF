using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public Text healthText;
    public float health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            health -= enemy.damageToCastle;
            health = Mathf.Max(0f, health);
            healthText.text = health.ToString();

            if(health == 0f)
            {
                GameManager.singleton.LoseTheGame();
                Time.timeScale = 0f;
            }
        }
    }
}
