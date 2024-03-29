using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Roaring : StateBase
{
    [Header("EnchantmentAnimations")]
    private readonly int roaringHash = Animator.StringToHash("roaring");

    private void OnEnable()
    {
        PlayRoaringAnimation();
        ai.StopEntity(true);
    }

    private void PlayRoaringAnimation()
    {
        ai.animator.SetTrigger(roaringHash);
    }
}
