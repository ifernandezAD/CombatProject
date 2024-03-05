using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using DG.Tweening;

//Command Pattern
public class CameraEvent : MonoBehaviour
{
    static List<CameraEvent> cameraEvents = new();


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

    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] float duration;
    [SerializeField] UnityEvent onCameraEventIn;
    [SerializeField] UnityEvent onCameraEventOut;


    [Header("Debug")]
    [SerializeField] bool debugPlay;


    private void OnValidate()
    {
        if (debugPlay)
        {
            debugPlay = false;
            Play();
        }
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
        onCameraEventIn?.Invoke();
        DOVirtual.DelayedCall(duration, CameraEventOut);
    }

    protected virtual void CameraEventOut()
    {
        camera.gameObject.SetActive(false);
        onCameraEventOut?.Invoke();
        Deque();
    }
}
