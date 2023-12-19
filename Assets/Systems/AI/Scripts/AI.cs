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

    NavMeshPath path;
    EntityMovement entityMovement;

    Vector3 lastSeekedPosition = Vector3.zero;

    private void Awake()
    {
        entityMovement = GetComponent<EntityMovement>();
        path = new();
    }

    void Update()
    {
        if (target)
        {
            if (Vector3.Distance(lastSeekedPosition, target.position) > pathCalculationThresoldDistance)
            {
                CalculatePath();
            }
        }
        else if (HasPath())
        {
            ClearPath();
        }

        Vector3 xzPlaneVelocity = FollowPath();
        entityMovement.Orientate(xzPlaneVelocity);
        entityMovement.Animate();
    }

    bool CalculatePath()
    {
        bool pathOk = NavMesh.CalculatePath(transform.position, target.position, areaMask, path);
        if (pathOk)
        {
            lastSeekedPosition = target.position;
            nextCorner = 0;
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


}
