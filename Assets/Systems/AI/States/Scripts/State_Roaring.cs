using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Roaring : StateBase
{
    [Header("CameraShake")]
    [SerializeField] float roarDelay = 1f;
    [SerializeField] float shakeIntensity = 1f;
    [SerializeField] float shakeTime = 1f;
    public static Action<float, float> onAdversaryRoaring;

    [Header("EnchantmentAnimations")]
    [SerializeField] float animationDuration = 3f;
    private readonly int roaringHash = Animator.StringToHash("Roaring");


    private void OnEnable()
    {
        ai.StopMovement();
        PlayRoaringAnimation();

        DOVirtual.DelayedCall(roarDelay, SendRoaringEvent);
        DOVirtual.DelayedCall(animationDuration, EndRoaring);
    }

    private void PlayRoaringAnimation()
    {
        ai.animator.SetTrigger(roaringHash);
    }

    private void SendRoaringEvent()
    {
        onAdversaryRoaring?.Invoke(shakeIntensity, shakeTime);
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
        ai.RestoreSpeed();
    }
}
