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
    public Transform hitVFX;
    public bool playerDetect = false;
    public Material water = null;

    private Waypoints wpoints;
    private int waypointIndex = 1;
    private Vector3 offset;
    private float currentHP;
    private EnemyManager manager;
    private EnemyAttack attack;
    private int bulletIndex;

    public enum Type
    {
        Normal,
        Powered_Normal,
        Fire,
        Thunder,
        Virus
    };
    public Type type;

    public void Initialize(Waypoints road, EnemyManager mgr)
    {
        wpoints = road;
        offset = new Vector3(0f, yOffset, 0f);
        currentHP = maxHP;
        HPBar.value = currentHP;
        manager = mgr;
        transform.position = wpoints.waypoints[0].transform.position;
        switch (type) {
            case Type.Fire:
                attack = GetComponent<EnemyAttack>();
                GameManager.singleton.poisonous = false;
                bulletIndex = 0;
                break;
            case Type.Thunder:
                attack = GetComponent<EnemyAttack>();
                GameManager.singleton.poisonous = false;
                bulletIndex = 1;
                break;
            case Type.Virus:
                attack = GetComponent<EnemyAttack>();
                GameManager.singleton.poisonous = true;
                bulletIndex = 2;
                break;
            default:
                GameManager.singleton.poisonous = false;
                break;
        }
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
        if (currentHP <= 0)
        {
            Instantiate(hitVFX, transform.position, transform.rotation);
            manager.ResetEnemy(this);
        }
        HPBar.value = currentHP / maxHP;
    }

    public void Fire(Transform player)
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        attackDirection.y = 0f;
        attack.Fire(attackDirection, bulletIndex);
    }
}
