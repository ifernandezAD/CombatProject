using System;
using UnityEngine;

public class State_Roaming : StateBase
{

    [SerializeField] string[] roamingAnimations;
    [SerializeField] [CivilianAnimation] public string selectedAnimation;

    private void OnEnable()
    {
        SetRoamingAnimation(selectedAnimation);
    }

    private void SetRoamingAnimation(string selectedAnimation)
    {
        animator.SetBool(selectedAnimation, true);
    }
}
