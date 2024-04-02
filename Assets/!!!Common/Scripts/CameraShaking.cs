using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShakeCamera;

    [SerializeField] private CinemachineFreeLook cinemachineFreeLookCamera;
    [SerializeField] float shakeIntensity = 1f;
    [SerializeField] float shakeTime = 1f;


    private void OnValidate()
    {
        if (debugShakeCamera)
        {
            ShakeCamera(shakeIntensity,shakeTime);
            debugShakeCamera = false;
        }
    }

    private void Awake()
    {
        GetActualVirtualCamera();
    }

    private void OnEnable()
    {
        State_Roaring.onAdversaryRoaring += ShakeCamera;
    }

    void Start()
    {        
        StopShake();
    }

    public void ShakeCamera(float shakeIntensity, float shakeTime)
    {
        Debug.Log("Roaring event listened");
        GetActualVirtualCamera();
        StartCoroutine(DoShake(shakeIntensity,shakeTime));
    }

    private IEnumerator DoShake(float shakeIntensity,float shakeTime)
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

    private void OnDisable()
    {
        State_Roaring.onAdversaryRoaring -= ShakeCamera;
    }


}

