using DG.Tweening;
using UnityEngine;

public class IllusionSpell : Spell
{
    [Header("Animations")]
    private readonly int illusionHash = Animator.StringToHash("IllusionSpell");
    private readonly int illusionFinishHash = Animator.StringToHash("IllusionSpellFinish");

    [Header("Skin References")]
    [SerializeField] Transform skinsParent;
    

    [Header("Illusion Spell")]
    [SerializeField] float spellEffectInitialDelay = 1.5f;
    [SerializeField] private float animationDuration = 2f;
    [SerializeField] private float spellAreaRange = 5f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    [SerializeField] private float endAnimationDuration = 4f;
    [SerializeField] private float recoverSkinDelay = 1f;

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
        DOVirtual.DelayedCall(animationDuration, EnablePlayerWeaponController);
    }

    void AffectClosestEntityOnArea()
    {
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
            ChangePlayerSkin(closestAI);
            ChangePlayerAllegiance(closestAI);
            closestAI.SetConfused(true);
        }
    }

    private int playerSkinChildNumber = 7;
    int activeChildIndex = -1;

    void ChangePlayerSkin(AI ai)
    {
        if (ai.senseable.allegiance == "Gangster" || ai.senseable.allegiance == "Civilian")
        {
            Transform aiSkinsParent = ai.transform.GetChild(0).transform.GetChild(0);
           
            foreach (Transform child in aiSkinsParent)
            {
                if (child.gameObject.activeSelf)
                {                 
                    activeChildIndex = child.GetSiblingIndex();
                    break; 
                }
            }

            skinsParent.GetChild(playerSkinChildNumber).gameObject.SetActive(false);
            skinsParent.GetChild(activeChildIndex).gameObject.SetActive(true);
        }
    }

    void RecoverPlayerSkin()
    {
        skinsParent.GetChild(playerSkinChildNumber).gameObject.SetActive(true);
        skinsParent.GetChild(activeChildIndex).gameObject.SetActive(false);
    }

    void ChangePlayerAllegiance(AI ai)
    {
        senseable.allegiance = ai.senseable.allegiance;
    }

    void RecoverPlayerAllegiance()
    {
        senseable.allegiance = "Player";
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

    void SetFinishSpellAnimation()
    {
        animator.SetTrigger(illusionFinishHash);
    }



    protected override void EndSpell()
    {
        SetFinishSpellAnimation();
        DisablePlayerController();
        RequestRemoveWeapon();

        DOVirtual.DelayedCall(recoverSkinDelay, RecoverPlayerSkin);

        DOVirtual.DelayedCall(endAnimationDuration, RequestRecoverWeapon);
        DOVirtual.DelayedCall(endAnimationDuration, EnablePlayerController);
        DOVirtual.DelayedCall(endAnimationDuration, EnablePlayerWeaponController);

        RecoverPlayerAllegiance();
    }
}
