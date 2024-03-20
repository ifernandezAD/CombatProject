using DG.Tweening;
using UnityEngine;

public class DemonDeath : MonoBehaviour
{
    EntityLife entityLife;
    Material material;

    [SerializeField] float dissolveDuration = 2f;

    private void Awake()
    {
        entityLife = GetComponent<EntityLife>();

        Renderer renderer = GetComponentInChildren<Renderer>();
        material = renderer.material;
    }

    private void OnEnable()
    {
        entityLife.onLifeDepleted.AddListener(ManageDemonDeath);
    }

    private void ManageDemonDeath()
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

    private void OnDisable()
    {
        entityLife.onLifeDepleted.RemoveListener(ManageDemonDeath);
    }
}
