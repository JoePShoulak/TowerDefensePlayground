using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damageOverTime = 1;
    public float slowAmount = 0.5f;
    public LineRenderer beam;
    public ParticleSystem impactEffect;
    public Light impactLight;

    void OnValidate()
    {
        damageOverTime = (int)Mathf.Max(1, damageOverTime);
    }

    public void AimBeam(Transform muzzle, GameObject target)
    {
        Vector3 dir = muzzle.position - target.transform.position;

        beam.SetPosition(0, muzzle.position);
        beam.SetPosition(1, target.transform.position);
        impactEffect.transform.position = target.transform.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    public void Activate()
    {
        beam.enabled = true;
        impactLight.enabled = true;
        impactEffect.Play();
    }

    public void Fire(Enemy enemy)
    {
        if (!beam.enabled) Activate();

        enemy.TakeDamage(damageOverTime * Time.deltaTime);
        enemy.Slow(slowAmount);
    }

    public void Deactivate()
    {
        beam.enabled = false;
        impactLight.enabled = false;
        impactEffect.Stop();
    }
}
