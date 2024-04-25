using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    [SerializeField] Transform eventsParent;
    public TextMeshProUGUI eventTMPro;

    private Event[] eventArray;

    private void Awake()
    {
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        Event[] eventsInChildren = eventsParent.GetComponentsInChildren<Event>();

        eventArray = new Event[eventsInChildren.Length];

        for (int i = 0; i < eventsInChildren.Length; i++)
        {
            Event currentEvent = eventsInChildren[i];
            currentEvent.Init(this);
            eventArray[i] = currentEvent;
        }
    }
}