using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HurtCollider>(out HurtCollider hurtCollider))
        {
            hurtCollider.NotifyHit(this);
        }
    }
}
