using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FollowPlayer : StateBase
{
    [SerializeField] AcknowledgePlayer acknowledgePlayer;

    private void Update()
    {
        ai.target = acknowledgePlayer.playerPosition;
        ai.SetDestinationToTarget();
    }
}
