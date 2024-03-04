using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityLife : MonoBehaviour
{
    [SerializeField] float originalLife = 1f;
    float currentLife;
    HurtCollider hurtCollider;

    public UnityEvent<float> onLifeChanged;
    public UnityEvent onLifeDepleted;

    private void Awake()
    {
        currentLife = originalLife;
        hurtCollider = GetComponent<HurtCollider>();
    }

    private void OnEnable()
    {
        hurtCollider.onHitWithOffender.AddListener(OnHitWithOffender);
    }

    private void OnHitWithOffender(IOffender offender)
    {
        if (currentLife > 0)
        {
            currentLife -= 0.3f;
            onLifeChanged.Invoke(Mathf.Clamp01(currentLife / originalLife));
            if (currentLife <= 0f)
            {
                onLifeDepleted.Invoke();
            }
        }
    }

    public void RecoverAllLife()
    {
        currentLife += originalLife;
        if (currentLife >= originalLife){ currentLife = originalLife;}
        
        onLifeChanged.Invoke(Mathf.Clamp01(currentLife / originalLife));
    }

    private void OnDisable()
    {
        hurtCollider.onHitWithOffender.RemoveListener(OnHitWithOffender);
    }
}
