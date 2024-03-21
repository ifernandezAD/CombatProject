using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOrientation : MonoBehaviour
{   
    private Transform target;

    private void Start()
    {
        if (PortalManager.instance != null)
        {
           target = PortalManager.instance.portalContainer.GetChild(0).transform;
        }     
    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
