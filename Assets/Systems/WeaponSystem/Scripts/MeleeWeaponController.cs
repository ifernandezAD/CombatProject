using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    EntityWeapons entityWeapons;

    private void Awake()
    {
        entityWeapons = GetComponent<EntityWeapons>();
    }

    internal void Attack(string collidersToActivate)
    {
        entityWeapons.GetCurrentWeapon().NotifyMeleeAttack(collidersToActivate);
    }
}
