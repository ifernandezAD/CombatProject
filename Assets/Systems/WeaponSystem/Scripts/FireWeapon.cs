using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
    [SerializeField] AttackType primaryAttackType = AttackType.Shot;
    [SerializeField] AttackType secondaryAttackType = AttackType.None;

    [Header("Debug")]
    [SerializeField] bool debugShot;
    [SerializeField] bool debugContinuousShot;

    protected bool continuousShooting;
    Barrel[] barrels;

    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            Shot();
        }

        if (debugContinuousShot != continuousShooting)
        {
            if (debugContinuousShot)
            {
                StartContinuousShooting();
            }else
            {
                StopContinuousShooting();
            }
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

    public override void StartContinuousShooting()
    {
        continuousShooting = true;
        foreach (Barrel b in barrels) { b.StartContinuousShooting(); }
    }

    public override void StopContinuousShooting()
    {
        continuousShooting = false;
        foreach (Barrel b in barrels) { b.StopContinuousShooting(); }
    }

    public override AttackType GetPrimaryAttackType()
    {
        return primaryAttackType;
    }

    public override AttackType GetSecondaryAttackType()
    {
        return secondaryAttackType;
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
