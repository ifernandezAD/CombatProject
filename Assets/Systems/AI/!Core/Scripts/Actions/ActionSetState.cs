using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSetState : ActionBase
{
    [SerializeField] StateBase state;

    internal override void Perform()
    {
        decissionMaker.ai.SetState(state);
    }
}
