using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using DG.Tweening;

//Command
public class CameraEvent : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] float duration;
    [SerializeField] UnityEvent onCameraEventEnter;
    [SerializeField] UnityEvent onCameraEventExit;

    [Header("Debug")]
    [SerializeField] bool debugPlay;

    static List<CameraEvent> cameraEvents = new();


    private void OnValidate()
    {
        if (debugPlay)
        {
            debugPlay = false;
            Play();
        }
    }

    static void Enqueue(CameraEvent cameraEvent)
    {
        cameraEvents.Add(cameraEvent);
        if (cameraEvents.Count == 1) { cameraEvent.CameraEventIn(); }
    }

    static void Deque()
    {
        cameraEvents.RemoveAt(0);
        if (cameraEvents.Count > 0) { cameraEvents[0].CameraEventIn(); }
    }

    private void Awake()
    {
        camera.gameObject.SetActive(false);
    }

    public void Play()
    {
        Enqueue(this);
    }

    protected virtual void CameraEventIn()
    {
        camera.gameObject.SetActive(true);
        DOVirtual.DelayedCall(duration, CameraEventOut);
    }

    protected virtual void CameraEventOut()
    {
        camera.gameObject.SetActive(false);
        Deque();
    }
}
