using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : StateBase
{
    [SerializeField] private Transform lastValidTarget;
    

    private void OnEnable()
    {
        ai.Stop();
        lastValidTarget = ai.target != null ? ai.target.transform : null;
    }

    private void Update()
    {
        UpdateOrientation();
        UpdateMovement();
        UpdateAttack();
    }

    private void UpdateOrientation()
    {
        //Debug.Log($"ai {ai}");
        //Debug.Log($"ai.entityMovement {ai.entityMovement}");
        //Debug.Log($"ai.target {ai.target}");
        //Debug.Log($"ai.target.transform {ai.target.transform}");
        //Debug.Log($"ai.transform {ai.transform}");

        if (ai.target == null)
        {
            ai.entityMovement.Orientate(lastValidTarget.transform.position - ai.transform.position);
        }
        else
        {
            ai.entityMovement.Orientate(ai.target.transform.position - ai.transform.position);
        }
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
