using System;
using UnityEngine;
using UnityEngine.AI;

public class AI : EntityBase
{
    [Header("Target")]
    [SerializeField] public Transform target;

    [Header("Path Calculation")]
    [SerializeField] float pathCalculationThresoldDistance = 1f;
    [SerializeField] int areaMask = NavMesh.AllAreas;

    [Header("Path Following")]
    [SerializeField] float cornerReachThresholdDistance = 1.5f;

    [Header("Senses")]
    [SerializeField] Transform sensesParent;

    [Header("References")]
    public EntityWeapons entityWeapons;
    NavMeshPath path;
    Sight sight;
    Audition audition;
    
    public Senseable senseable;
    public StateBase[] allStates;

    Vector3 lastSeekedPosition = Vector3.zero;

    protected override void ChildAwake()
    {
        entityWeapons = GetComponent<EntityWeapons>();
        senseable = GetComponent<Senseable>();       
        sight = sensesParent.GetComponentInChildren<Sight>();
        audition = sensesParent.GetComponentInChildren<Audition>();
        sight?.SetMySenseable(senseable);
        audition?.SetMySenseable(senseable);

        path = new();

        allStates = GetComponents<StateBase>();
        foreach (StateBase sb in allStates) { sb.Init(this); }
    }

    protected override void ChildStart()
    {

    }

    void Update()
    {
        UpdateTarget();
        UpdateFollowPath();

        
    }

    bool hasLostTarget;
    Vector3 lastTargetPosition;

    private void UpdateTarget()
    {
        Transform oldTarget = target;

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


        if (oldTarget != target)
        {
            if (target == null)
            {
                hasLostTarget = true;
            }
        }

        if (target)
        {
            hasLostTarget = false;
            lastTargetPosition = target.transform.position;
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

    public void Stop()
    {
        ClearPath();
    }

    bool CalculatePath(Vector3 position)
    {
        bool pathOk = NavMesh.CalculatePath(transform.position, position, areaMask, path);
        if (pathOk)
        {
            lastSeekedPosition = position;
            nextCorner = (path.corners.Length > 1) ? 1 : 0;
        }else
        {
            ClearPath();
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
        //Debug.Log(target);
        if (target != null)
        {
            SetDestination(target.position);
        }
    }

    internal void SetState(StateBase nextState)
    {
        if (!nextState.enabled)
        {
            foreach (StateBase sb in allStates)
            {
                if (sb.enabled)
                {
                    sb.enabled = false;
                }
            }
            nextState.enabled = true;
        }
    }

    internal bool HasLostTarget()
    {
        return hasLostTarget;
    }

    internal void SetDestinationToLastTargetPosition()
    {
        SetDestination(lastTargetPosition);
    }

    internal Vector3 GetLastTargetPosition()
    {
        return lastTargetPosition;
    }

    internal void NotifyLastTargetPositionVisited()
    {
        hasLostTarget = false;
    }

    internal void RestoreSpeed()
    {
        entityMovement.RestoreSpeed();
    }

    internal void StopMovement()
    {
        entityMovement.StopMovement();
    }


    #region Enchantment State

    [SerializeField] bool isEnchanted;

    public void SetEnchanted(bool value)
    {
        isEnchanted = value;
    }

    internal bool IsEnchanted()
    {
        return isEnchanted;
    }

    #endregion

    #region Confuse State

    [SerializeField] bool isConfused;

    public void SetConfused(bool value)
    {
        isConfused = value;
    }

    internal bool IsConfused()
    {
        return isConfused;
    }

    #endregion

    #region Roaring State

    [SerializeField] bool isRoaring;

    public void SetRoaring(bool value)
    {
        isRoaring = value;
    }

    internal bool IsRoaring()
    {
        return isRoaring;
    }

    #endregion

    #region Roaming State

    [SerializeField] bool isRoaming = true;

    public void SetRoaming(bool value)
    {
        isRoaming = value;
    }

    internal bool IsRoaming()
    {
        return isRoaming;
    }
    #endregion
}

