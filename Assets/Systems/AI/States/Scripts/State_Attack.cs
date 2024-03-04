using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : StateBase
{
    private void OnEnable()
    {
        ai.Stop();
    }

    private void Update()
    {
        UpdateOrientation();
        UpdateMovement();
        UpdateAttack();
    }

    private void UpdateOrientation()
    {
        ai.entityMovement.Orientate(ai.target.transform.position - ai.transform.position);
    }

    private void UpdateMovement()
    {
       
    }

    private void UpdateAttack()
    {
        switch (ai.entityWeapons.GetCurrentWeapon().GetPrimaryAttackType())
        {
            case Weapon.AttackType.None:
                break;
            case Weapon.AttackType.Melee:
                ai.entityWeapons.MeleeAttack();
                break;
            case Weapon.AttackType.Shot:
                ai.entityWeapons.Shot();
                break;
            case Weapon.AttackType.Burst:
                break;
            case Weapon.AttackType.ContinousShot:
                break;
        }
    }
}
