using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDeath : Death
{
    DecissionMaker decissionMaker;
    AI ai;
    CharacterController characterController;

    public bool isDead { get; private set; }

    protected override void InternalAwake()
    {
        base.InternalAwake();

        ai = GetComponent<AI>();
        characterController = GetComponent<CharacterController>();
        decissionMaker = gameObject.transform.parent.GetComponentInChildren<DecissionMaker>();
    }

    protected override void ManageDeath()
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

}
