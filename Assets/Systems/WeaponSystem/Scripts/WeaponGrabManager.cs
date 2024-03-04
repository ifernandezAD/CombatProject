using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class WeaponGrabManager : MonoBehaviour
{
    [SerializeField] Rig leftArmRig;
    [SerializeField] Rig rightArmRig;

    [SerializeField] ParentConstraint leftTarget;
    [SerializeField] ParentConstraint leftHint;
    [SerializeField] ParentConstraint rightTarget;
    [SerializeField] ParentConstraint rightHint;

    Transform currentIkGrabPointsParent;
    EntityWeapons entityWeapons;

    private void Awake()
    {
        entityWeapons = GetComponent<EntityWeapons>();
    }

    private void OnEnable()
    {
        entityWeapons.onChangeWeapon.AddListener(OnChangeWeapon);
    }

    private void LateUpdate()
    {
        if (currentIkGrabPointsParent)
        {
            leftTarget.transform.position = currentIkGrabPointsParent.transform.GetChild(0).position;
            leftHint.transform.position = currentIkGrabPointsParent.transform.GetChild(1).position;
            rightTarget.transform.position = currentIkGrabPointsParent.transform.GetChild(2).position;
            rightHint.transform.position = currentIkGrabPointsParent.transform.GetChild(3).position;

            leftTarget.transform.rotation = currentIkGrabPointsParent.transform.GetChild(0).rotation;
            leftHint.transform.rotation = currentIkGrabPointsParent.transform.GetChild(1).rotation;
            rightTarget.transform.rotation = currentIkGrabPointsParent.transform.GetChild(2).rotation;
            rightHint.transform.rotation = currentIkGrabPointsParent.transform.GetChild(3).rotation;
        }
    }

    void OnChangeWeapon(Weapon weapon)
    {
        if (weapon && weapon.ikGrabPointsParent)
        {
            currentIkGrabPointsParent = weapon.ikGrabPointsParent;

            //Emparentar los sources a los grabpoints

            leftArmRig.weight = 1f;
            rightArmRig.weight = 1f;
        }
        else
        {
            leftArmRig.weight = 0f;
            rightArmRig.weight = 0f;
        }
    }

    private void OnDisable()
    {
        entityWeapons.onChangeWeapon.RemoveListener(OnChangeWeapon);
    }
}
