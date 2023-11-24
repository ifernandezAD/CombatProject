using UnityEngine;

public class AttackEventForwarder : MonoBehaviour
{
    MeleeWeaponController meleeWeaponController;

    private void Awake()
    {
        meleeWeaponController = GetComponentInParent<MeleeWeaponController>();
    }

    void Attack(string collidersToActivate)
    {
        meleeWeaponController.Attack(collidersToActivate);
    }
}
