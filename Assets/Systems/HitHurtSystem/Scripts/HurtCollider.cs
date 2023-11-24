using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    public UnityEvent onHit;
    public UnityEvent<HitCollider> onHitWithHitCollider;
    public UnityEvent<MeleeWeaponByRaycast> onHitWithMeleeWeaponByRaycast;

    internal void NotifyHit(HitCollider hitCollider)
    {
        onHit.Invoke();
        onHitWithHitCollider.Invoke(hitCollider);
    }

    internal void NotifyHit(MeleeWeaponByRaycast meleeWeaponByRaycast)
    {
        onHit.Invoke();
        onHitWithMeleeWeaponByRaycast.Invoke(meleeWeaponByRaycast);
    }
}
