using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public static Action onTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onTriggerEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
