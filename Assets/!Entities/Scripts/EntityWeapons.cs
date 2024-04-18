using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityWeapons : MonoBehaviour
{
    [Header("Weapon Selection")]
    [SerializeField] Transform weaponsParent;
    [SerializeField] int  startingWeaponIndex = -1;

    [Header("Aiming")]
    [SerializeField] Transform weaponsRotation;
    [SerializeField] public Transform aimPoint;

    [Header("Events")]
    [HideInInspector] public UnityEvent<Weapon> onChangeWeapon;
   
    [Header("Debug")]
    [SerializeField] bool debugNextWeapon;
    [SerializeField] bool debugPreviousWeapon;
    [Space(10)]
    [SerializeField] bool debugSelectWeapon;
    [SerializeField] int debugWeaponIndexToSelect;
    [SerializeField] bool debugRemoveWeapon;
    [SerializeField] bool debugRecoverWeapon;

    Weapon[] weapons;
    int previousWeapon;
    int currentWeapon = -1;

    Animator animator;
    RuntimeAnimatorController originalRuntimeAnimatorController;

    private void OnValidate() //Solo funciona en el editor no en runtime, ideal para pruebas
    {
        if (debugNextWeapon)
        {
            SelectNextWeapon();
            debugNextWeapon = false;
        }

        if (debugPreviousWeapon)
        {
            SelectPreviousWeapon();
            debugPreviousWeapon = false;
        }

        if (debugSelectWeapon)
        {
            SelectWeapon(debugWeaponIndexToSelect);
            debugSelectWeapon = false;
        }

        if (debugRemoveWeapon)
        {
            RemoveWeapon();
            debugRemoveWeapon = false;
        }

        if (debugRecoverWeapon)
        {
            RecoverWeapon();
            debugRecoverWeapon = false;
        }
    }

    private void Awake()
    {
        weapons = weaponsParent.GetComponentsInChildren<Weapon>();
        animator = GetComponentInChildren<Animator>();
        originalRuntimeAnimatorController = animator.runtimeAnimatorController;
    }

    private void Start()
    {
        SelectWeapon(startingWeaponIndex);
    }

    private void Update()
    {
        //Aiming system, paused until completed

        //Vector3 aimDirection = aimPoint.position - weaponsRotation.position : transform.forward;
        //Vector3 aimDirectionOnPlayerPlane = Vector3.ProjectOnPlane(aimDirection, transform.right);
        //float verticalAngle = Vector3.SignedAngle(transform.forward, aimDirectionOnPlayerPlane,transform.right);
        //weaponsRotation.localRotation = Quaternion.AngleAxis(verticalAngle, Vector3.right);
    }

    public void SelectWeapon(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(weaponIndex == i);
        }

        if (weaponIndex < -1 || weaponIndex > weapons.Length-1)
        {
            weaponIndex = -1;
        }

        if ((weaponIndex != -1) && (weapons[weaponIndex].animatorForWeapon != null))
        {
            animator.runtimeAnimatorController = weapons[weaponIndex].animatorForWeapon;
        }
        else
        {
            animator.runtimeAnimatorController = originalRuntimeAnimatorController;
        }
        currentWeapon = weaponIndex;


        onChangeWeapon.Invoke(weaponIndex >= 0 ? weapons[currentWeapon] : null);
    }

    public void SelectNextWeapon()
    {
        currentWeapon++;
        int nextWeapon = CycleWeapon(currentWeapon);
        SelectWeapon(nextWeapon);
    }

    public void SelectPreviousWeapon()
    {
        currentWeapon--;
        int previousWeapon = CycleWeapon(currentWeapon);
        SelectWeapon(previousWeapon);
    }

    public int CycleWeapon(int i)
    {
        if (i >= weapons.Length) { i = -1; }
        else if (i < -1) { i = weapons.Length - 1; }
        return i;
    }

    public void MeleeAttack()
    {
        animator.SetTrigger("MeleeAttack");
    }

    public void Shot()
    {
        weapons[currentWeapon].Shot();
    }

    public Weapon GetCurrentWeapon() { return currentWeapon == -1 ? null : weapons[currentWeapon]; }

    internal void StartShooting()
    {
        weapons[currentWeapon].StartContinuousShooting();
    }

    internal void StopShooting()
    {
        weapons[currentWeapon].StopContinuousShooting();
    }

    public void RemoveWeapon()
    {
        previousWeapon = currentWeapon;
        SelectWeapon(-1);
    }

    public void RecoverWeapon()
    {
        SelectWeapon(previousWeapon);
    }
}

