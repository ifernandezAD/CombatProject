using System;
using UnityEngine;

[Obsolete("Use DecisionMaker")]
public class DecisionMakerOLD : MonoBehaviour
{
    AI ai;

    State_Patrol statePatrol;
    State_Seeking stateSeeking;

    StateBase[] allStates;

    private void Awake()
    {
        ai = GetComponent<AI>();
        allStates = GetComponents<StateBase>();
        statePatrol = GetComponent<State_Patrol>();
        stateSeeking = GetComponent<State_Seeking>();
    }

    void Update()
    {
        if (ai.HasTarget())
        {
            SetState(stateSeeking);
        }
        else
        {
            SetState(statePatrol);
        }
    }

    private void SetState(StateBase nextState)
    {
        if (!nextState.enabled)
        {
            foreach (StateBase sb in allStates)
            {
                if (sb.enabled)
                {
                    sb.enabled = false;
                }
            }
            nextState.enabled = true;
        }
    }
}
