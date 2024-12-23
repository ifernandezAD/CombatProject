using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour, IOffender
{
    [SerializeField] public UnityEvent<HitCollider, HurtCollider> onHitNotified;
    [SerializeField] public UnityEvent<HitCollider, Collider> onHitAgainstCollider;
    [SerializeField] public UnityEvent<HitCollider, Collider> onHitAgainstTrigger;

    Vector3 hitDirection;

    private void OnCollisionEnter(Collision collision)
    {
        onHitAgainstCollider.Invoke(this, collision.collider);
        CheckCollider(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        onHitAgainstTrigger.Invoke(this, other);
        CheckCollider(other);
    }

    private void CheckCollider(Collider other)
    {
        if (other.TryGetComponent<HurtCollider>(out HurtCollider hurtCollider) && !IsSameEntity(other.gameObject))
        {
            hitDirection = other.transform.position - transform.position;
            onHitNotified.Invoke(this, hurtCollider);
            hurtCollider.NotifyHit(this);
        }
    }

    private bool IsSameEntity(GameObject otherGameObject)
    {
        return otherGameObject.transform.root == transform.root;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Vector3 GetDirection()
    {
        return hitDirection;
    }

    public IOffender.WeaponType GetWeaponType()
    {
        return IOffender.WeaponType.meleeWeapon;
    }
}
