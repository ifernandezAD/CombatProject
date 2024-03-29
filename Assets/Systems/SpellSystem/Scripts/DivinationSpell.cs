using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DivinationSpell : Spell
{
    [Header("Animations")]
    private readonly int divinationHash = Animator.StringToHash("DivinationSpell");

    [Header("Divination Spell")]
    [SerializeField] private SimpleAudioEvent divinationAudioEvent;
    [SerializeField] GameObject magicArrow;
    [SerializeField] private float animationDuration = 2f;

    protected override void SetSpellAnimation(){animator.SetTrigger(divinationHash); }
            
    protected override void BeginSpell()
    {
        magicArrow.SetActive(true);

        if (PortalManager.instance != null){PortalManager.instance.countdownText.enabled = true;}
                    
        DOVirtual.DelayedCall(animationDuration, entityWeapons.RecoverWeapon);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerController);
        DOVirtual.DelayedCall(animationDuration, EnablePlayerWeaponController);
    }

    public void PlayDivinationSound() { divinationAudioEvent.Play(voiceAudioSource); }

    protected override void EndSpell()
    {
        magicArrow.SetActive(false);

        if (PortalManager.instance != null) {PortalManager.instance.countdownText.enabled = false;}                       
    }
}

