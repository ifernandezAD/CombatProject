using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellsController : MonoBehaviour
{
    private void OnEnable()
    {
        SpellWheelButtonController.onIllusionSpellNotified += CastIllusionSpell;
        SpellWheelButtonController.onHypnosisSpellNotified += CastHypnosisSpell;
        SpellWheelButtonController.onNigromancySpellNotified += CastNigromancySpell;
        SpellWheelButtonController.onShieldSpellNotified += CastShieldSpell;
    }

    private void CastIllusionSpell()
    {
        Debug.Log("Illusion Spell Casted");
    }

    private void CastHypnosisSpell()
    {
        Debug.Log("Hypnosis Spell Casted");
    }

    private void CastNigromancySpell()
    {
        Debug.Log("Nigromancy Spell Casted");
    }

    private void CastShieldSpell()
    {
        Debug.Log("Shield Spell Casted");
    }

    private void OnDisable()
    {
        SpellWheelButtonController.onIllusionSpellNotified -= CastIllusionSpell;
        SpellWheelButtonController.onHypnosisSpellNotified -= CastHypnosisSpell;
        SpellWheelButtonController.onNigromancySpellNotified -= CastNigromancySpell;
        SpellWheelButtonController.onShieldSpellNotified -= CastShieldSpell;
    }
}
