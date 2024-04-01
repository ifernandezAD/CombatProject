using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Roaring : StateBase
{
    [Header("EnchantmentAnimations")]
    private readonly int roaringHash = Animator.StringToHash("Roaring");
    [SerializeField] float animationDuration = 3f;

    private void OnEnable()
    {
        ai.StopEntity(true);
        PlayRoaringAnimation();
        
        DOVirtual.DelayedCall(animationDuration, EndRoaring);
    }


    private void PlayRoaringAnimation()
    {
        ai.animator.SetTrigger(roaringHash);
    }

    private void ChangeWeapon()
    {
        int randomWeapon = UnityEngine.Random.Range(0, 2);

        ai.entityWeapons.SelectWeapon(randomWeapon);
    }

    private void EndRoaring()
    {
        ChangeWeapon();
        ai.SetRoaring(false);
        ai.StopEntity(false);
    }
}
