using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleOnHit : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Senseable>().allegiance == "Adversary")
        {
            ApplyRandomForce();
        }
    }

    private void ApplyRandomForce()
    {
        throw new NotImplementedException();
    }
}
