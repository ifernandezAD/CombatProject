using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Seeking : StateBase
{
    private void Update()
    {
        ai.SetDestinationToTarget();
    }
}
