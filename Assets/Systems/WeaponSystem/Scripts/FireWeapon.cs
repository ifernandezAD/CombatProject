using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : Weapon
{
    [SerializeField] AttackType primaryAttackType = AttackType.Shot;
    [SerializeField] AttackType secondaryAttackType = AttackType.None;

    [SerializeField] float primaryAttacksPerSecond = 3f;
    [SerializeField] float secondaryAttacksPerSecond = 1f;

    [SerializeField] AudibleByDuration playerAudible;

    [Header("Debug")]
    [SerializeField] bool debugShot;
    [SerializeField] bool debugContinuousShot;

    protected bool continuousShooting;
    Barrel[] barrels;

    float lastAttackTime;

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

    protected override void InternalAwake()
    {
        base.InternalAwake();
        barrels = GetComponentsInChildren<Barrel>();
    }
    public override void Shot()
    {
        float shotsPerSecond = ChooseShotsPerSecondAccountingAttackType();
        float timeBetweenShots = 1f / shotsPerSecond;
        if ((Time.time - lastAttackTime) > timeBetweenShots)
        {
            foreach (Barrel b in barrels) { b.Shot(); }
            lastAttackTime = Time.time;
        }  
        
        if (playerAudible)
        {
            playerAudible.enabled = true;
            DOVirtual.DelayedCall(0.5f, () => {playerAudible.enabled = false;});       
        }

        PlayWeaponSound();
    }



    private float ChooseShotsPerSecondAccountingAttackType()
    {
        if (primaryAttackType == AttackType.Shot) { return primaryAttacksPerSecond; }
        else if (secondaryAttackType == AttackType.Shot) { return secondaryAttacksPerSecond; }
        else return -1f;
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

    void PlayWeaponSound() { weaponAudioEvent.Play(audioSource); }

}
