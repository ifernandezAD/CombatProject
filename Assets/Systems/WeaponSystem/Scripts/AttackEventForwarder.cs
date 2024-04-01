using UnityEngine;

public class AttackEventForwarder : MonoBehaviour
{
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
}
