using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecissionItemBase : MonoBehaviour
{
    internal enum ItemType 
    { 
        Action,
        Condition,
    };

    internal abstract ItemType GetItemType();

    protected DecissionMaker decissionMaker;

    internal void Init(DecissionMaker decissionMaker)
    {
        this.decissionMaker = decissionMaker;
        List<DecissionItemBase> decissionItemsList = new();
        foreach (Transform t in transform)
        {
            DecissionItemBase dib = t.GetComponent<DecissionItemBase>();
            if (dib)
            {
                decissionItemsList.Add(dib);
            }
        }
        children = decissionItemsList.ToArray();
    }
    internal DecissionItemBase[] children;
}
