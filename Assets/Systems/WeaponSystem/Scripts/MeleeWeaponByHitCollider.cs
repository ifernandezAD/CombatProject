using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeleeWeaponByHitCollider : MeleeWeapon
{

    [SerializeField] Transform hitCollidersParent;

    protected override void InternalStart()
    {
        foreach (Transform t in hitCollidersParent){t.gameObject.SetActive(false);}
    }

    public override void NotifyMeleeAttack(string collidersToActivate)
    {
        string[] colliderNames = collidersToActivate.Split(' ');
        foreach (string s in colliderNames)
        {
            Transform collider = hitCollidersParent.Find(s);
            if (collider)
            {
                collider.gameObject.SetActive(true);
                DOVirtual.DelayedCall(hitDuration, () => collider.gameObject.SetActive(false));
            }
        }

        PlayWeaponSound();
    }
}
