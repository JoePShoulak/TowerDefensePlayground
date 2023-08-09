using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;

    public float speed = 70f;
    public GameObject impactEffectPrefab;

    public void Seek(GameObject _target)
    {
        target = _target;
    }

    void HitTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffectPrefab, transform.position, transform.rotation);

        Destroy(effect, 2f);
        Destroy(target);
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

        if (dir.magnitude <= distanceThisFrame)
            HitTarget();

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
