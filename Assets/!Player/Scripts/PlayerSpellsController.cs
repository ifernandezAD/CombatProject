using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSpellsController : MonoBehaviour
{
    Animator animator;

    [Header("Shield Spell")]
    [SerializeField] GameObject magicShield;
    [SerializeField] float magicShieldSpellAnimationDelay = 1.7f;
    [SerializeField] float magicShieldDuration = 10f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        SpellWheelButtonController.onIllusionSpellNotified += CastIllusionSpell;
        SpellWheelButtonController.onHypnosisSpellNotified += CastHypnosisSpell;
        SpellWheelButtonController.onNigromancySpellNotified += CastNigromancySpell;
        SpellWheelButtonController.onShieldSpellNotified += CastShieldSpell;
    }

    private void CastIllusionSpell()
    {
        animator.SetTrigger("IllusionSpell");
    }

    private void CastHypnosisSpell()
    {
        animator.SetTrigger("HypnosisSpell");
    }

    private void CastNigromancySpell()
    {
        animator.SetTrigger("NecromancySpell");
    }

    private void CastShieldSpell()
    {
        animator.SetTrigger("ShieldSpell");
        StartCoroutine(DoShieldSpellEffectCorroutine());
    }

    IEnumerator DoShieldSpellEffectCorroutine()
    {
        yield return new WaitForSeconds(magicShieldSpellAnimationDelay);
        magicShield.SetActive(true);
        yield return new WaitForSeconds(magicShieldDuration);
        magicShield.SetActive(false);
    }

    private void OnDisable()
    {
        SpellWheelButtonController.onIllusionSpellNotified -= CastIllusionSpell;
        SpellWheelButtonController.onHypnosisSpellNotified -= CastHypnosisSpell;
        SpellWheelButtonController.onNigromancySpellNotified -= CastNigromancySpell;
        SpellWheelButtonController.onShieldSpellNotified -= CastShieldSpell;
    }
}
