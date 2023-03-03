using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float movingSpeed;
    public Transform bomb;

    private GunManager gunManager;
    private EnemyManager enemyManager;
    private Waypoints waypoints;
    private int waypointIndex = 1;

    public void Initialize(GunManager gunMgr, Waypoints wPoints, EnemyManager enyMgr)
    {
        gunManager = gunMgr;
        waypoints = wPoints;
        enemyManager = enyMgr;
        SetForward();
    }

    private void FixedUpdate()
    {
        if(waypoints.waypoints.Length > 0)
        {
            MovingTowardWaypoint();
        }
        if (enemyManager.allEnemyDead)
        {
            GameManager.singleton.fighterSupportIcon.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void MovingTowardWaypoint()
    {
        Vector3 towardPosition = waypoints.waypoints[waypointIndex].position;
        towardPosition.y = 20f;
        transform.position = Vector3.MoveTowards(transform.position, towardPosition, movingSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, towardPosition) < 0.1f)
        {
            GoToNextDestination();
        }
    }

    private void GoToNextDestination()
    {
        if (waypointIndex < waypoints.waypoints.Length - 1)
        {
            waypointIndex++;
            SetForward();
            DeliverGun();
        }
        else
        {
            GameManager.singleton.fighterSupportIcon.SetActive(false);
            Destroy(gameObject);
        }
    }
    
    private void SetForward()
    {
        Vector3 towardDirection;
        towardDirection = (waypoints.waypoints[waypointIndex].position - waypoints.waypoints[waypointIndex - 1].position).normalized;
        towardDirection.y = 0f;
        transform.forward = towardDirection;
    }

    private void DeliverGun()
    {
        gunManager.CreateGun(transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && GameManager.singleton.bomb && GameManager.singleton.playerIsDead == false)
        {
            DeliverBomb();
            GameManager.singleton.bombIcon.SetActive(false);
            GameManager.singleton.bomb = false;
        }
    }

    private void DeliverBomb()
    {
        EnemyManager enemyManager = GameObject.Find("Enemy Pool").GetComponent<EnemyManager>();
        Transform firstActiveEnemy = enemyManager.GetFirstActiveEnemyPosition();
        if(firstActiveEnemy != null)
        {
            Vector3 deliverDirection = (firstActiveEnemy.position - transform.position).normalized;
            Transform bombInstant = Instantiate(bomb.transform, transform.position, transform.rotation);
            bombInstant.GetComponent<Bomb>().firstActiveEnemy = firstActiveEnemy;
        }
    }
}
