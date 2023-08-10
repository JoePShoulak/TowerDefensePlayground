using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float defaultSpeed = 10f;
    public float waypointDetectionRadius = 1f;
    [Header("Combat")]
    public float health = 1f;
    public int value = 1;
    public GameObject deathEffect;
    public int damage = 1;

    private float distanceToTarget;
    private int waypoint;
    private float speed;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float DistanceToTarget
    {
        get { return distanceToTarget; }
        set { distanceToTarget = value; }
    }
    public int Waypoint
    {
        get { return waypoint; }
        set { waypoint = value; }
    }

    void Start()
    {
        speed = defaultSpeed;
    }

    void Die()
    {
        Player.Money += value;
        EffectManager.Spawn(2f, deathEffect, transform.position);
        Destroy(gameObject);
        return;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) Die();
    }

    public void Slow(float slowAmount)
    {
        speed = defaultSpeed * (1 - slowAmount);
    }

    public void OnValidate()
    {
        health = (int)Mathf.Max(1f, health);
        value = (int)Mathf.Max(0f, value);
        speed = Mathf.Max(1f, speed);
        waypointDetectionRadius = Mathf.Max(1f, waypointDetectionRadius);
        damage = (int)Mathf.Max(1f, damage);
    }
}
