using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelByParticleSystem : Barrel, IOffender
{
    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule emission;

    Vector3 hitDirection;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        emission = particleSystem.emission;
        emission.enabled = false;
    }

    public override void Shot(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    private void OnParticleCollision(GameObject other)
    {
        hitDirection = other.transform.position - transform.position;
        other.GetComponent<HurtCollider>()?.NotifyHit(this);
    }

    public override void StartContinuousShooting()
    {
        emission.enabled = true;
    }

    public override void StopContinuousShooting()
    {
        emission.enabled = false;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Vector3 GetDirection()
    {
        return hitDirection;
    }


    public IOffender.WeaponType GetWeaponType()
    {
        return IOffender.WeaponType.flamesWeapon;
    }
}
