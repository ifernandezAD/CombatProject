using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IOffender
{
    [SerializeField] float explosionForce = 1000f;
    [SerializeField] float upwardsModifier = 300f;
    [SerializeField] float range = 10f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    [SerializeField] GameObject visualExplosionPrefab;

    Vector3 hitDirection;

    private void Start()
    {
        if (visualExplosionPrefab){ Instantiate(visualExplosionPrefab, transform.position, Quaternion.identity); }

        Collider[] colliders = Physics.OverlapSphere(transform.position, range, layerMask);
        foreach (Collider c in colliders)
        {
            hitDirection = c.transform.position - transform.position;
            c.GetComponent<HurtCollider>()?.NotifyHit(this);
            c.attachedRigidbody?.AddExplosionForce(explosionForce, transform.position,range,upwardsModifier);
        }

        Destroy(gameObject);
    }

    public Vector3 GetDirection()
    {
        return hitDirection;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
