using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedTargetIcon : MonoBehaviour
{
    [SerializeField] PlayerTargetSelector playerTargetSelector;
    [SerializeField] Transform visuals;

    [SerializeField] Vector3 offset = new Vector3(0f, 2f, 0f);
 
    Transform acquiredTarget;

    private void OnEnable()
    {
        playerTargetSelector.onTargetAcquired.AddListener(OnTargetAcquired);
        playerTargetSelector.onTargetReleased.AddListener(OnTargetReleased);
    }

    private void Start()
    {
        visuals.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        transform.position = acquiredTarget.position;
    }

    private void OnTargetAcquired(Transform target)
    {
        visuals.gameObject.SetActive(true);
        acquiredTarget = target;
    }

    private void OnTargetReleased(Transform target)
    {
        visuals.gameObject.SetActive(false);
        acquiredTarget = null;
    }

    private void OnDisable()
    {
        playerTargetSelector.onTargetAcquired.RemoveListener(OnTargetAcquired);
        playerTargetSelector.onTargetReleased.RemoveListener(OnTargetReleased);
    }
}
