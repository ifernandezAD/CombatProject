using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    public UnityEvent onHit;
    public UnityEvent<IOffender> onHitWithOffender;


    public void NotifyHit(IOffender offennder) 
    {
        onHit.Invoke();
        onHitWithOffender.Invoke(offennder);
    }
}
