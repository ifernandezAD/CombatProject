using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

[Obsolete("RedSkyController")]
public class SunColorController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugIncreaseSunColorIndicator;
    [SerializeField] bool debugResetSunColorIndicator;

    Light sun;

    [Header("Variables")]
    [SerializeField] int currentSunColorIndicator = 0;
    [SerializeField] int maxSunColorIndicator = 10;
    [SerializeField] float normalSunColorRecoveryTime = 10f;

    private void OnValidate()
    {
        if (debugIncreaseSunColorIndicator)
        {
            IncreaseSunColorIndicator(1);
            debugIncreaseSunColorIndicator = false;
        }
        if (debugResetSunColorIndicator)
        {
            DebugResetSunColorIndicator();
            debugResetSunColorIndicator = false;
        }
    }

    private void Awake() { sun = GetComponent<Light>(); }

    private void OnEnable() { Spell.spellDyePowerNotified += IncreaseSunColorIndicator; }

    private void Start()
    {
        currentSunColorIndicator = 0;
        StartCoroutine(DecreaseSunColorIndicatorByTimeCorroutine());
    }

    void DebugResetSunColorIndicator()
    {
        currentSunColorIndicator = 0;
        DyeSun();
    }

    void IncreaseSunColorIndicator(int spellNotifiedPower)
    {
        if (currentSunColorIndicator >= maxSunColorIndicator) { return; }

        currentSunColorIndicator += spellNotifiedPower;

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

    [SerializeField] float SunDyeSmoothDuration = 1f;

    void DyeSun()
    {
        float t = (float)currentSunColorIndicator / (float)maxSunColorIndicator;
        Color skyColor = Color.Lerp(Color.white, Color.red, t);

        sun.DOColor(skyColor, SunDyeSmoothDuration);
    }

    private void OnDisable() { Spell.spellDyePowerNotified -= IncreaseSunColorIndicator; }
}
