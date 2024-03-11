using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionSpell : Spell
{
    protected override void SetSpellAnimation()
    {
        animator.SetTrigger("IllusionSpell");
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
