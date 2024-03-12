using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enchantment : Spell
{
    [Header("Animations")]
    private readonly int enchantmentHash = Animator.StringToHash("EnchantmentSpell");

    [Header("Enchantment Spell")]
    [SerializeField] private SimpleAudioEvent enchantmentAudioEvent;
    [SerializeField] private float animationDuration = 4f;
      
    protected override void BeginSpell()
    {
        PlayAbjurationSound();
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    protected override void SetSpellAnimation() {animator.SetTrigger(enchantmentHash);}

    public void PlayAbjurationSound() { enchantmentAudioEvent.Play(voiceAudioSource); }

    protected override void EndSpell()
    {
        //Nothing yet
    }
}
