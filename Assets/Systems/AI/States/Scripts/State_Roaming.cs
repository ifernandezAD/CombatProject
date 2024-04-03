using System;
using UnityEngine;

public class State_Roaming : StateBase
{

    [SerializeField] string[] roamingAnimations;
    [SerializeField] [CivilianAnimation] public string selectedAnimation;

    private void OnEnable()
    {
        Debug.Log("Entered Roaming Status");
        SetRoamingAnimation(selectedAnimation);
    }

    private void SetRoamingAnimation(string selectedAnimation)
    {
        animator.SetBool(selectedAnimation, true);
    }

    private void RemoveRoamingAnimation(string selectedAnimation)
    {
        animator.SetBool(selectedAnimation, false);
    }

    private void OnDisable()
    {
        RemoveRoamingAnimation(selectedAnimation);
    }

}
