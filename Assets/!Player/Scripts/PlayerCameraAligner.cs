using Cinemachine;
using System.Linq;
using UnityEngine;

public class PlayerCameraAligner : MonoBehaviour
{
    CinemachineFreeLook freeLook;
    Transform mainCamera;

    [Header("Movement")]
    [SerializeField] float angularSpeed = 360f;

    public enum UpdateMode
    {
        Always,
        OnlyOnActivation,
    }

    [Header("Update Mode")]
    [SerializeField] UpdateMode updateMode;
    [SerializeField] CinemachineBrain cinemachineBrain;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        freeLook = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        if (updateMode == UpdateMode.OnlyOnActivation)
        {
            if (updateMode == UpdateMode.Always)
            { PerformRotation(Mathf.Infinity); }
        }
    }

    private void Update()
    {
        if (updateMode == UpdateMode.Always)
        { PerformRotation(angularSpeed); }
    }

    private void PerformRotation(float angularSpeed)
    {
        Vector3 mainCameraForwardOnPlane = Vector3.ProjectOnPlane(mainCamera.forward, Vector3.up);
        Vector3 freelookCameraForward = freeLook.LookAt.position - freeLook.transform.position;
        Vector3 freelookCameraForwardOnPlane = Vector3.ProjectOnPlane(freelookCameraForward, Vector3.up);

        float angularDistance = Vector3.SignedAngle(mainCameraForwardOnPlane, freelookCameraForwardOnPlane, Vector3.up);
        freeLook.m_XAxis.Value = -Mathf.Sign(angularDistance) * Mathf.Min(Mathf.Abs(angularDistance), angularSpeed * Time.deltaTime);
    }
}
