using System.Collections;
using UnityEngine;

public class MeleeWeaponByRaycast : MeleeWeapon, IOffender
{
    [SerializeField] float maxPositionsDistance = 0.05f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    [SerializeField] Transform rayDefinitionsParent;

    Vector3 hitDirection = Vector3.zero;

    protected override void InternalStart()
    {
       //Nothing yet
    }

    public override void NotifyMeleeAttack(string itemsToActivate)
    {
        string[] itemNames = itemsToActivate.Split(' ');

        foreach (string s in itemNames)
        {
            StartCoroutine(PerformWeaponRaycasting(s));
        }
    }

    IEnumerator PerformWeaponRaycasting(string item)
    {
        float startTime = Time.time;
        Transform rayDefinition = rayDefinitionsParent.Find(item);
        Transform start = rayDefinition.GetChild(0);
        Transform end = rayDefinition.GetChild(1);

        Vector3 oldStartPosition = start.position;
        Vector3 oldEndPosition = end.position;

        while ((Time.time - startTime) < hitDuration)
        {
            yield return null;
            Vector3 currentStartPosition = start.position;
            Vector3 currentEndPosition = end.position;

            int startCuts = Mathf.CeilToInt((oldStartPosition - currentStartPosition).magnitude / maxPositionsDistance);
            int endCuts = Mathf.CeilToInt((oldEndPosition - currentEndPosition).magnitude / maxPositionsDistance);
            int numberOfCuts = Mathf.Max(startCuts, endCuts);

            for (int i = 0; i < numberOfCuts; i++)
            {
                float t = (float)i / (float)numberOfCuts;
                Vector3 startPosition = Vector3.Lerp(oldStartPosition,currentStartPosition, t);
                Vector3 endPosition = Vector3.Lerp(oldEndPosition, currentEndPosition, t);
                Vector3 direction = endPosition - startPosition;
                if (Physics.Raycast(startPosition, direction, out RaycastHit hit, direction.magnitude, layerMask))
                {
                    hitDirection = endPosition - oldStartPosition;
                    HurtCollider hurtCollider = hit.collider.GetComponent<HurtCollider>();
                    hurtCollider?.NotifyHit(this);
                }
            }
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Vector3 GetDirection()
    {
        return hitDirection;
    }


    public IOffender.WeaponType GetWeaponType()
    {
        return IOffender.WeaponType.meleeWeapon;
    }
}
