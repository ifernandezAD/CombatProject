using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    Ragdollizer ragdollizer;
    HurtCollider hurtCollider;
    EntityLife entityLife;
    Animator animator;

    IOffender lastOffender;

    private void Awake()
    {
        ragdollizer = GetComponentInChildren<Ragdollizer>();
        hurtCollider = GetComponent<HurtCollider>();
        entityLife = GetComponent<EntityLife>();
        animator = GetComponentInChildren<Animator>();

        hurtCollider.onHitWithOffender.AddListener(OnHitWithOffender);
        entityLife.onLifeDepleted.AddListener(OnLifeDepleted);

        ChildAwake();
    }

    protected abstract void ChildAwake();

    private void Start()
    {
        ragdollizer.UnRagdollize();
        ChildStart();
    }

    protected abstract void ChildStart();

    private void OnHitWithOffender(IOffender offender)
    {
        lastOffender = offender;
    }

    private void OnLifeDepleted()
    {
        animator.enabled = false;
        ragdollizer.Ragdollize(transform.position - lastOffender.GetTransform().position, 3000f, 6000f);
    }
}
