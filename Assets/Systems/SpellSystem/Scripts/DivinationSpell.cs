using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivinationSpell : Spell
{
    [SerializeField] GameObject magicArrow;
    private readonly int divinationHash = Animator.StringToHash("DivinationSpell");

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(divinationHash);
    }

    protected override void CastSpell()
    {
        magicArrow.SetActive(true);
        if (PortalManager.instance != null)
        {
            PortalManager.instance.countdownText.enabled = true;
        }
    }

    protected override void EndSpell()
    {
        magicArrow.SetActive(false);
        if (PortalManager.instance != null)
        {
            PortalManager.instance.countdownText.enabled = false;
        }
    }
}

