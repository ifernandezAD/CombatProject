using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudibleByCollision : AudibleBase
{
    [Header("Collision Emission")]
    [SerializeField] string[] collidingTags = { "Untagged" };
    [SerializeField] float range = 10f;


    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
    }

    void CheckCollision(Collider collider)
    {
        if (collidingTags.Contains(collider.tag))
        {
            InternalEmit(range);
        }
    }
}
