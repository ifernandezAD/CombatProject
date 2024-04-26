using DG.Tweening;
using System;
using UnityEngine;

public class Event : MonoBehaviour
{
    [Header("OnEnable Actions")]
    [SerializeField] private OnEnableAction enableAction = OnEnableAction.None;
    private enum OnEnableAction
    {
        None,
        SpawnEventTrigger,
        SpawnEventDummy
    }

    [Header("OnDisable Actions")]
    [SerializeField] private OnDisableAction disableAction = OnDisableAction.None;
    private enum OnDisableAction
    {
        None,
        SpawnEventTrigger,
        SpawnEventDummy
    }

    [Header("Next Event Condition")]
    [SerializeField] private NextEventCondition nextCondition = NextEventCondition.ByTime;
    private enum NextEventCondition
    {
        ByTime,
        ByTrigger,
        ByAction
    }

    [Header("Show Text Variables")]
    [SerializeField] string[] eventTextArray;
    [SerializeField] float textDisplayTime = 1f;

    [Header("Actions Variables")]
    [SerializeField] GameObject triggerPrefab;
    [SerializeField] Transform triggerPosition;
    [SerializeField] GameObject dummyPrefab;
    [SerializeField] Transform dummyPosition;

    private EventManager eventManager;

    private void OnEnable()
    {
        ShowTextsByTime(() => CheckNextEventCondition(nextCondition));
        CheckOnEnableAction(enableAction);
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
            eventManager.eventTMPro.enabled = false;
            onComplete?.Invoke();
            return;
        }

        eventManager.eventTMPro.enabled = true;


        ShowTextDelayed(currentTextIndex, () =>
        {
            eventManager.eventTMPro.enabled = false;
            onComplete?.Invoke();
        });
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

        switch (condition)
        {
            case NextEventCondition.ByTime:
                eventManager.ActivateNextEvent();
                break;
            case NextEventCondition.ByTrigger:
                EventTrigger.onTriggerEvent += NextEventConditionByTrigger;
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

    private void NextEventConditionByTrigger()
    {
        eventManager.ActivateNextEvent();
        EventTrigger.onTriggerEvent -= NextEventConditionByTrigger;
    }

    private void CheckOnEnableAction(OnEnableAction enableAction)
    {
        switch (enableAction)
        {
            case OnEnableAction.None:
                break;
            case OnEnableAction.SpawnEventTrigger:
                SpawnEventTrigger();
                break;
            case OnEnableAction.SpawnEventDummy:
                SpawnEventDummy();
                break;
        }
    }

    private void CheckOnDisableAction(OnDisableAction disableAction)
    {

        switch (disableAction)
        {
            case OnDisableAction.None:
                break;
            case OnDisableAction.SpawnEventTrigger:
                SpawnEventTrigger();
                break;
            case OnDisableAction.SpawnEventDummy:
                SpawnEventDummy();
                break;
        }
    }

    private void SpawnEventTrigger()
    {
        Instantiate(triggerPrefab, triggerPosition.position, Quaternion.identity);
    }

    private void SpawnEventDummy()
    {
        Instantiate(dummyPrefab, dummyPosition.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        CheckOnDisableAction(disableAction);
    }
}



