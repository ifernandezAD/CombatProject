using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdollizer : MonoBehaviour
{
    Collider[] colliders;
    Rigidbody[] rigidbodys;

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        rigidbodys = GetComponentsInChildren<Rigidbody>();
    }

    public void UnRagdollize()
    {
        foreach (Collider c in colliders) { c.enabled = false; }
        foreach(Rigidbody rb in rigidbodys) { rb.isKinematic = true; }
    }

    public void Ragdollize()
    {
        Ragdollize(Vector3.zero);
    }

    public void Ragdollize(Vector3 pushDirection, float minPushForce =0f, float maxPushForce=0f)
    {
        foreach (Collider c in colliders) { c.enabled = true; }
        foreach (Rigidbody rb in rigidbodys) 
        { 
            rb.isKinematic = false;
            rb.WakeUp();
            rb.AddForce(pushDirection.normalized * Random.Range(minPushForce, maxPushForce));
        }
    }
}
