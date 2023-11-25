using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Barrel : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShot;

    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            Shot();
        }
    }

    public abstract void Shot();
}
