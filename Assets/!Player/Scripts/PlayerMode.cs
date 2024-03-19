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

        public CinemachineVirtualCameraBase cameraBase;

        public GameObject[] modeExclusiveObjects;

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
                pmp.debugActivate = false;
                SetPlayerModeProfile(pmp);
            }
        }
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    PlayerModeProfile currentActiveProfile;
    public void SetPlayerModeProfile(PlayerModeProfile profile)
    {
        if (currentActiveProfile != null)
        {
            foreach (GameObject go in currentActiveProfile.modeExclusiveObjects)
            {
                go.SetActive(false);
            }
        }

        playerController.SetMovementMode(profile.movementMode);
        playerController.SetOrientationMode(profile.orientationMode,profile.orientationTarget);

        previousCamera?.gameObject.SetActive(false);
        profile.cameraBase?.gameObject.SetActive(true);
        previousCamera = profile.cameraBase;

        foreach (GameObject go in profile.modeExclusiveObjects)
        {
            go.SetActive(true);
        }
        currentActiveProfile = profile;
    }
}
