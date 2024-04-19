using UnityEngine;

public interface IOffender
{
    enum WeaponType
    {
        fireWeapon,
        meleeWeapon,
        flamesWeapon,
        explosionWeapon
    }

    Transform GetTransform();
    Vector3 GetDirection();
    WeaponType GetWeaponType();
}
