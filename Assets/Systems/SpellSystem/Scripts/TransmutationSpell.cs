using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationSpell : Spell
{
    private readonly int transmutationHash = Animator.StringToHash("TransmutationSpell");
    

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(transmutationHash);
    }

    protected override void BeginSpell()
    {
        entityLife.RecoverAllLife();
        entityWeapons.RemoveWeapon();
    }

    protected override void EndSpell()
    {
        entityWeapons.RecoverWeapon();
    }
}
