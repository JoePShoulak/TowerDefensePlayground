using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private int waypointIndex = 0;
    private Transform target;
    private Vector3 targetDelta;

    private Enemy enemy;

    void SetDistanceToTarget()
    {
        enemy.DistanceToTarget = targetDelta.magnitude;
    }

    void SetWaypoint(Transform waypoint)
    {
        enemy.Waypoint = waypointIndex;
        target = waypoint;
    }

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        TargetNextWaypoint();
    }


    void TargetNextWaypoint()
    {
        Transform nextWaypoint = Waypoints.waypoints[waypointIndex++];
        SetWaypoint(nextWaypoint);
    }


    void Update()
    {
        targetDelta = target.position - transform.position;
        SetDistanceToTarget();

        if (targetDelta.magnitude < enemy.waypointDetectionRadius)
        {
            if (waypointIndex < Waypoints.waypoints.Count) TargetNextWaypoint();
            else ReachEnd();
        }

        Move();
    }

    void Move()
    {
        transform.Translate(targetDelta.normalized * enemy.Speed * Time.deltaTime);

        enemy.Speed = enemy.defaultSpeed;
    }

    void ReachEnd()
    {
        Player.TakeDamage(enemy.damage);
        enemy.DestroySelf();
    }

}
