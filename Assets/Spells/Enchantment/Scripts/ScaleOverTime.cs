using DG.Tweening;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    [SerializeField] float scaleSpeed = 1f;
    [SerializeField] float duration = 2f;
    private Vector3 originalScale;

    private void OnEnable()
    {
        originalScale = transform.localScale;
        Vector3 finalScale = originalScale * scaleSpeed;

        transform.DOScale(finalScale, duration).OnComplete(() =>
        {
            transform.localScale = originalScale;
            gameObject.SetActive(false);
        });
    }
}
