using DG.Tweening;
using System.Collections;
using UnityEngine;


public class AbjurationSpell : Spell
{
    [Header("Animations")]
    private readonly int abjurationHash = Animator.StringToHash("AbjurationSpell");

    [Header("Abjuration Spell")]
    [SerializeField] GameObject magicShield;
    [SerializeField] private float animationDuration = 2f;

    protected override void BeginSpell()
    {
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    protected override void SetSpellAnimation(){ animator.SetTrigger(abjurationHash); }
    
    public void ActivateMagicShield()
    {
        magicShield.SetActive(true);
        entityLife.isInmortal = true;
    }

    public void DeactivateMagicShield() 
    { 
        magicShield.SetActive(false);
        entityLife.isInmortal = false;
    }

    protected override void EndSpell(){DeactivateMagicShield();}
               
}
