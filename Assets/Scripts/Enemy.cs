using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float waypointDetectionRadius = 1f;
    public int damage = 1;
    public float health = 1f;
    public int value = 1;
    public GameObject deathEffect;

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

    void ReachEnd()
    {
        PlayerStats.TakeDamage(damage);
        Destroy(gameObject);
        return;
    }

    void Die()
    {
        PlayerStats.Money += value;
        EffectManager.Spawn(2f, deathEffect, transform.position);
        Destroy(gameObject);
        return;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) Die();
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
            else ReachEnd();
        }

        Move();
    }

    public void OnValidate()
    {
        speed = Mathf.Max(1f, speed);
        waypointDetectionRadius = Mathf.Max(1f, waypointDetectionRadius);
        damage = (int)Mathf.Max(1f, damage);
        health = (int)Mathf.Max(1f, health);
        value = (int)Mathf.Max(0f, value);
    }
}
