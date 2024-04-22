using UnityEngine;

public class EntityParticles : MonoBehaviour
{
    private HurtCollider hurtCollider;

    [Header("Particles")]
    [SerializeField] GameObject bloodParticle;
    [SerializeField] GameObject meleeParticle;
    [SerializeField] GameObject fireParticle;

    private void Awake()
    {
        hurtCollider = GetComponent<HurtCollider>();
    }

    private void OnEnable()
    {
        hurtCollider.onHitWithOffender.AddListener(InstantiateParticle);
    }

    private void InstantiateParticle(IOffender offender)
    {
        Vector3 spawnPosition = transform.position + transform.up;

        if ((offender.GetWeaponType() == IOffender.WeaponType.fireWeapon) || (offender.GetWeaponType() == IOffender.WeaponType.explosionWeapon))
        {
            GameObject bloodParticleClone = Instantiate(bloodParticle, spawnPosition, Quaternion.identity);
            Destroy(bloodParticleClone, 1);
        }

        if (offender.GetWeaponType() == IOffender.WeaponType.meleeWeapon)
        {
            GameObject meleeParticleClone = Instantiate(meleeParticle, spawnPosition, Quaternion.identity);
            Destroy(meleeParticleClone, 1);
        }

        if (offender.GetWeaponType() == IOffender.WeaponType.flamesWeapon)
        {
            GameObject flamesParticleClone = Instantiate(fireParticle, spawnPosition, Quaternion.identity);
            Destroy(flamesParticleClone, 1);
        }

    }

    private void OnDisable()
    {
        hurtCollider.onHitWithOffender.RemoveListener(InstantiateParticle);
    }

}
