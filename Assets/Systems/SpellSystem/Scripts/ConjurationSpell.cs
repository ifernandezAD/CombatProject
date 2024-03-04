using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjurationSpell : Spell
{
    private readonly int conjurationHash = Animator.StringToHash("ConjurationSpell");
    private readonly int conjurationFinishHash = Animator.StringToHash("ConjurationSpellFinish");

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(conjurationHash);
    }

    protected override void CastSpell()
    {
        //Nothing Yet
    }

    protected override void EndSpell()
    {
        animator.SetTrigger(conjurationFinishHash);
    }
}
