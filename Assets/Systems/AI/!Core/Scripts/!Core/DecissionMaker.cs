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
        CheckItem(firstItem);
    }

    private void CheckItem(DecissionItemBase dib)
    {
        switch (dib.GetItemType())
        {
            case DecissionItemBase.ItemType.Action:
                (dib as ActionBase).Perform();
                //TODO -Seguir ejecutando en el caso de que haya más acciones
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