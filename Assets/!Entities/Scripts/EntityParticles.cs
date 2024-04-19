using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityParticles : MonoBehaviour
{
    [SerializeField] GameObject bloodParticle;
    private HurtCollider hurtCollider;

    private void Awake()
    {
        hurtCollider = GetComponent<HurtCollider>();
    }

    private void OnEnable()
    {
        hurtCollider.onHitWithOffender.AddListener(InstantiateParticle);
    }

    private void InstantiateParticle(IOffender offender)
    {
       if (offender.GetWeaponType() == IOffender.WeaponType.fireWeapon)
        {
          GameObject bloodParticleClone = Instantiate(bloodParticle, transform.position, Quaternion.identity);
          Destroy(bloodParticleClone, 1);
        }
    }

    private void OnDisable()
    {
        hurtCollider.onHitWithOffender.RemoveListener(InstantiateParticle);
    }

}
