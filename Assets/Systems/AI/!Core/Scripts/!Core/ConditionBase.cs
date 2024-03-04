using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionBase : DecissionItemBase
{
    internal override ItemType GetItemType()
    {
        return ItemType.Condition;
    }

    internal abstract bool IsMeet();
}
