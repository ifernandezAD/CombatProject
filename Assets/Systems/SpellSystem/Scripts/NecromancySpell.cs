using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class NecromancySpell : Spell
{
    [Header("Animations")]
    private readonly int necromancyHash = Animator.StringToHash("NecromancySpell");

    [Header("Necromancy Spell")]
    [SerializeField] GameObject necromancyVfx;
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] float necromancyVfxDelay = 2f;
    [SerializeField] float necromancySpellDelay = 2f;
    [SerializeField] float animationDuration = 2.5f;

    [SerializeField] private float spellAreaRange = 6;
    [SerializeField] private LayerMask spellLayerMask = Physics.DefaultRaycastLayers;

    protected override void BeginSpell()
    {
        DOVirtual.DelayedCall(necromancyVfxDelay, SetActiveVfx);
        DOVirtual.DelayedCall(necromancySpellDelay, DetectDeathEntitiesOnArea);
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(necromancyHash);
    }

    void SetActiveVfx()
    {
        necromancyVfx.SetActive(true);
    }

    void DetectDeathEntitiesOnArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spellAreaRange, spellLayerMask);
        foreach (Collider c in colliders)
        {
            if (c.gameObject == gameObject)
                continue;

            if (c.TryGetComponent<HumanDeath>(out HumanDeath humanDeath))
            {
                if (humanDeath.isDead)
                {
                    SpawnZombie(humanDeath.transform);
                    Destroy(humanDeath.transform.parent.gameObject);
                }
            }
        }
    }

    private void SpawnZombie(Transform bodyPosition)
    {
        GameObject zombieClone = Instantiate(zombiePrefab, bodyPosition.position, transform.rotation);
    }

    protected override void EndSpell()
    {
        necromancyVfx.SetActive(false);
    }
}
