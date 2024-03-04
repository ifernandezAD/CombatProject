using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeInTheField : MonoBehaviour
{
    Vector3 originalPosition;
    CubeField parentCubeField;

    private void Awake()
    {
        originalPosition = transform.position;
        parentCubeField = GetComponentInParent<CubeField>();
    }

    public void OnPointerEnter()
    {
        transform.DOMoveY(originalPosition.y + 0.25f, 0.25f);
    }

    public void OnPointerExit()
    {
        transform.DOMoveY(originalPosition.y, 0.25f);
    }

    public void OnClick()
    {
        parentCubeField.NotifyCubeClicked(this);
    }
}
