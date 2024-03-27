using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerTargetSelector : MonoBehaviour
{
    [SerializeField] InputActionReference targetLock;
    [SerializeField] Vector3 bounds = new Vector3(6f, 6f, 30f);
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    [SerializeField] [Range(0f, 0.5f)] float lockDistance = 0.05f;
    [SerializeField] Transform[] doNotTarget;
    [SerializeField][Allegiance] string[] selectableAllegiances;
    

    public UnityEvent<Transform> onTargetAcquired;
    public UnityEvent<Transform> onTargetReleased;


    Camera mainCamera;

    public Transform lockedTarget { get; private set; }

    [Header("Debug")]
    [SerializeField] Transform debugLockedTarget;

    private void OnEnable()
    {
        targetLock.action.Enable();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    Transform oldLockedTarget;

    private void Update()
    {
        if (targetLock.action.IsPressed())
        {
            UpdateLock();
        }
        else
        {
            lockedTarget = null;
        }

        UpdateTargetAcquisition();

        debugLockedTarget = lockedTarget;
    }

    private void UpdateTargetAcquisition()
    {
        if (lockedTarget != oldLockedTarget)
        {
            if (oldLockedTarget)
            {
                onTargetReleased.Invoke(oldLockedTarget);
            }
            if (lockedTarget)
            {
                onTargetAcquired.Invoke(lockedTarget);
            }
        }

        oldLockedTarget = lockedTarget;
    }

    Vector2 viewportCenter = Vector2.one / 2f;
    private void UpdateLock()
    {
        Vector3 boxCenter = transform.position + (transform.forward * (bounds.z / 2f));
        Collider[] colliders = Physics.OverlapBox(boxCenter, bounds / 2f, transform.rotation, layerMask);
        List<Senseable> senseables = new();

        foreach (Collider c in colliders)
        {
            Senseable senseable = c.GetComponent<Senseable>();
            if ((senseable != null) &&
                !doNotTarget.Contains(senseable.transform) &&
                (Array.Find(selectableAllegiances, x => x == senseable.allegiance) != null) )
            {
                senseables.Add(senseable);
            }
        }

        //colliders.OrderBy(c => Vector2.Distance(viewportCenter, mainCamera.WorldToViewportPoint(c.transform.position)));

        senseables.OrderBy(e => Vector2.Distance(viewportCenter, mainCamera.WorldToViewportPoint(e.transform.position)));

        lockedTarget = senseables.Count > 0 ? senseables[0].transform : null;
    }

    private void OnDisable()
    {
        targetLock.action.Disable();
    }
}
