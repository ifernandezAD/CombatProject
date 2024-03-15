using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Confused : StateBase
{
    [Header("ConfusedAnimations")]
    private readonly int gangsterConfusedHash = Animator.StringToHash("IsConfused");


    private void Update()
    {
        //Cambia de Skin
        //Cambia su allegiance a player
        

        ai.StopEntity(true);
        PlayConfusedAnimation();    
    }

    internal void PlayConfusedAnimation()
    {
        ai.animator.SetBool(gangsterConfusedHash, true);
    }

    internal void StopConfusedAnimation()
    {
        ai.animator.SetBool(gangsterConfusedHash, false);
    }
}
