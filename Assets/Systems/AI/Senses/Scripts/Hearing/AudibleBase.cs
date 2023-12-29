using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Senseable))]
public abstract class AudibleBase : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    Senseable senseable;

    private void Awake()
    {
        senseable = GetComponent<Senseable>();
        InternalAwake();
    }

    private void Update()
    {
        CheckEmit(Time.deltaTime);
    }

    protected virtual void InternalAwake() { }
    protected virtual void CheckEmit(float timeSinceLastCheck) { }

    protected void InternalEmit(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, layerMask);
        foreach (Collider c in colliders)
        {
            c.GetComponent<Audition>()?.NotifyAudibleInRange(this, senseable);
        }
    }
}
