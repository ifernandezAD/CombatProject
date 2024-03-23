using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodySetStartSpeed : MonoBehaviour
{
    [SerializeField] float speed = 5f; 

    private void Awake()
    {
        Vector3 localForwardSpeed = transform.forward * speed;

        GetComponent<Rigidbody>().velocity = localForwardSpeed;
    }
}
