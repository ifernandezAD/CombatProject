using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EvocationSpell : Spell
{


    [Header("Animations")]
    private readonly int evocationHash = Animator.StringToHash("EvocationSpell");
    

    [Header("Evocation Spell")]
    [SerializeField] float animationDuration = 3.5f;
    [SerializeField] float summonDelay = 2.5f;
    [SerializeField] GameObject summonPrefab;
    private GameObject summonClone;
    [SerializeField] Transform summonInstantationPosition;

    protected override void BeginSpell()
    {
        DOVirtual.DelayedCall(summonDelay, InstantiateDemon);
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    void InstantiateDemon()
    {
       summonClone=Instantiate(summonPrefab, summonInstantationPosition.position, summonInstantationPosition.rotation);
    }

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(evocationHash);
    }

    protected override void EndSpell()
    {
        Destroy(summonClone);
    }
}
