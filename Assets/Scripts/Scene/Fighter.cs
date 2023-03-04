using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public static Fighter singleton;

    public Vector3 startPosition;
    public Vector3 startRotation;
    public float movingSpeed;
    public int supportCount;

    public Transform bomb;
    public List<Transform> availableBlocks;
    public GameObject fighterSupportIcon;

    private GunManager gunManager;
    private EnemyManager enemyManager;
    private Waypoints waypoints;
    private int waypointIndex = 1;
    private bool support = false;

    private void Start()
    {
        singleton = this;
    }

    public void Initialize(GunManager gunMgr, EnemyManager enyMgr)
    {
        gunManager = gunMgr;
        enemyManager = enyMgr;
        ResetPose();
        GenerateWaypoints();
        SetForward();
        fighterSupportIcon.SetActive(true);
        support = true;
    }

    private void ResetPose()
    {
        transform.position = startPosition;
        transform.rotation =Quaternion.Euler(startRotation);
    }

    private void GenerateWaypoints()
    {
        if (waypoints == null)
        {
            waypoints = GetComponent<Waypoints>();
        }
        waypoints.waypoints = new Transform[supportCount + 1];
        waypoints.waypoints[0] = transform;
        for (int i = 1; i <= supportCount; i++)
        {
            Transform selectblock = availableBlocks[Random.Range(0, availableBlocks.Count)];
            waypoints.waypoints[i] = selectblock;
        }
    }


    private void FixedUpdate()
    {
        if(support)
        {
            MovingTowardWaypoint();
        }
        if (enemyManager.allEnemyDead)
        {
            fighterSupportIcon.SetActive(false);
            support = false;
            gameObject.SetActive(false);
            SoundManager.singleton.Stop();
        }
    }

    private void MovingTowardWaypoint()
    {
        Vector3 towardPosition = waypoints.waypoints[waypointIndex].position;
        towardPosition.y = 20f;
        transform.position = Vector3.MoveTowards(transform.position, towardPosition, movingSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, towardPosition) < 0.1f)
        {
            DeliverGun(waypoints.waypoints[waypointIndex]);
            GoToNextDestination();
        }
    }

    private void GoToNextDestination()
    {
        if (waypointIndex < waypoints.waypoints.Length - 1)
        {
            waypointIndex++;
            SetForward();
        }
        else
        {
            fighterSupportIcon.SetActive(false);
            support = false;
            gameObject.SetActive(false);
            SoundManager.singleton.Stop();
        }
    }
    
    private void SetForward()
    {
        Vector3 towardDirection;
        towardDirection = (waypoints.waypoints[waypointIndex].position - waypoints.waypoints[waypointIndex - 1].position).normalized;
        towardDirection.y = 0f;
        transform.forward = towardDirection;
    }

    private void DeliverGun(Transform dropBlock)
    {
        gunManager.CreateGun(transform.position, dropBlock);
        availableBlocks.Remove(dropBlock);
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

    public void SetBlockAvailable(Transform dropBlock)
    {
        availableBlocks.Add(dropBlock);
    }
}
