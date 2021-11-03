using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float movingSpeed;
    public float yOffset;
    public float maxHP = 100f;
    public Slider HPBar;
    public bool playerDetect = false;

    private Waypoints wpoints;
    private int waypointIndex = 1;
    private Vector3 offset;
    private float currentHP;
    private EnemyManager manager;
    //private EnemyAttack attack;

    public void Initialize(Waypoints road, EnemyManager mgr)
    {
        wpoints = road;
        offset = new Vector3(0f, yOffset, 0f);
        currentHP = maxHP;
        HPBar.value = currentHP;
        manager = mgr;
        transform.position = wpoints.waypoints[0].transform.position;
        //attack = GetComponent<EnemyAttack>();
    }

    private void FixedUpdate()
    {
        MovingTowardWaypoint();
    }

    private void MovingTowardWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, wpoints.waypoints[waypointIndex].position + offset, movingSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, wpoints.waypoints[waypointIndex].position + offset) < 0.1f)
        {
            GoToNextDestination();
        }
    }

    private void GoToNextDestination()
    {
        if (waypointIndex < wpoints.waypoints.Length - 1)
        {
            waypointIndex++;
        }
        else
        {
            manager.ResetEnemy(this);
        }
    }

    public void Damaged(float damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            manager.ResetEnemy(this);
        }
        HPBar.value = currentHP / maxHP;
    }

    public void Fire(Transform player)
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        //attack.Fire(attackDirection);
    }
}
