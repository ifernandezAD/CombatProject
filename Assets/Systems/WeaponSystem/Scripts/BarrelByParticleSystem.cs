using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelByParticleSystem : Barrel, IOffender
{
    ParticleSystem particleSystem;

    Vector3 hitDirection;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
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

    public Transform GetTransform()
    {
        return transform;
    }

    public Vector3 GetDirection()
    {
        return hitDirection;
    }
}
