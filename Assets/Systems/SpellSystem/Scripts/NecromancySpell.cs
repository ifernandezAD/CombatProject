using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NecromancySpell : Spell
{
    [Header("Animations")]
    private readonly int necromancyHash = Animator.StringToHash("NecromancySpell");

    [Header("Necromancy Spell")]
    [SerializeField] float animationDuration = 2.5f;


    protected override void BeginSpell()
    {
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(necromancyHash);
    }

    protected override void EndSpell()
    {
        //Nothing yet
    }
}
