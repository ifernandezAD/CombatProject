using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Enchanted : StateBase
{
    [SerializeField] float enchantedDuration = 10f;
    float currentTime;

    private void OnEnable()
    {
        currentTime = enchantedDuration;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        ai.SetEnchantedAnimation();

        if (currentTime <= 0)
        {
            ai.SetEnchanted(false);
        }       
    }

}
