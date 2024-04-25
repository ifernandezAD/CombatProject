using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EventManager : MonoBehaviour
{
    [SerializeField] Transform eventsParent;
    public TextMeshProUGUI eventTMPro;

    public Event[] eventArray;

    [Header("Debug")]
    [SerializeField] bool debugActivateFirstEvent;
    [SerializeField] bool debugActivateNextEvent;

    private void OnValidate()
    {
        if (debugActivateFirstEvent)
        {
            ActivateFirstEvent();
            debugActivateFirstEvent = false;
        }

        if (debugActivateNextEvent)
        {
            ActivateNextEvent();
            debugActivateNextEvent = false;
        }
    }

    private void Awake()
    {
        InitializeEvents();
    }

    private void Start()
    {
        ActivateFirstEvent();
    }

    private void ActivateFirstEvent()
    {
        if (eventArray != null && eventArray.Length > 0)
        {
            eventArray[0].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No events found or event array is not initialized.");
        }
    }

    private void InitializeEvents()
    {
        Event[] eventsInChildren = eventsParent.GetComponentsInChildren<Event>(true);

        eventArray = new Event[eventsInChildren.Length];

        for (int i = 0; i < eventsInChildren.Length; i++)
        {
            Event currentEvent = eventsInChildren[i];
            currentEvent.Init(this);
            eventArray[i] = currentEvent;
        }
    }

    public int currentEventIndex = 0;
    public void ActivateNextEvent()
    {
        bool isLastEventGameObject = currentEventIndex == eventArray.Length - 1;
        if (isLastEventGameObject)
        {
            eventArray[currentEventIndex].gameObject.SetActive(false);
            return;
        }

        if (currentEventIndex >= 0 && currentEventIndex < eventArray.Length)
        {
            eventArray[currentEventIndex].gameObject.SetActive(false);

            currentEventIndex = (currentEventIndex + 1) % eventArray.Length;

            eventArray[currentEventIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Current event index is out of bounds.");
        }
    }
}