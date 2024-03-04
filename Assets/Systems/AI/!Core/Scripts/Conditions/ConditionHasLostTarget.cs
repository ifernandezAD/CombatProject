using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionHasLostTarget : ConditionBase
{
    internal override bool IsMeet()
    {
        return decissionMaker.ai.HasLostTarget();
    }
}
