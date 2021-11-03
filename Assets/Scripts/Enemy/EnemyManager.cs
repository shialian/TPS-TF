using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Waypoints generalRoad;
    public Waypoints waterRoad;
    public float colddown = 0.2f;
    public int number = 5;

    private EnemyPool enemyPool = null;
    private float timer = 0f;

    private void Start()
    {
        enemyPool = new EnemyPool(enemyPrefab);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= colddown && number > 0)
        {
            CreateEnemy();
            number--;
            timer = 0;
        }
    }

    public Enemy CreateEnemy()
    {
        Enemy enemy = enemyPool.Rent();
        enemy.Initialize(generalRoad, this);
        enemy.transform.SetParent(transform);

        return enemy;
    }

    IEnumerator ResetEnemy(Enemy enemy, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        enemyPool.Return(enemy);
    }

    public void ResetEnemy(Enemy enemy)
    {
        enemyPool.Return(enemy);
    }
}

public class EnemyPool : PrefabPool<Enemy>
{
    public EnemyPool(Enemy ememy) : base(ememy) { }
    protected override void OnBeforeReturn(Enemy instance)
    {
        base.OnBeforeReturn(instance);
    }
}