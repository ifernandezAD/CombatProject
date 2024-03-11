using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Spell : MonoBehaviour
{
    [Header("References")]
    protected Animator animator;
    protected EntityLife entityLife;
    protected PlayerController playerController;
    protected EntityWeapons entityWeapons;

    [Header("AudioEvents")]
    [SerializeField] private SimpleAudioEvent spellAudioEvent;
    private AudioSource spellAudioSource;
    protected AudioSource voiceAudioSource;


    [Header("Spells Variables")]
    [SerializeField] protected float spellDuration = 5f;
    [SerializeField] protected int spellDyePower = 1;


    public static event Action<int> spellDyePowerNotified;
    public static event Action endSpellNotified;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        entityLife = GetComponent<EntityLife>();
        entityWeapons = GetComponent<EntityWeapons>();
        playerController = GetComponent<PlayerController>();

        spellAudioSource = GetComponent<AudioSource>();
        voiceAudioSource = GetComponents<AudioSource>()[1];
    }

    private void OnEnable() { InternalOnEnable(); }
    protected virtual void InternalOnEnable()
    {
        InvokeSpellDyePowerEvent();
        DisablePlayerController();
        RequestRemoveWeapon();
        PlaySpellSound();
        SetSpellAnimation();
        BeginSpell();
        DOVirtual.DelayedCall(spellDuration, () => this.enabled = false); ;
    }

    void InvokeSpellDyePowerEvent() { spellDyePowerNotified?.Invoke(spellDyePower); }

    public void EnablePlayerController() { playerController.enabled = true; }
    public void DisablePlayerController() { playerController.enabled = false; }

    public void RequestRemoveWeapon() { entityWeapons.RemoveWeapon(); }
    public void RequestRecoverWeapon() { entityWeapons.RecoverWeapon(); }

    void PlaySpellSound() { spellAudioEvent.Play(spellAudioSource); }

    protected abstract void SetSpellAnimation();
    protected abstract void BeginSpell();
    protected abstract void EndSpell();

    private void OnDisable() { InternalOnDisable(); }
    protected virtual void InternalOnDisable()
    {
        EndSpell();
        endSpellNotified?.Invoke();
    }
}
