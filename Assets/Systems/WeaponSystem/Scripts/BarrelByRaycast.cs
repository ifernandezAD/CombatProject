using UnityEngine;

public class BarrelByRaycast : Barrel, IOffender
{
    [Header("Behaviour")]
    [SerializeField] float range = 30f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    [Header("Visuals")]
    [SerializeField] GameObject shotTrailPrefab;

    Vector3 hitDirection = Vector3.zero;

    public override void Shot(Vector3 direction)
    {
        Vector3 finalShotPoint;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, range, layerMask))
        {
            hitDirection = direction;
            hit.collider.GetComponent<HurtCollider>()?.NotifyHit(this);
            finalShotPoint = hit.point;
        }
        else
        {
            finalShotPoint = transform.position + direction * range;
        }

        if (shotTrailPrefab)
        {
            GameObject shotTrailGO = Instantiate(shotTrailPrefab);
            ShotTrail shotTrail = shotTrailGO.GetComponent<ShotTrail>();
            shotTrail.Init(transform.position, finalShotPoint);
        }
    }

    public override void StartContinuousShooting() { }
    public override void StopContinuousShooting() { }

    public Vector3 GetDirection()
    {
        return hitDirection;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
