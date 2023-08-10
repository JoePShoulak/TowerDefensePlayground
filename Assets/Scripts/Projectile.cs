using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;

    public float speed = 70f;
    public float damage = 1;
    public float explosionRadius = 0f;
    public GameObject impactEffectPrefab;

    public void Seek(GameObject _target)
    {
        target = _target;
    }

    void DamageEnemy(GameObject enemyObj)
    {
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        if (enemy == null) return;

        enemy.TakeDamage(damage);
    }

    void Explode()
    {
        List<Collider> hitColliders = new List<Collider>(Physics.OverlapSphere(transform.position, explosionRadius));
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Enemy") DamageEnemy(collider.gameObject);
        }
    }

    void HitTarget()
    {
        if (explosionRadius > 0f) Explode();
        else DamageEnemy(target);

        EffectManager.Spawn(2f, impactEffectPrefab, transform.position);
        Destroy(gameObject);
        return;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) HitTarget();

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target.transform);
    }

    void OnValidate()
    {
        explosionRadius = Mathf.Max(0f, explosionRadius);
        speed = Mathf.Max(0f, speed);
        damage = Mathf.Max(0.1f, damage);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
