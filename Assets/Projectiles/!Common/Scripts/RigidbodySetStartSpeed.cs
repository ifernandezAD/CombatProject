using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodySetStartSpeed : MonoBehaviour
{
    [SerializeField] Vector3 startSpeed = Vector3.forward * 5f;

    private void Awake()
    {
        GetComponent<Rigidbody>().velocity = startSpeed;
    }
}
