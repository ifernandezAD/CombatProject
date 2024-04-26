using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDummyBehaviour : MonoBehaviour
{
    private HurtCollider hurtCollider;

    public static Action onHitByMelee;
    public static Action onHitByWeapon;
    public static Action onHitByFire;

    private void Awake()
    {
        hurtCollider = GetComponent<HurtCollider>();
    }

    private void OnEnable()
    {
        hurtCollider.onHitWithOffender.AddListener(CheckAttackType);
    }

    private void CheckAttackType(IOffender offender)
    {
        if (offender.GetWeaponType() == IOffender.WeaponType.meleeWeapon)
        {
            onHitByMelee?.Invoke();
        }

        if (offender.GetWeaponType() == IOffender.WeaponType.fireWeapon)
        {
            onHitByWeapon?.Invoke();
        }

        if (offender.GetWeaponType() == IOffender.WeaponType.flamesWeapon)
        {
            onHitByFire?.Invoke();
        }
    }

    private void OnDisable()
    {
        hurtCollider.onHitWithOffender.RemoveListener(CheckAttackType);
    }
}
