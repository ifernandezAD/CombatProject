using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    public UnityEvent onHit;
    public UnityEvent<HitCollider> onHitWithHitCollider;

    internal void NotifyHit(HitCollider hitCollider)
    {
        onHit.Invoke();
        onHitWithHitCollider.Invoke(hitCollider);
    }
}
