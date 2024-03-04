using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionHasTarget : ConditionBase
{
    internal override bool IsMeet()
    {
        return decissionMaker.ai.HasTarget();
    }
}
