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


}
