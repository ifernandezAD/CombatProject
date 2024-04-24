using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Transform eventsParent;

    private void Start()
    {
        eventsParent.GetChild(0).gameObject.SetActive(true);
    }
}
