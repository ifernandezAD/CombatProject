using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeField : MonoBehaviour
{
    internal void NotifyCubeClicked(CubeInTheField cubeInTheField)
    {
        Transform cube1 = transform.GetChild(0);
        Transform cube2 = transform.GetChild(1);
        Transform cube3 = transform.GetChild(2);

        DOTween.Sequence().
            Append(cube1.DOLocalJump(Vector3.zero, 3f, 3, 3f).SetRelative(true).SetEase(Ease.Linear)).
            Append(cube2.DOLocalMoveY(3f, 1.5f).SetRelative(true).SetEase(Ease.OutElastic)).
            Append(cube2.DOLocalMoveY(-3f, 1.5f).SetRelative(true).SetEase(Ease.OutElastic)).
            AppendCallback(() => Debug.Log("Adieu")).
            Append(cube2.DOLocalMoveY(-3, 1.5f).SetRelative(true).SetEase(Ease.OutElastic)).
            AppendCallback(DebugHola).
            Insert(2f, cube3.DORotate(Vector3.up * 720f, 1f,RotateMode.FastBeyond360)).
            OnComplete(() => Debug.Log("Termine!"));


        //Llamar una función cuando pase un tiempo determinado

    }

    void DebugHola()
    {
        Debug.Log("Hola");
    }
}
