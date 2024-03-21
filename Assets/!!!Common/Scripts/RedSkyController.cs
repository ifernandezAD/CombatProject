using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class RedSkyController : MonoBehaviour
{
    public static RedSkyController instance { get; private set; }

    [Header("Debug")]
    [SerializeField] bool debugIncreaseVolumeWeightIndicator;
    [SerializeField] bool debugResetVolumeWeightIndicator;

    Volume volume;

    [Header("Variables")]
    [SerializeField] int currentVolumeWeightIndicator = 0;
    [SerializeField] int maxVolumeWeightIndicator = 10;
    [SerializeField] float volumeWeightRecoveryTime = 10f;

    [Header("Events")]
    public UnityEvent onMaxRedSkyReached;
    private bool maxRedSkyReached;

    private void OnValidate()
    {
        if (debugIncreaseVolumeWeightIndicator)
        {
            IncreaseVolumeWeightIndicator(1);
            debugIncreaseVolumeWeightIndicator = false;
        }
        if (debugResetVolumeWeightIndicator)
        {
            DebugResetSunColorIndicator();
            debugResetVolumeWeightIndicator = false;
        }
    }

    private void Awake()
    {
        instance = this;

        volume = GetComponent<Volume>(); 
    }

    private void OnEnable() { Spell.spellDyePowerNotified += IncreaseVolumeWeightIndicator; }

    private void Start()
    {
        currentVolumeWeightIndicator = 0;
        StartCoroutine(DecreaseSunColorIndicatorByTimeCorroutine());
    }

    void DebugResetSunColorIndicator()
    {
        currentVolumeWeightIndicator = 0;
        DyeSky();
    }

    void IncreaseVolumeWeightIndicator(int spellNotifiedPower)
    {
        currentVolumeWeightIndicator += spellNotifiedPower;

        if (currentVolumeWeightIndicator >= maxVolumeWeightIndicator && !maxRedSkyReached)
        {        
            maxRedSkyReached = true;
            onMaxRedSkyReached?.Invoke();
            return; 
        }       

        DyeSky();
    }

    IEnumerator DecreaseSunColorIndicatorByTimeCorroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(volumeWeightRecoveryTime);

            if (currentVolumeWeightIndicator > 0 && currentVolumeWeightIndicator < maxVolumeWeightIndicator)
            {
                currentVolumeWeightIndicator--;
                DyeSky();
            }
        }
    }

    [SerializeField] float smoothDuration = 1f;

    void DyeSky()
    {
        float t = (float)currentVolumeWeightIndicator / (float)maxVolumeWeightIndicator;
        float targetWeight = Mathf.Lerp(0f, 1f, t);

        DOTween.To(() => volume.weight, x => volume.weight = x, targetWeight, smoothDuration);
    }

    private void OnDisable() { Spell.spellDyePowerNotified -= IncreaseVolumeWeightIndicator; }
}