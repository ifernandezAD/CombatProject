using System;
using UnityEngine;

public class State_Confused : StateBase
{
    [Header("Skin Change")]
    [SerializeField] Transform skinsParent;
    [SerializeField] GameObject playerSkin;

    [Header("Timing")]
    [SerializeField] float confusedDuration = 10f;
    float currentTime;

    [Header("ConfusedAnimations")]
    private readonly int confusedHash = Animator.StringToHash("IsConfused");


    private void OnEnable()
    {
        currentTime = confusedDuration;
        ai.StopMovement();

        PlayConfusedAnimation();
        ChangeSkin();
        ChangeAllegiance();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            StopConfusedStatus();
            ai.RestoreSpeed();
            ai.SetEnchanted(false);
        }
    }

    void PlayConfusedAnimation() { ai.animator.SetBool(confusedHash, true); }
    void StopConfusedAnimation() { ai.animator.SetBool(confusedHash, false); }

    int activeChildIndex = -1;
    public void ChangeSkin()
    {
        foreach (Transform child in skinsParent)
        {
            if (child.gameObject.activeSelf)
            {
                activeChildIndex = child.GetSiblingIndex();
                child.gameObject.SetActive(false);
                break;
            }
        }

        playerSkin.SetActive(true);
    }

    void StopConfusedStatus()
    {
        StopConfusedAnimation();
        RecoverPreviousSkin();
        RecoverPreviousAllegiance();

        ai.SetConfused(false);
    }
 

    private void RecoverPreviousSkin()
    {
        playerSkin.SetActive(false);
        skinsParent.GetChild(activeChildIndex).gameObject.SetActive(true);
    }

    private string previousAllegiance;
    void ChangeAllegiance()
    {
        previousAllegiance = ai.senseable.allegiance;
        ai.senseable.allegiance = "Player";
    }

    private void RecoverPreviousAllegiance()
    {
        ai.senseable.allegiance = previousAllegiance;
    }

}
