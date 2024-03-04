using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] float primaryRange = 0.5f;
    [SerializeField] float secondaryRange = 0.5f;

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

    [SerializeField] public Transform ikGrabPointsParent;


    public abstract AttackType GetPrimaryAttackType();
    internal float GetPrimaryRange() { return primaryRange; }

    public virtual AttackType GetSecondaryAttackType() { return AttackType.None; }
    internal float GetSecondaryRange() { return secondaryRange; }

    public abstract void NotifyMeleeAttack(string collidersToActivate);

    private void Start() { InternalStart(); }

    protected abstract void InternalStart();

    public virtual void Shot() {}
    public virtual void StartContinuousShooting() { }
    public virtual void StopContinuousShooting() { }
}
