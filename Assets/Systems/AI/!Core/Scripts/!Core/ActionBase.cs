using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : DecissionItemBase
{
    internal override ItemType GetItemType()
    {
        return ItemType.Action;
    }

    internal abstract void Perform();
}
