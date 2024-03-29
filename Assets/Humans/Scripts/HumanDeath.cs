using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDeath : MonoBehaviour
{
    EntityLife entityLife;
    DecissionMaker decissionMaker;
    AI ai;
    CharacterController characterController;

    public bool isDead { get; private set; }

    private void Awake()
    {
        entityLife = GetComponent<EntityLife>();
        ai = GetComponent<AI>();
        characterController = GetComponent<CharacterController>();
        decissionMaker = gameObject.transform.parent.GetComponentInChildren<DecissionMaker>();
    }

    private void OnEnable()
    {
        entityLife.onLifeDepleted.AddListener(ManageHumanDeath);
    }

    private void ManageHumanDeath()
    {
        ShrinkCharacterController();
        
        ai.senseable.DisableSenseables();
        DisableAIStateMachine();

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

    private void DisableAIStateMachine()
    {
        decissionMaker.enabled = false;

        foreach (StateBase sb in ai.allStates)
        {
            if (sb.enabled)
            {
                sb.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        entityLife.onLifeDepleted.RemoveListener(ManageHumanDeath);
    }

}
