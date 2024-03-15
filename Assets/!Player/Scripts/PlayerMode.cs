using Cinemachine;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    [System.Serializable]
    public class PlayerModeProfile
    {
        public string name;
        public PlayerController.MovementMode movementMode;
        public PlayerController.OrientationMode orientationMode;

        public Transform orientationTarget;
        public Transform orientationCamera;

        public CinemachineVirtualCameraBase cameraBase;

        public bool debugActivate;
    }

    PlayerController playerController;
    CinemachineVirtualCameraBase previousCamera;

    [Header("Debug")]
    [SerializeField] PlayerModeProfile[] debugPlayerModeProfiles;

    private void OnValidate()
    {
        foreach (PlayerModeProfile pmp in debugPlayerModeProfiles)
        {
            if (pmp.debugActivate)
            {

            }
        }
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void SetPlayerModeProfile(PlayerModeProfile profile)
    {
        playerController.SetMovementMode(profile.movementMode);
        playerController.SetOrientationMode(profile.orientationMode,profile.orientationTarget);


        previousCamera?.gameObject.SetActive(false);
        profile.cameraBase?.gameObject.SetActive(true);
        previousCamera = profile.cameraBase;
    }

}
