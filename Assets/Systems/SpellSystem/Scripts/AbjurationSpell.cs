using System.Collections;
using UnityEngine;

public class AbjurationSpell : Spell
{
    [SerializeField] GameObject magicShield;
    [SerializeField] float magicShieldSpellAnimationDelay = 2f;

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger("ShieldSpell");
    }

    protected override void CastSpell()
    {
        StartCoroutine(DoShieldSpellEffectCorroutine());
    }

    IEnumerator DoShieldSpellEffectCorroutine()
    {
        yield return new WaitForSeconds(magicShieldSpellAnimationDelay);
        magicShield.SetActive(true);
        yield return new WaitForSeconds(spellDuration);
        magicShield.SetActive(false);
    }

    protected override void EndSpell()
    {
        //Nothing yet
    }
}
