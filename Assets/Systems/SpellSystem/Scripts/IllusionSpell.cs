using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IllusionSpell : Spell
{
    [Header("Animations")]
    private readonly int illusionHash = Animator.StringToHash("IllusionSpell");

    [Header("Illusion Spell")]
    [SerializeField] CanvasGroup whiteFlashCanvas;
    [SerializeField] float initialDelay = 1f;
    [SerializeField] float flashDelay = 1f;
    [SerializeField] float flashDuration = 1f;


    [SerializeField] private float animationDuration = 2f;
    [SerializeField] private float spellAreaRange = 5f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    protected override void BeginSpell()
    {
        AffectEntitiesOnArea();
        DOVirtual.DelayedCall(initialDelay, CanvasFade);
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
    }

    void AffectEntitiesOnArea()
    {
        //Comprobar los enemigos en rango
        //Eliges al enemigo más cercano
        //Le activas su estado confuso

        //Tu cambias A SU SKIN (dependerá del tipo de humano) y me cambia la allegiance
        //Al finalizar el hechizo recuperas tu skin y allegiance


       //Collider[] colliders = Physics.OverlapSphere(transform.position, spellAreaRange, layerMask);
       //foreach (Collider c in colliders)
       //{
       //    if (c.gameObject == gameObject)
       //        continue;
       //
       //    if (c.TryGetComponent<AI>(out AI ai))
       //    {
       //        ai.SetEnchanted(true);
       //    }
       //}
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
