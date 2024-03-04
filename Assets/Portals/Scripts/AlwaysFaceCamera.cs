using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{
    void Update()
    {        
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        Vector3 flattenedDirection = Vector3.ProjectOnPlane(directionToCamera, Vector3.up);

        transform.rotation = Quaternion.LookRotation(flattenedDirection, Vector3.up);
    }
}
