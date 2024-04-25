using UnityEngine;
using DG.Tweening;
using System;

public class Event : MonoBehaviour
{
    [Header("Show Text Variables")]
    [SerializeField] string[] eventTextArray;
    [SerializeField] float textDisplayTime = 1f;

    [Header("Next Event Condition")]
    [SerializeField] private NextEventCondition nextCondition = NextEventCondition.ByTime;
    private enum NextEventCondition
    {
        ByTime,
        ByTrigger,
        ByAction
    }

    private EventManager eventManager;

    private void OnEnable()
    {
        ShowTextsByTime(() => CheckNextEventCondition(nextCondition));
    }

    public void Init(EventManager eventManager)
    {
        this.eventManager = eventManager;
    }

    private int currentTextIndex = 0;
    private void ShowTextsByTime(Action onComplete = null)
    {
        if (eventTextArray.Length == 0)
        {
            onComplete?.Invoke();
            return;
        }

        ShowTextDelayed(currentTextIndex, onComplete);
    }

    private void ShowTextDelayed(int index, Action onComplete = null)
    {
        if (index < 0 || index >= eventTextArray.Length)
            return;

        eventManager.eventTMPro.text = eventTextArray[index];

        currentTextIndex++;
        if (currentTextIndex < eventTextArray.Length)
        {
            DOTween.Sequence()
                .AppendInterval(textDisplayTime)
                .OnComplete(() => ShowTextDelayed(currentTextIndex, onComplete));
        }
        else
        {
            DOTween.Sequence()
                .AppendInterval(textDisplayTime)
                .OnComplete(() => onComplete?.Invoke());
        }
    }

    private void CheckNextEventCondition(NextEventCondition condition)
    {

        bool isLastEvent = currentTextIndex == eventTextArray.Length;
        if (isLastEvent){return;}
                          
        switch (condition)
        {
            case NextEventCondition.ByTime:
                eventManager.ActivateNextEvent();
                break;
            case NextEventCondition.ByTrigger:
                // Handle condition for ByTrigger
                Debug.Log("Next event condition is ByTrigger");
                break;
            case NextEventCondition.ByAction:
                // Handle condition for ByAction
                Debug.Log("Next event condition is ByAction");
                break;
            default:
                Debug.LogWarning("Unknown next event condition");
                break;
        }
    }
}
