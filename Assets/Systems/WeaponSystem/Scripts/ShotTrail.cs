using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShotTrail : MonoBehaviour
{
    [SerializeField] float duration = 0.25f;
    LineRenderer lineRenderer;
    Tween tween;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Init(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Vector3.Lerp(startPoint, endPoint, (float)i / (float)lineRenderer.positionCount);
        }
        lineRenderer.SetPositions(positions);
        tween = DOTween.To(() => lineRenderer.widthMultiplier, (x) => lineRenderer.widthMultiplier = x, 0f, duration);

        Destroy(gameObject, duration);
    }

    private void OnDestroy()
    {
        tween.Kill(false);
    }
}
