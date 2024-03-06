using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjurationSpell : Spell
{
    private readonly int conjurationHash = Animator.StringToHash("ConjurationSpell");
    private readonly int conjurationFinishHash = Animator.StringToHash("ConjurationSpellFinish");
    [SerializeField] float endAnimationDuration =2f;

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(conjurationHash);
    }

    protected override void CastSpell()
    {
        entityWeapons.RemoveWeapon();
    }

    protected override void EndSpell()
    {
        animator.SetTrigger(conjurationFinishHash);
        DOVirtual.DelayedCall(endAnimationDuration,entityWeapons.RecoverWeapon);       
    }
}
