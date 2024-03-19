using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NecromancySpell : Spell
{
    [Header("Animations")]
    private readonly int necromancyHash = Animator.StringToHash("NecromancySpell");

    [Header("Necromancy Spell")]
    [SerializeField] GameObject necromancyVfx;
    [SerializeField] float necromancyVfxDelay = 2f;
    [SerializeField] float animationDuration = 2.5f;



    protected override void BeginSpell()
    {
        DOVirtual.DelayedCall(necromancyVfxDelay, SetActiveVfx);
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(necromancyHash);
    }

    void SetActiveVfx()
    {
        necromancyVfx.SetActive(true);
    }

    protected override void EndSpell()
    {
        necromancyVfx.SetActive(false);
    }
}
