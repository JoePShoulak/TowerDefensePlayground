using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetMode { ClosestToSelf, ClosestToEnd, ClosestToStart };
public enum AttackMethod { Projectile, Laser };

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 15f;
    public AttackMethod method = AttackMethod.Projectile;

    [Header("Use Bullets (default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float turnSpeed = 3f;
    public GameObject projectilePrefab;

    [Header("Use Laser")] // TODO: Make an editor file for this
    public LineRenderer laserBeam;

    public TargetMode targetMode = TargetMode.ClosestToSelf;

    [Header("Misc")]
    public string enemyTag = "Enemy";
    [Range(1, 20)]
    public int updatesPerSecond = 1;
    public Transform aimTransform;
    public Transform muzzle;

    private GameObject target;

    // Helper
    public float DistanceTo(GameObject enemy)
    {
        return (transform.position - enemy.transform.position).magnitude;
    }

    // Interesting Stuff
    public List<GameObject> GetEnemiesInRange()
    {
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag(enemyTag));
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = DistanceTo(enemy);
            if (distanceToEnemy < range) enemiesInRange.Add(enemy);
        }

        return enemiesInRange;
    }

    public void UpdateTarget()
    {
        List<GameObject> enemiesInRange = GetEnemiesInRange();
        switch (targetMode)
        {
            case TargetMode.ClosestToSelf:
                target = Find.ClosestToTransform(enemiesInRange, transform);
                break;
            case TargetMode.ClosestToEnd:
                target = Find.ClosestToEnd(enemiesInRange);
                break;
            case TargetMode.ClosestToStart:
                target = Find.ClosestToStart(enemiesInRange);
                break;
            default:
                target = null;
                break;
        }
    }

    public void AimAtTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion rotationQ = Quaternion.LookRotation(dir);
        Vector3 rotationE = Quaternion.Lerp(aimTransform.rotation, rotationQ, Time.deltaTime * turnSpeed).eulerAngles;
        aimTransform.rotation = Quaternion.Euler(0f, rotationE.y, 0f);
    }

    public void FireAtTarget()
    {
        if (method == AttackMethod.Laser) ActivateLaser();
        else FireProjectile();
    }

    public void ActivateLaser()
    {
        if (!laserBeam.enabled) laserBeam.enabled = true;

        laserBeam.SetPosition(0, muzzle.position);
        laserBeam.SetPosition(1, target.transform.position);
    }

    public void DeactivateLaser()
    {
        laserBeam.enabled = false;
    }

    public void FireProjectile()
    {
        if (fireCountdown <= 0f)
        {
            fireCountdown = 1f / fireRate;

            GameObject projectileObj = (GameObject)Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            Projectile projectile = projectileObj.GetComponent<Projectile>();

            if (projectile == null) return;

            projectile.Seek(target);
        }
    }

    // Main Stuff
    public void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f / (float)updatesPerSecond);
    }

    public void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (target != null)
        {
            AimAtTarget();
            FireAtTarget();
        }
        else if (method == AttackMethod.Laser)
        {
            DeactivateLaser();
        }
    }

    // Boring Stuff
    public void OnValidate()
    {
        range = Mathf.Max(1f, range);
        turnSpeed = Mathf.Max(1f, turnSpeed);
        fireRate = Mathf.Max(0.1f, fireRate);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
