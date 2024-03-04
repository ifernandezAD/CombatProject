using DG.Tweening;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    public override AttackType GetPrimaryAttackType()
    {
        return AttackType.Melee;
    }
}
