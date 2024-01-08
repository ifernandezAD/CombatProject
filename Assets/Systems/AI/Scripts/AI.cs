using System;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform target;

    [Header("Path Calculation")]
    [SerializeField] float pathCalculationThresoldDistance = 1f;



    [SerializeField] int areaMask = NavMesh.AllAreas;

    [Header("Debugging")]
    [SerializeField] float speed = 4f;


    [Header("Path Following")]
    [SerializeField] float cornerReachThresholdDistance = 1.5f;

    [Header("Senses")]
    [SerializeField] Transform sensesParent;

    NavMeshPath path;
    public EntityMovement entityMovement;

    Vector3 lastSeekedPosition = Vector3.zero;

    Sight sight;
    Audition audition;
    StateBase[] allStates;

    private void Awake()
    {
        entityMovement = GetComponent<EntityMovement>();

        Senseable senseable = GetComponent<Senseable>();
        sight = sensesParent.GetComponentInChildren<Sight>();
        audition = sensesParent.GetComponentInChildren<Audition>();
        sight.SetMySenseable(senseable);
        audition.SetMySenseable(senseable);

        path = new();

        allStates = GetComponents<StateBase>();
        foreach (StateBase sb in allStates) { sb.Init(this); }
    }

    void Update()
    {
        UpdateTarget();
        UpdatePathToTarget();
        UpdateFollowPath();
    }

    private void UpdateTarget()
    {
        Transform sightTarget = sight.GetSenseable()?.transform;
        Transform auditionTarget = audition.GetSenseable()?.transform;

        if (!sightTarget)
        {
            target = auditionTarget;
        }
        else if (!auditionTarget)
        {
            target = sightTarget;
        }
        else
        {
            float distanceToVisible = Vector3.Distance(transform.position, sightTarget.position);
            float distanceToAudible = Vector3.Distance(transform.position, auditionTarget.position);
            target = distanceToVisible < distanceToAudible ? sightTarget : auditionTarget;
        }
    }

    private void UpdatePathToTarget()
    {
        if (target)
        {
            if (Vector3.Distance(lastSeekedPosition, target.position) > pathCalculationThresoldDistance)
            {
                CalculatePath(target.position);
            }
        }
        else if (HasPath())
        {
            ClearPath();
        }
    }

    private void UpdateFollowPath()
    {
        Vector3 xzPlaneVelocity = FollowPath();
        entityMovement.Orientate(xzPlaneVelocity);
        entityMovement.Animate();
    }

    public void SetDestination(Vector3 position)
    {
        CalculatePath(position);
    }

    bool CalculatePath(Vector3 position)
    {
        bool pathOk = NavMesh.CalculatePath(transform.position, position, areaMask, path);
        if (pathOk)
        {
            lastSeekedPosition = position;
            nextCorner = 1;
        }
        return pathOk;
    }

    bool HasPath()
    {
        return path.corners.Length > 0;
    }

    void ClearPath()
    {
        path.ClearCorners();
    }

    int nextCorner = 0;
    Vector3 FollowPath()
    {
        Vector3 xzPlaneVelocity = Vector3.zero;
        if (HasPath())
        {
            Vector3 nextPoint = path.corners[nextCorner];
            Vector3 direction = nextPoint - transform.position;

            xzPlaneVelocity = entityMovement.Move(direction, false);

            if (Vector3.Distance(nextPoint, transform.position) < cornerReachThresholdDistance)
            {
                nextCorner++;
                if (nextCorner >= path.corners.Length)
                {
                    ClearPath();
                }
            }
        }

        return xzPlaneVelocity;
    }

    internal bool HasTarget()
    {
        return target != null;
    }

    internal void SetDestinationToTarget()
    {
        SetDestination(target.position);
    }
}
