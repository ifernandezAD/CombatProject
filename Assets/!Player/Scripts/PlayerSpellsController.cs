using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellsController : MonoBehaviour
{
    Animator animator;

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
    }

    private void OnDisable()
    {
        SpellWheelButtonController.onIllusionSpellNotified -= CastIllusionSpell;
        SpellWheelButtonController.onHypnosisSpellNotified -= CastHypnosisSpell;
        SpellWheelButtonController.onNigromancySpellNotified -= CastNigromancySpell;
        SpellWheelButtonController.onShieldSpellNotified -= CastShieldSpell;
    }
}
