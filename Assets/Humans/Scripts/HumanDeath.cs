using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDeath : MonoBehaviour
{
    EntityLife entityLife;
    Senseable senseable;
    CharacterController characterController;

    [SerializeField] private bool isDead;

    private void Awake()
    {
        entityLife = GetComponent<EntityLife>();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        entityLife.onLifeDepleted.AddListener(ManageHumanDeath);
    }

    private void ManageHumanDeath()
    {
        ShrinkCharacterController();
        senseable.DisableSenseables();
        isDead = true;
    }

    private void ShrinkCharacterController()
    {
        var controllerYpos = characterController.center.y;

        characterController.stepOffset = 0;
        characterController.radius = 0.1f;
        characterController.height = 0.1f;
        controllerYpos = 0.1f;
    }

    private void OnDisable()
    {
        entityLife.onLifeDepleted.RemoveListener(ManageHumanDeath);
    }

}
