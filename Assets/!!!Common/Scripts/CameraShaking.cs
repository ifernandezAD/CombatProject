using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShakeCamera;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeIntensity = 1f;
    private float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin cbmcp;


    private void OnValidate()
    {
        if (debugShakeCamera)
        {
            ShakeCamera();
            debugShakeCamera = false;
        }
    }

    private void Awake()
    {
    }

    void Start()
    {
        GetActualVirtualCamera();
        StopShake();
    }

    public void ShakeCamera()
    {
        GetActualVirtualCamera();
        StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        CinemachineBasicMultiChannelPerlin cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = shakeIntensity;

        yield return new WaitForSeconds(shakeTime);

        StopShake();
    }

    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = 0f;

        timer = 0;
    }

    public void GetActualVirtualCamera()
    {
        cinemachineVirtualCamera =
            Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();      
    }
}
