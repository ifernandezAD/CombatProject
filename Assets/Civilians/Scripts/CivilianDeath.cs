using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianDeath : HumanDeath
{
    public static event Action<int> penaltyDyePowerNotified;
    [SerializeField] int penaltyDyePower = 1;
    protected override void ManageDeath()
    {
        base.ManageDeath();

        penaltyDyePowerNotified?.Invoke(penaltyDyePower); 
    }
}
