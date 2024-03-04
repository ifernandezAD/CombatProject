using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudibleByConstantEmission : AudibleBase
{
    [Header("Constant Emission")]
    [SerializeField] float range = 10f;

    protected override void CheckEmit(float timeSinceLastCheck)
    {
        InternalEmit(range);
    }

}
