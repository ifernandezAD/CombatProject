using UnityEngine;

public class ExplodeWhenHitAgainstCollider : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    HitCollider hitCollider;

    private void Awake()
    {
        hitCollider = GetComponent<HitCollider>();
    }

    private void OnEnable()
    {
        hitCollider.onHitAgainstCollider.AddListener(OnHitAgainstCollider);
    }

    private void OnDisable()
    {
        hitCollider.onHitAgainstCollider.RemoveListener(OnHitAgainstCollider);
    }

    void OnHitAgainstCollider(HitCollider hitCollider, Collider collider)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
