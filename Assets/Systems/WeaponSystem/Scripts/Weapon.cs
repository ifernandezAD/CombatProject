using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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

    protected AudioSource audioSource;
    [SerializeField] protected SimpleAudioEvent weaponAudioEvent;
    [SerializeField] public RuntimeAnimatorController animatorForWeapon;
    [SerializeField] protected float hitDuration = 0.5f;

    [SerializeField] public Transform ikGrabPointsParent;


    public abstract AttackType GetPrimaryAttackType();
    internal float GetPrimaryRange() { return primaryRange; }

    public virtual AttackType GetSecondaryAttackType() { return AttackType.None; }
    internal float GetSecondaryRange() { return secondaryRange; }

    public abstract void NotifyMeleeAttack(string collidersToActivate);


    private void Awake() { InternalAwake(); }
    protected virtual void InternalAwake() { audioSource = GetComponent<AudioSource>();}

    private void Start() { InternalStart(); }
    protected abstract void InternalStart();

    public virtual void Shot() {}
    public virtual void StartContinuousShooting() { }
    public virtual void StopContinuousShooting() { }

    internal void PlayWeaponSound() { weaponAudioEvent.Play(audioSource); }
}
