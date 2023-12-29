using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudibleByMovement : AudibleBase
{
    [Header("Emisssion by Movement")]
    [SerializeField] float minVelocity = 0.5f;
    [SerializeField] float maxVelocity = 8f;
    [SerializeField] float rangeOnMinVelocity = 2f;
    [SerializeField] float rangeOnMaxVelocity = 10f;

    Vector3 oldPosition;

    protected override void InternalAwake()
    {
        oldPosition = transform.position;
    }

    protected override void CheckEmit(float timeSinceLastCheck)
    {
        float currentVelocity = ((transform.position - oldPosition) / timeSinceLastCheck).magnitude;
        if (currentVelocity > minVelocity)
        {
            float t = Mathf.InverseLerp(minVelocity, maxVelocity, currentVelocity);
            float range = Mathf.Lerp(rangeOnMinVelocity, rangeOnMaxVelocity, t);
            InternalEmit(range);
        }
        oldPosition = transform.position;
    }
}
