using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Confused : StateBase
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

        ai.PlayEnchantedAnimation();
        ai.StopEntity(true);

        if (currentTime <= 0)
        {
            ai.StopEnchantedAnimation();
            ai.StopEntity(false);
            ai.SetEnchanted(false);
        }       
    }

}
