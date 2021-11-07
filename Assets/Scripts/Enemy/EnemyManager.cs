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
    public Queue<int> q = new Queue<int>();
    public bool allEnemyDead = true;

    private float timer = 0f;
    private List<GameObject> currentEnemy = new List<GameObject>();
    private int bubbleIndex;
    private int bubbleAmount;

    private void FixedUpdate()
    {
        allEnemyDead = CheckAllDead();
        timer += Time.fixedDeltaTime;
        if (timer >= colddown && bubbleAmount > 0)
        {
            Enemy enemy = enemyCreator[bubbleIndex].CreateEnemy();
            currentEnemy.Add(enemy.gameObject);
            bubbleAmount--;
            timer = 0;
        }
        else if(bubbleAmount == 0 && q.Count != 0)
        {
            GetData();
        }
        else if(bubbleAmount == 0 && q.Count == 0 && allEnemyDead)
        {
            GameManager.singleton.SetRemindTextOn();
        }
    }

    public void BubbleDataIn(int index, int amount)
    {
        q.Enqueue(index);
        q.Enqueue(amount);
    }

    private void GetData()
    {
        bubbleIndex = q.Dequeue();
        bubbleAmount = q.Dequeue();
    }

    public bool CheckAllDead()
    {
        for(int i = 0; i < currentEnemy.Count; i++)
        {
            if (currentEnemy[i].activeSelf == true)
            {
                return false;
            }
        }
        if (currentEnemy.Count > 0)
        {
            GameManager.singleton.bombIcon.SetActive(false);
        }
        currentEnemy.Clear();
        return true;
    }

    public Transform GetFirstActiveEnemyPosition()
    {
        for (int i = 0; i < currentEnemy.Count; i++)
        {
            if (currentEnemy[i].activeSelf == true)
            {
                return currentEnemy[i].transform;
            }
        }
        return null;
    }

    public void GetBomb(Vector3 position, float distance)
    {
        for(int i = 0; i < currentEnemy.Count; i++)
        {
            if(currentEnemy[i].activeSelf && Vector3.Distance(currentEnemy[i].transform.position, position) <= distance)
            {
                Enemy enemy = currentEnemy[i].GetComponent<Enemy>();
                if (enemy.type != Enemy.Type.Bomb)
                {
                    enemy.Damaged(enemy.maxHP);
                }
            }
        }
    }
}

