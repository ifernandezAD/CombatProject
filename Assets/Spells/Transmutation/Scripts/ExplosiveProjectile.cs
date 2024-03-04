using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    [SerializeField] QuadraticCurve curve;
    [SerializeField] Explosion explosion;
    [SerializeField] MeshRenderer beerMesh;
    [SerializeField] float speed;

    private float sampleTime;

    void Start()
    {
        sampleTime = 0f;
    }

    void Update()
    {
        sampleTime += Time.deltaTime * speed;
        transform.position = curve.Evaluate(sampleTime);
        transform.forward = curve.Evaluate(sampleTime + 0.001f) - transform.position;

        if (sampleTime >= 1f)
        {
            beerMesh.enabled = false;
            explosion.enabled = true;
        }
    }
}
