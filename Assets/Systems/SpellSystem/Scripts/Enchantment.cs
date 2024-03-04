using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchantment : Spell
{

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger("HypnosisSpell");
    }

    protected override void CastSpell()
    {
        //Nothing Yet
    }

    protected override void EndSpell()
    {
        //Nothing yet
    }
}
