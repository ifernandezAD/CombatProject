using UnityEngine;

public class State_Roaming : StateBase
{
    [SerializeField] [CivilianAnimation] string[] roamingAnimation;

    private void OnEnable()
    {
        Debug.Log("The Civilian is Roaming");
    }
}
