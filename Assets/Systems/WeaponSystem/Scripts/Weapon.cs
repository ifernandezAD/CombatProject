using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum AttackType
    {
        None,
        Melee,
        Shot,
        Burst,
        ContinousShot,
    }

    [SerializeField] public RuntimeAnimatorController animatorForWeapon;
    [SerializeField] protected float hitDuration = 0.5f;

    public abstract AttackType GetPrimaryAttackType();
    public virtual AttackType GetSecondaryAttackType() { return AttackType.None; }

    public abstract void NotifyMeleeAttack(string collidersToActivate);

    private void Start() { InternalStart(); }

    protected abstract void InternalStart();

    public virtual void Shot() {}
    public virtual void StartContinuousShooting() { }
    public virtual void StopContinuousShooting() { }

}
