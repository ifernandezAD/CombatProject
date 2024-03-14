using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IllusionSpell : Spell
{
    [Header("Animations")]
    private readonly int illusionHash = Animator.StringToHash("IllusionSpell");

    [Header("Illusion Spell")]
    [SerializeField] private float animationDuration = 2f;
    [SerializeField] private float spellAreaRange = 5f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    protected override void BeginSpell()
    {
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(illusionHash);
    }


    protected override void EndSpell()
    {
        //Nothing yet
    }
}
