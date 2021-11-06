using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Waypoints generalRoad;
    public Waypoints waterRoad;
    public EnemyCreator[] enemyCreator;
    public float colddown = 0.2f;
    public int number = 5;

    private float timer = 0f;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= colddown && number > 0)
        {
            if (number < 6)
            {
                enemyCreator[0].CreateEnemy();
            }
            else
            {
                enemyCreator[2].CreateEnemy();
            }
            number--;
            timer = 0;
        }
    }
}

