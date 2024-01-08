using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunColorController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugSunColorIndicatorUp;

    Light sun;

    [Header("Variables")]
    [SerializeField] int currentSunColorIndicator = 0;
    [SerializeField] int maxSunColorIndicator = 10;
    [SerializeField] float normalSunColorRecoveryTime = 10f;

    private void OnValidate() 
    {
        if (debugSunColorIndicatorUp)
        {
            IncreaseSunColorIndicator();
            debugSunColorIndicatorUp = false;
        }
    }

    private void Awake()
    {
        sun = GetComponent<Light>();
    }

    private void OnEnable()
    {
        SpellWheelButtonController.onIllusionSpellNotified += IncreaseSunColorIndicator;
        SpellWheelButtonController.onHypnosisSpellNotified += IncreaseSunColorIndicator;
        SpellWheelButtonController.onNigromancySpellNotified += IncreaseSunColorIndicator;
        SpellWheelButtonController.onShieldSpellNotified += IncreaseSunColorIndicator;
    }

    private void Start()
    {
        currentSunColorIndicator = 0;
        StartCoroutine(DecreaseSunColorIndicatorByTimeCorroutine());
    }

    private void IncreaseSunColorIndicator()
    {
        if (currentSunColorIndicator >= maxSunColorIndicator) { return; }

        currentSunColorIndicator++;

        DyeSun();
    }

    IEnumerator DecreaseSunColorIndicatorByTimeCorroutine()
    {       
        while (true)
        {
            yield return new WaitForSeconds(normalSunColorRecoveryTime);

            if (currentSunColorIndicator > 0)
            {
                currentSunColorIndicator--;
                DyeSun();
            }
        }
    }

    void DyeSun()
    {
        float t = (float)currentSunColorIndicator / (float)maxSunColorIndicator;
        Color skyColor = Color.Lerp(Color.white, Color.red, t);
        sun.color = skyColor;
    }


    private void OnDisable()
    {
        SpellWheelButtonController.onIllusionSpellNotified -= IncreaseSunColorIndicator;
        SpellWheelButtonController.onHypnosisSpellNotified -= IncreaseSunColorIndicator;
        SpellWheelButtonController.onNigromancySpellNotified -= IncreaseSunColorIndicator;
        SpellWheelButtonController.onShieldSpellNotified -= IncreaseSunColorIndicator;
    }
}
