using UnityEngine;

public class State_Roaming : StateBase
{

    [SerializeField] string[] roamingAnimations;
    [SerializeField] [CivilianAnimation] public string selectedAnimation;

    private void OnEnable()
    {
        Debug.Log("The Civilian is Roaming");
    }
}
