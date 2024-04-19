using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealthFeedback : MonoBehaviour
{
    public Volume volume;
    private EntityLife entityLife;

    private void Awake()
    {
        entityLife = GetComponent<EntityLife>();
    }

    private void OnEnable()
    {
        entityLife.onLifeChanged.AddListener(UpdatePostProcessingVolume);
    }

    void UpdatePostProcessingVolume(float lifePercentage)
    {
        volume.weight = Mathf.Clamp01(1f - lifePercentage);
    }

    private void OnDisable()
    {
        entityLife.onLifeChanged.RemoveListener(UpdatePostProcessingVolume);
    }
}
