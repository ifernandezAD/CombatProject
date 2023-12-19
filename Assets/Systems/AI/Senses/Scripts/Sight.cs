using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [SerializeField] Vector3 sightSize = new Vector3(10f,10f,30f);

    public class Senseable
    {
        public Transform transform;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * sightSize.z / 2f, sightSize / 2f, 
            transform.rotation);
    }
}
