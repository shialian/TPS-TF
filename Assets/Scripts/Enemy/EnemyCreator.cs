using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Waypoints waypoints;

    private EnemyPool enemyPool = null;

    private void Start()
    {
        enemyPool = new EnemyPool(enemyPrefab);
    }

    public Enemy CreateEnemy()
    {
        Enemy enemy = enemyPool.Rent();
        enemy.Initialize(waypoints, this);
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
