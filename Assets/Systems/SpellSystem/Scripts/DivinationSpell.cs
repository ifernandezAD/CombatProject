using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DivinationSpell : Spell
{
    [SerializeField] GameObject magicArrow;
    [SerializeField] private float animationDuration = 2f;
    private readonly int divinationHash = Animator.StringToHash("DivinationSpell");

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(divinationHash);
    }

    protected override void BeginSpell()
    {
        entityWeapons.RemoveWeapon();
        magicArrow.SetActive(true);

        if (PortalManager.instance != null){PortalManager.instance.countdownText.enabled = true;}
                    
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
    }

    protected override void EndSpell()
    {
        magicArrow.SetActive(false);

        if (PortalManager.instance != null) {PortalManager.instance.countdownText.enabled = false;}
                          
    }
}

