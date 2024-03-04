using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelByInstantiation : Barrel
{
    [SerializeField] GameObject prefabToInstantiate;

    public override void Shot(Vector3 direction)
    {
        Instantiate(prefabToInstantiate, transform.position, Quaternion.LookRotation(direction));
    }

    public override void StartContinuousShooting() { }
    public override void StopContinuousShooting() { }
}
