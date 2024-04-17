using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Roaring : StateBase
{
    [Header("RoarAudio")]
    [SerializeField] private SimpleAudioEvent roarAudioEvent;
    private AudioSource audioSource;
    [SerializeField] float roarSoundDelay;


    [Header("CameraShake")]
    [SerializeField] float roareventDelay = 1f;
    [SerializeField] float shakeIntensity = 1f;
    [SerializeField] float shakeTime = 1f;
    public static Action<float, float> onAdversaryRoaring;

    [Header("EnchantmentAnimations")]
    [SerializeField] float animationDuration = 3f;
    private readonly int roaringHash = Animator.StringToHash("Roaring");


    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();

        ai.StopMovement();
        PlayRoaringAnimation();

        DOVirtual.DelayedCall(roareventDelay, SendRoaringEvent);
        DOVirtual.DelayedCall(roarSoundDelay, PlayRoarSound);
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

    void PlayRoarSound() { roarAudioEvent.Play(audioSource); }

    private void EndRoaring()
    {
        ChangeWeapon();
        ai.SetRoaring(false);
        ai.RestoreSpeed();
    }
}
