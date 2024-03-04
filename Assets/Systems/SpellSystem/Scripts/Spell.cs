using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Spell : MonoBehaviour
{
    protected Animator animator;
    protected EntityLife entityLife;
    [SerializeField] private SimpleAudioEvent audioEvent;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] protected float spellDuration = 5f;
    [SerializeField] protected int spellDyePower = 1;

    public static event Action<int> spellDyePowerNotified;
    public static event Action endSpellNotified;

    private void Awake() 
    {
        animator = GetComponentInChildren<Animator>();
        entityLife = GetComponent<EntityLife>();
    }
          
    private void OnEnable() { InternalOnEnable(); }
    protected virtual void InternalOnEnable() 
    {
        InvokeSpellDyePowerEvent();
        SetSpellAnimation();
        PlaySpellSound();
        CastSpell();
        DOVirtual.DelayedCall(spellDuration, () => this.enabled = false); ;
    }

    void InvokeSpellDyePowerEvent() { spellDyePowerNotified?.Invoke(spellDyePower); }
    void PlaySpellSound() { audioEvent.Play(audioSource); }

    protected abstract void SetSpellAnimation();
    protected abstract void CastSpell();
    protected abstract void EndSpell();

    private void OnDisable(){InternalOnDisable();}       
    protected virtual void InternalOnDisable()
    {
        EndSpell();
        endSpellNotified?.Invoke();
    }
}
