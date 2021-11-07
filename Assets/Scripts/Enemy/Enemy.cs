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
    public float damageToCastle = 10f;
    public Transform bombVFX;

    private Waypoints wpoints;
    [SerializeField]
    private int waypointIndex = 1;
    private Vector3 offset;
    private float currentHP;
    private EnemyCreator creator;
    private EnemyAttack attack;
    private int bulletIndex;

    public enum Type
    {
        Normal,
        Powered_Normal,
        Fire,
        Thunder,
        Virus,
        Bomb
    };
    public Type type;

    public void Initialize(Waypoints road, EnemyCreator ctr)
    {
        wpoints = road;
        waypointIndex = 1;
        offset = new Vector3(0f, yOffset, 0f);
        currentHP = maxHP;
        HPBar.value = currentHP;
        creator = ctr;
        transform.position = wpoints.waypoints[0].transform.position + offset;
        switch (type) {
            case Type.Fire:
                attack = GetComponent<EnemyAttack>();
                bulletIndex = 0;
                break;
            case Type.Thunder:
                attack = GetComponent<EnemyAttack>();
                bulletIndex = 1;
                break;
            case Type.Virus:
                attack = GetComponent<EnemyAttack>();
                bulletIndex = 2;
                break;
            default:
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
            creator.ResetEnemy(this);
        }
    }

    public void Damaged(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Instantiate(hitVFX, transform.position, transform.rotation);
            if(type == Type.Bomb)
            {
                EnemyManager enemyManager = GameObject.Find("Enemy Pool").GetComponent<EnemyManager>();
                Instantiate(bombVFX, transform.position, transform.rotation);
                enemyManager.GetBomb(transform.position, 12f);
            }
            creator.ResetEnemy(this);
        }
        HPBar.value = currentHP / maxHP;
    }

    public void Fire(Transform player)
    {
        Vector3 attackDirection = (player.position - transform.position + new Vector3(0f, 1f, 0f)).normalized;
        attack.Fire(attackDirection, bulletIndex);
    }
}
