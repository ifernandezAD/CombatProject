using UnityEngine;

[DefaultExecutionOrder(-50)]
public class DecissionMaker : MonoBehaviour
{
    [SerializeField] public AI ai;
    DecissionItemBase firstItem;

    private void Awake()
    {
        DecissionItemBase[] decissionItems = GetComponentsInChildren<DecissionItemBase>();
        foreach (DecissionItemBase dib in decissionItems) { dib.Init(this); }

        firstItem = decissionItems.Length > 0 ? decissionItems[0] : null;
    }

    void Update()
    {
        Transform firstChild = transform.childCount > 0 ? transform.GetChild(0) : null;
        CheckItem(firstItem);
    }

    private void CheckItem(DecissionItemBase dib)
    {
        switch (dib.GetItemType())
        {
            case DecissionItemBase.ItemType.Action:
                (dib as ActionBase).Perform();
                //TODO -Seguir ejecutando
                break;
            case DecissionItemBase.ItemType.Condition:
                if ((dib as ConditionBase).IsMeet())
                {
                    CheckItem(dib.children[0]);
                }
                else
                {
                    CheckItem(dib.children[1]);
                }
                break;
        }
    }
}