using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyParticleCollision : MonoBehaviour,IOffender
{
    Vector3 hitDirection;

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
