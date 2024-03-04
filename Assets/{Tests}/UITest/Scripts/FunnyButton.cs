using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FunnyButton : MonoBehaviour
{
    EventTrigger eventTrigger;
    Vector3 originalScale;

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();

        originalScale = transform.localScale;
    }

    public void OnPointerEnter()
    {
        transform.localScale = originalScale *1.5f;

        transform.DOScale(Vector3.one * 2f, 0.25f).From().SetEase(Ease.OutBounce);
    }

    public void OnPointerExit()
    {
        transform.DOScale(originalScale, 0.5f).SetEase(Ease.Linear);
    }
}
