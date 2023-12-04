using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
    [Header("Debug")]
    [SerializeField] bool debugShot;

    Barrel[] barrels;

    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            Shot();
        }
    }

    private void Awake()
    {
        barrels = GetComponentsInChildren<Barrel>();
    }

    public override void Shot()
    {
        foreach (Barrel b in barrels) { b.Shot();}
    }

    public override AttackType GetPrimaryAttackType()
    {
        return AttackType.Shot;
    }

    public override AttackType GetSecondaryAttackType()
    {
        return AttackType.Melee;
    }

    protected override void InternalStart()
    {
        // Nothing yet
    }

    public override void NotifyMeleeAttack(string collidersToActivate)
    {
        throw new System.NotImplementedException();
    }

}
