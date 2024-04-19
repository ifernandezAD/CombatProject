using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour, IOffender
{
    [SerializeField] float explosionForce = 1000f;
    [SerializeField] float upwardsModifier = 300f;
    [SerializeField] float range = 10f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    [SerializeField] GameObject visualExplosionPrefab;
    private AudioSource audioSource;
    [SerializeField] private SimpleAudioEvent explosionAudioEvent;

    Vector3 hitDirection;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (visualExplosionPrefab){ Instantiate(visualExplosionPrefab, transform.position, Quaternion.identity); }

        Collider[] colliders = Physics.OverlapSphere(transform.position, range, layerMask);
        foreach (Collider c in colliders)
        {
            hitDirection = c.transform.position - transform.position;
            c.GetComponent<HurtCollider>()?.NotifyHit(this);
            c.attachedRigidbody?.AddExplosionForce(explosionForce, transform.position,range,upwardsModifier);
        }

        PlayExplosionSound();
        DOVirtual.DelayedCall(4, () => Destroy(gameObject)); ;      
    }

    public void PlayExplosionSound()
    {
        explosionAudioEvent.Play(audioSource);
    }

    public Vector3 GetDirection()
    {
        return hitDirection;
    }

    public Transform GetTransform()
    {
        return transform;
    }


    public IOffender.WeaponType GetWeaponType()
    {
        return IOffender.WeaponType.explosionWeapon;
    }
}
