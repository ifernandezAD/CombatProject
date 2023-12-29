using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudibleByDuration : AudibleBase
{
    [Header("Emission by Duration")]
    [SerializeField] float timeRemaining = 0f;
    [SerializeField] float range = 10f;

    protected override void CheckEmit(float timeSinceLastCheck)
    {
        timeRemaining -= timeSinceLastCheck;
        if (timeRemaining > 0f)
        {
            InternalEmit(range);
        }
    }

    public void Emit(float duration)
    {
        timeRemaining = duration;
    }

}
