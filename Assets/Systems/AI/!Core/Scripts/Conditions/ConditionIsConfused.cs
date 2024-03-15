using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionIsConfused : ConditionBase
{
    internal override bool IsMeet()
    {
        return decissionMaker.ai.IsConfused();
    }   
}
