using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float movingSpeed;

    private GunManager gunManager;
    private Waypoints waypoints;
    private int waypointIndex = 1;

    public void Initialize(GunManager gunMgr, Waypoints wPoints)
    {
        gunManager = gunMgr;
        waypoints = wPoints;
        SetForward();
    }

    private void FixedUpdate()
    {
        if(waypoints.waypoints.Length > 0)
        {
            MovingTowardWaypoint();
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
            Destroy(this.gameObject);
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
}
