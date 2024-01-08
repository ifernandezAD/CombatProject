using System;
using System.Collections.Generic;
using UnityEngine;

public class Sight : Sense
{
    [SerializeField] Vector3 sightSize = new Vector3(10f, 10f, 30f);
    [SerializeField] LayerMask sightMask = Physics.DefaultRaycastLayers;

    [Header("Debug")]
    [SerializeField] List<Senseable> debugSenseablesInSight;

    List<Senseable> senseablesInSight = new List<Senseable>();

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * sightSize.z / 2f, sightSize / 2f,
            transform.rotation,
            sightMask);

        senseablesInSight.Clear();
        foreach (Collider c in colliders)
        {
            Senseable senseable = c.GetComponent<Senseable>();
            if (senseable && senseable!=GetMySenseable() && senseable.isVisible)
            {             
                if (HasLineOfSight(this, c))
                {
                senseablesInSight.Add(senseable);
                }
            }
        }

        debugSenseablesInSight = senseablesInSight;
    }

    private bool HasLineOfSight(Sight sight, Collider collider)
    {
        Vector3 direction = collider.transform.position - sight.transform.position;
        bool hasLineOfSight = false;

        if (Physics.Raycast(sight.transform.position, direction, out RaycastHit hit, direction.magnitude, sightMask))
        {
            hasLineOfSight = hit.collider == collider;
        }

        return hasLineOfSight;
    }

    public override Senseable GetSenseable()
    {
        return senseablesInSight.Count > 0 ? senseablesInSight[0]: null;
    }
}
