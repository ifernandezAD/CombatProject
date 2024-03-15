using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IllusionSpell : Spell
{
    [Header("Animations")]
    private readonly int illusionHash = Animator.StringToHash("IllusionSpell");

    [Header("Skin References")]
    [SerializeField] GameObject playerSkin;
    [SerializeField] GameObject gangsterSkin;

    [Header("Illusion Spell")]
    [SerializeField] float spellEffectInitialDelay = 1.5f;
    [SerializeField] private float animationDuration = 2f;
    [SerializeField] private float spellAreaRange = 5f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    [Header("FlashVfx")]
    [SerializeField] CanvasGroup whiteFlashCanvas;
    [SerializeField] float flashInitialDelay = 1f;
    [SerializeField] float flashDelay = 1f;
    [SerializeField] float flashDuration = 1f;


    protected override void BeginSpell()
    {
        DOVirtual.DelayedCall(flashInitialDelay, CanvasFade);
        DOVirtual.DelayedCall(spellEffectInitialDelay, AffectClosestEntityOnArea);
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    void AffectClosestEntityOnArea()
    {
        //Tu cambias A SU SKIN (dependerá del tipo de humano) y me cambia la allegiance
        //Al finalizar el hechizo recuperas tu skin y allegiance

        Collider[] colliders = Physics.OverlapSphere(transform.position, spellAreaRange, layerMask);
        AI closestAI = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider c in colliders)
        {
            if (c.gameObject == gameObject)
                continue;

            if (c.TryGetComponent<AI>(out AI ai))
            {
                float distanceSqr = (ai.transform.position - transform.position).sqrMagnitude;

                if (distanceSqr < closestDistanceSqr)
                {
                    closestAI = ai;
                    closestDistanceSqr = distanceSqr;
                }
            }
        }

        if (closestAI != null)
        {
            closestAI.SetConfused(true);
            ChangePlayerSkin(closestAI);
        }
    }

    void ChangePlayerSkin(AI ai)
    {
        playerSkin.SetActive(false);

        if (ai.senseable.allegiance == "Gangster")
        {
            gangsterSkin.SetActive(true);
        }
    }

    void CanvasFade()
    {  
        whiteFlashCanvas.DOFade(1f, flashDuration)
            .SetDelay(0f)
            .OnComplete(() =>
            {           
                DOVirtual.DelayedCall(flashDelay, () =>
                {               
                    whiteFlashCanvas.DOFade(0f, flashDuration);
                });
            });
    }

    protected override void SetSpellAnimation()
    {
        animator.SetTrigger(illusionHash);
    }


    protected override void EndSpell()
    {
        //Nothing yet
    }
}
