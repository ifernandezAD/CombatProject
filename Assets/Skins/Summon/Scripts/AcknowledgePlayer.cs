using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcknowledgePlayer : MonoBehaviour
{
    public Transform playerPosition { get; private set; }
    private float spellAreaRange = 5f;
    private int layerMask = Physics.DefaultRaycastLayers;

    private void Awake()
    {
        SeekPlayer(); 
    }

    void SeekPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spellAreaRange, layerMask);
 
        foreach (Collider c in colliders)
        {
            if (c.gameObject == gameObject)
                continue;

            if (c.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                playerPosition = playerController.transform;
            }
        }
    }
}
