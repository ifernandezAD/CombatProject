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
    [SerializeField] private float spellAreaRange = 5f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    protected override void BeginSpell()
    {
        PlayAbjurationSound();
        AffectEntitiesOnArea();
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    protected override void SetSpellAnimation() {animator.SetTrigger(enchantmentHash);}

    void PlayAbjurationSound() { enchantmentAudioEvent.Play(voiceAudioSource); }

    void AffectEntitiesOnArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spellAreaRange, layerMask);
        foreach (Collider c in colliders)
        {
            if (c.gameObject == gameObject)
                continue;

            if (c.TryGetComponent<AI>(out AI ai))
            {
                //Debug.Log($"Collider detected with name {c.transform.parent.name}");
                //Debug.Log($"AI detected from {ai.transform.parent.name}");
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
