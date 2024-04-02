using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShakeCamera;

    [SerializeField] private CinemachineFreeLook cinemachineFreeLookCamera;
    private float shakeIntensity = 1f;
    private float shakeTime = 0.2f;


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
        GetActualVirtualCamera();
    }

    void Start()
    {        
        StopShake();
    }

    public void ShakeCamera()
    {
        GetActualVirtualCamera();
        StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        CinemachineBasicMultiChannelPerlin[] perlinModules = cinemachineFreeLookCamera.GetComponentsInChildren<CinemachineBasicMultiChannelPerlin>();
        foreach (CinemachineBasicMultiChannelPerlin cbmcp in perlinModules)
        {
            cbmcp.m_AmplitudeGain = shakeIntensity;
        }

        yield return new WaitForSeconds(shakeTime);

        StopShake();
    }

    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin[] perlinModules = cinemachineFreeLookCamera.GetComponentsInChildren<CinemachineBasicMultiChannelPerlin>();

        foreach (CinemachineBasicMultiChannelPerlin cbmcp in perlinModules)
        {
            cbmcp.m_AmplitudeGain = 0f;
        }
    }

    public void GetActualVirtualCamera()
    {
        cinemachineFreeLookCamera = null; 
        foreach (Transform child in transform)
        {
            CinemachineFreeLook childFreelookCamera = child.GetComponent<CinemachineFreeLook>();
            if (childFreelookCamera != null)
            {
                if (childFreelookCamera.gameObject.activeInHierarchy)
                {
                    cinemachineFreeLookCamera = childFreelookCamera;
                    break; 
                }
            }
        }

        if (cinemachineFreeLookCamera == null)
        {
            Debug.LogWarning("No active Cinemachine Virtual Camera found among child GameObjects.");
        }
    }
}

