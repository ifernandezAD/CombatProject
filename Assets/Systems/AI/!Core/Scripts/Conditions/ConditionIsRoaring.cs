using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionIsRoaring : ConditionBase
{
    internal override bool IsMeet()
    {
        return decissionMaker.ai.IsRoaring();
    }
}
