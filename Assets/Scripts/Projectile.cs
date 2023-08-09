using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffectPrefab;

    public void Seek(GameObject _target)
    {
        target = _target;
    }

    void DamageEnemy(GameObject enemy)
    {
        Destroy(enemy);
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
        GameObject effect = (GameObject)Instantiate(impactEffectPrefab, transform.position, transform.rotation);

        if (explosionRadius > 0f)
        {
            // Damage all in range
            Explode();
        }
        {
            // Damage one enemy
            DamageEnemy(target);
        }

        Destroy(effect, 2f);
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
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
