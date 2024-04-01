using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : StateBase
{
    [SerializeField] private Transform lastValidTarget;
    
    [Header("Animations")]
    private readonly int adversaryMeteorHash = Animator.StringToHash("MeteorAttack");
    [SerializeField] float adversaryMeteorAnimationDuration = 2f;

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
                if (ai.senseable.allegiance == "Adversary")
                {
                    ai.animator.SetTrigger(adversaryMeteorHash);
                    DOVirtual.DelayedCall(adversaryMeteorAnimationDuration, ai.entityWeapons.Shot);
                    DOVirtual.DelayedCall(adversaryMeteorAnimationDuration+0.5f, ReturnToRoaringState);
                }
                else
                {
                    ai.entityWeapons.Shot();
                }
                    break;
            case Weapon.AttackType.Burst:
                break;
            case Weapon.AttackType.ContinousShot:
                break;
        }
    }

    void ReturnToRoaringState()
    {
        ai.SetRoaring(true);
    }
}
