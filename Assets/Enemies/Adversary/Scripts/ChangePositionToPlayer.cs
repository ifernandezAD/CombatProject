using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionToPlayer : MonoBehaviour
{
    private Transform playerPosition;

    private void Awake()
    {
        playerPosition = GameLogic.instance.playerTransform;
    }

    void Update()
    {
        transform.position = playerPosition.position;
    }
}
