using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionIsRoaming : ConditionBase
{
    internal override bool IsMeet()
    {
        return decissionMaker.ai.IsRoaming();
    }
}
