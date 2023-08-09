using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float waypointDetectionRadius = 1f;

    private Transform target;
    private Vector3 targetDelta;
    private int waypointIndex = 0;

    public float DistanceToTarget
    {
        get { return targetDelta.magnitude; }
    }

    public int Waypoint
    {
        get { return waypointIndex; }
    }

    void TargetNextWaypoint()
    {
        target = Waypoints.waypoints[waypointIndex++];
    }

    void Move()
    {
        transform.Translate(targetDelta.normalized * speed * Time.deltaTime);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        TargetNextWaypoint();
    }

    void Update()
    {
        targetDelta = target.position - transform.position;

        if (targetDelta.magnitude < waypointDetectionRadius)
        {
            if (waypointIndex < Waypoints.waypoints.Count) TargetNextWaypoint();
            else Die();
        }

        Move();
    }

    public void OnValidate()
    {
        speed = Mathf.Max(1f, speed);
        waypointDetectionRadius = Mathf.Max(1f, waypointDetectionRadius);
    }
}
