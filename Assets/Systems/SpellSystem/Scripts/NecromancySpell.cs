using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancySpell : Spell
{
    protected override void SetSpellAnimation()
    {
        animator.SetTrigger("NecromancySpell");
    }

    protected override void BeginSpell()
    {
        //Nothing Yet
    }

    protected override void EndSpell()
    {
        //Nothing yet
    }
}
