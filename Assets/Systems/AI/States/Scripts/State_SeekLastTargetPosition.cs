using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_SeekLastTargetPosition : StateBase
{
    [SerializeField] float reachDistance = 1f;

    private void Update()
    {
        ai.SetDestinationToLastTargetPosition();
        if (Vector3.Distance(ai.GetLastTargetPosition(), transform.position) < reachDistance)
        {
            ai.NotifyLastTargetPositionVisited();
        }
    }
}
