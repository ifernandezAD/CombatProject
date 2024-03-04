using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenHitAgainstCollider : MonoBehaviour
{
    HitCollider hitCollider;

    private void Awake()
    {
        hitCollider = GetComponent<HitCollider>();
    }

    private void OnEnable()
    {
        hitCollider.onHitAgainstCollider.AddListener(OnHitAgainstCollider);
    }

    private void OnDisable()
    {
        hitCollider.onHitAgainstCollider.RemoveListener(OnHitAgainstCollider);
    }

    void OnHitAgainstCollider(HitCollider hitCollider, Collider collider)
    {
        Destroy(gameObject);
    }
}
