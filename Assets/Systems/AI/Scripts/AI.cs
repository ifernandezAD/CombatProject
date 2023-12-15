using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target)
        {
            navMeshAgent.destination = target.position;
        }
        else
        {
            navMeshAgent.destination = transform.position;
        }
    }
}
