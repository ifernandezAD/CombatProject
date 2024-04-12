using DG.Tweening;
using UnityEngine;

public class DemonDeath : Death
{
    Material material;

    [SerializeField] float dissolveDuration = 2f;

    protected override void InternalAwake()
    {
        base.InternalAwake();

        Renderer renderer = GetComponentInChildren<Renderer>();
        material = renderer.material;
    }

    protected override void ManageDeath()
    {
        DissolveEntity();
    }

    private void DissolveEntity()
    {
        float currentDissolve = material.GetFloat("_Dissolve");

        Tween dissolveTween = DOTween.To(() => currentDissolve, x => currentDissolve = x, 1f, dissolveDuration)
            .OnUpdate(() => material.SetFloat("_Dissolve", currentDissolve))
            .SetEase(Ease.Linear); 

        dissolveTween.OnComplete(() => Destroy(transform.parent.gameObject));
    }

}
