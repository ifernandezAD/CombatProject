using UnityEngine;

public class AttackEventForwarder : MonoBehaviour
{
    [SerializeField] GameObject meleeMagicParticle;
    [SerializeField] Transform meleeMagicParticlePosition;

    MeleeWeaponController meleeWeaponController;
    AI ai;

    private void Awake()
    {
        meleeWeaponController = GetComponentInParent<MeleeWeaponController>();
        ai = GetComponentInParent<AI>();
    }

    void Attack(string collidersToActivate)
    {
        meleeWeaponController.Attack(collidersToActivate);
    }

    void Roar()
    {
        ai.SetRoaring(true);
    }

    void InstantiateMagicParticle()
    {
       GameObject meleeMagiParticleClone= Instantiate(meleeMagicParticle, meleeMagicParticlePosition.position, meleeMagicParticlePosition.rotation);
       Destroy(meleeMagiParticleClone, 1);
    }
}
