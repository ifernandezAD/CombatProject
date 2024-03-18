using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignPatrolPointWithPlayer : MonoBehaviour
{
    [SerializeField ]private AcknowledgePlayer acknowledgePlayer;
    private PatrolPoint patrolPoint;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -2);

    private void Awake()
    {
        patrolPoint = GetComponent<PatrolPoint>();
    }

    private void Update()
    {
        patrolPoint.transform.position = acknowledgePlayer.playerPosition.position + offset;
    }

}
