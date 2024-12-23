using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Enchantment : Spell
{
    [Header("Animations")]
    private readonly int enchantmentHash = Animator.StringToHash("EnchantmentSpell");

    [Header("Enchantment Spell")]
    [SerializeField] Transform enchantmentVfxParent;
    [SerializeField] private SimpleAudioEvent enchantmentAudioEvent;
    [SerializeField] private float animationDuration = 4f;
    [SerializeField] private float spellAreaRange = 5f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    protected override void BeginSpell()
    {
        PlayEnchantmentSound();
        PlayEnchantmentVfx();
        AffectEntitiesOnArea();
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerWeaponController);
    }


    protected override void SetSpellAnimation() {animator.SetTrigger(enchantmentHash);}

    void PlayEnchantmentSound() { enchantmentAudioEvent.Play(voiceAudioSource); }

    private void PlayEnchantmentVfx()
    {
        foreach (Transform child in enchantmentVfxParent.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    void AffectEntitiesOnArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spellAreaRange, layerMask);
        foreach (Collider c in colliders)
        {
            if (c.gameObject == gameObject)
                continue;

            if (c.TryGetComponent<AI>(out AI ai))
            {
                ai.SetEnchanted(true);           
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, spellAreaRange);
    }

    protected override void EndSpell()
    {
        //Nothing yet
    }
}
