using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Enchanted : StateBase
{
    [Header("EnchantmentAnimations")]
    private readonly int gangsterEnchantmentHash = Animator.StringToHash("EnchantmentType");

    [SerializeField] float enchantedDuration = 10f;
    float currentTime;

    private void OnEnable()
    {
        currentTime = enchantedDuration;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        PlayEnchantedAnimation();
        ai.StopEntity(true);

        if (currentTime <= 0)
        {
            StopEnchantedAnimation();
            ai.StopEntity(false);
            ai.SetEnchanted(false);
        }       
    }

    internal void PlayEnchantedAnimation()
    {
        if (ai.senseable.allegiance == "Gangster")
        {
            int randomAnim = UnityEngine.Random.Range(1, 6);
            ai.animator.SetInteger(gangsterEnchantmentHash, randomAnim);
        }
    }

    internal void StopEnchantedAnimation()
    {
        if (ai.senseable.allegiance == "Gangster")
        {
            ai.animator.SetInteger(gangsterEnchantmentHash, 0);
        }
    }

}
