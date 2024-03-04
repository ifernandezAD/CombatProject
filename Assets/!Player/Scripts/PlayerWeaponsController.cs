using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerWeaponsController : MonoBehaviour
{
    [Header("WeaponSelection")]
    [SerializeField] InputActionReference changeWeapons;
    [SerializeField] InputActionReference[] selectWeapons;

    [Header("Attack")]
    [SerializeField] InputActionReference primaryAttack;
    [SerializeField] InputActionReference secondaryAttack;

    EntityWeapons entityWeapons;


    private void Awake()
    {
        entityWeapons = GetComponent<EntityWeapons>();
    }

    private void OnEnable()
    {
        primaryAttack.action.Enable();
        secondaryAttack.action.Enable();
        changeWeapons.action.Enable();
        foreach (InputActionReference iar in selectWeapons)
        {
            iar.action.Enable();
        }
    }

    private void Update()
    {
        UpdateWeaponSelection();
        UpdateAttack();
    }

    void UpdateWeaponSelection()
    {
        Vector2 changeWeaponValue = changeWeapons.action.ReadValue<Vector2>();

        if (changeWeaponValue.y > 0f)
        {
            entityWeapons.SelectNextWeapon();
        }
        else if (changeWeaponValue.y < 0f)
        {
            entityWeapons.SelectPreviousWeapon();
        }
        for (int i = 0; i < selectWeapons.Length; i++)
        {
            if (selectWeapons[i].action.WasPerformedThisFrame())
            {
                entityWeapons.SelectWeapon(i == 0 ? -1 : i - 1);
            }
        }
    }

    bool primaryAttackWasPressed = false;
    bool secondaryAttackWasPressed = false;

    void UpdateAttack()
    {
        if (entityWeapons.GetCurrentWeapon())
        {
            UpdateWeapon(entityWeapons.GetCurrentWeapon().GetPrimaryAttackType(), primaryAttack, primaryAttackWasPressed);
            UpdateWeapon(entityWeapons.GetCurrentWeapon().GetSecondaryAttackType(), secondaryAttack, secondaryAttackWasPressed);
        }

        primaryAttackWasPressed = primaryAttack.action.IsPressed();
        secondaryAttackWasPressed = secondaryAttack.action.IsPressed();
    }

    private void UpdateWeapon(Weapon.AttackType attackType, InputActionReference inputAction, bool actionWasPressed)
    {
        switch (attackType)
        {
            case Weapon.AttackType.None:
                break;
            case Weapon.AttackType.Melee:
                if (inputAction.action.WasPerformedThisFrame()) { entityWeapons.MeleeAttack(); }
                break;
            case Weapon.AttackType.Shot:
                if (inputAction.action.WasPerformedThisFrame()) { entityWeapons.Shot(); }
                break;
            case Weapon.AttackType.Burst:
                if (inputAction.action.WasPerformedThisFrame())
                {
                    //?????
                }
                break;
            case Weapon.AttackType.ContinousShot:
                if (inputAction.action.IsPressed() != actionWasPressed)
                {
                    if (inputAction.action.IsPressed())
                    {
                        entityWeapons.StartShooting();
                    }
                    else
                    {
                        entityWeapons.StopShooting();
                    }
                }
                break;
        }
    }

    private void OnDisable()
    {
        primaryAttack.action.Disable();
        secondaryAttack.action.Disable();
        changeWeapons.action.Disable();
        foreach (InputActionReference iar in selectWeapons)
        {
            iar.action.Disable();
        }
    }
}
