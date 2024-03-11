using DG.Tweening;
using UnityEngine;

public class ConjurationSpell : Spell
{
    [Header("Animations")]
    private readonly int conjurationHash = Animator.StringToHash("ConjurationSpell");
    private readonly int conjurationFinishHash = Animator.StringToHash("ConjurationSpellFinish");


    [Header("Conjuration Spell")]

    [SerializeField] private GameObject cigarPrefab;
    [SerializeField] private GameObject cigarParticle;
    [SerializeField] private SimpleAudioEvent conjurationAudioEvent;
    [SerializeField]private float endAnimationDuration = 5f;

    protected override void BeginSpell()
    {
        
    }

    protected override void SetSpellAnimation(){animator.SetTrigger(conjurationHash);}
              
    public void PlayConjurationSound() { conjurationAudioEvent.Play(voiceAudioSource); }

    public void ShowCigar() { cigarPrefab.SetActive(true); }
    public void HideCigar() { cigarPrefab.SetActive(false); }

    public void ShowCigarParticle() { cigarParticle.SetActive(true); }
    public void HideCigarParticle() { cigarParticle.SetActive(false); }


    protected override void EndSpell()
    {
        animator.SetTrigger(conjurationFinishHash);

        HideCigarParticle();

        DisablePlayerController();
        DOVirtual.DelayedCall(endAnimationDuration, RequestRecoverWeapon);
        DOVirtual.DelayedCall(endAnimationDuration, EnablePlayerController);
    }              
}
