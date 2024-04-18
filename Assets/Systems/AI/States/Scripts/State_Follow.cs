using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Follow : StateBase
{
    [SerializeField] Transform followTarget;
    [SerializeField] float holdPositionDistance;
    [SerializeField] float timeBetweenLookDirectionChange = 2.5f;

    private void OnEnable()
    {
        followTarget = GameLogic.instance.playerTransform;
    }

    private void Update()
    {
        Vector3 direction = followTarget.position - transform.position;
        float distance2 = direction.sqrMagnitude;

        if (distance2 < (holdPositionDistance * holdPositionDistance))
        {
            UpdateHoldPosition();
        }
        else
        {
            UpdateFollow();
        }
    }

    private void UpdateFollow()
    {
        ai.SetDestination(followTarget.position);
        timeLastLookDirectionChange = timeBetweenLookDirectionChange;
        desiredLookDirection = transform.forward;
        timeLastLookDirectionChange = Time.time;
    }

    float timeLastLookDirectionChange;
    Vector3 desiredLookDirection;
    private void UpdateHoldPosition()
    {
        ai.SetDestination(transform.position);

        if ((Time.time - timeLastLookDirectionChange) > timeBetweenLookDirectionChange)
        {
            timeLastLookDirectionChange = Time.time;
            Vector2 newLookDirection = UnityEngine.Random.insideUnitCircle;
            desiredLookDirection = new Vector3(newLookDirection.x, 0f, newLookDirection.y);          
        }
        ai.entityMovement.Orientate(desiredLookDirection);
    }
}
