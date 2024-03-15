using UnityEngine;

public class State_Confused : StateBase
{
    [Header("Skin Change")]
    //Drag the skins to the inspector in every human variant
    [SerializeField] GameObject skinToEnable;
    [SerializeField] GameObject skinToDisable;

    [Header("ConfusedAnimations")]
    private readonly int confusedHash = Animator.StringToHash("IsConfused");


    private void OnEnable()
    {
        ai.StopEntity(true);
        PlayConfusedAnimation();
        ChangeSkin();
        ChangeAllegiance();
    }

    void PlayConfusedAnimation() { ai.animator.SetBool(confusedHash, true); }
    void StopConfusedAnimation(){ai.animator.SetBool(confusedHash, false);}
     
    void ChangeSkin()
    {
        skinToDisable.SetActive(false);
        skinToEnable.SetActive(true);
    }

    void ChangeAllegiance(){ai.senseable.allegiance = "Player";}
              
}
