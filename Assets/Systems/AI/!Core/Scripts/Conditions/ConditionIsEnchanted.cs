using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionIsEnchanted : ConditionBase
{
    internal override bool IsMeet()
    {
        return decissionMaker.ai.IsEnchanted();
    }

    
}
