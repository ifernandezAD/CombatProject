using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEditor;
using System;

public class PlayerController : EntityBase
{
    public enum MovementMode
    {
        PlayerDirection,
        CameraDirection,
    }

    public enum OrientationMode
    {
        CameraForward,
        MovementForward,
        LookToTarget,
    }

    [Header("Movement")]
    [SerializeField] MovementMode movementMode = MovementMode.PlayerDirection;
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference run;
    
    [Header("Vertical Movement")] 
    [SerializeField] InputActionReference jump;

    [Header("Dash")]
    [SerializeField] InputActionReference dash;

    [SerializeField] EntityMovement.ExtraMovementInfo dashMovementInfo;

    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.MovementForward; 
    [SerializeField] Transform target;

    [Header("Aiming")]
    [SerializeField] InputActionReference aim;

    [Header("Events")]
    public UnityEvent<PlayerController> onPlayerDeath;

    [Header("Player Modes Profiles")]
    PlayerMode playerMode;
    [SerializeField] PlayerMode.PlayerModeProfile defaultMovement;
    [SerializeField] PlayerMode.PlayerModeProfile aimingProfile;
    [SerializeField] PlayerMode.PlayerModeProfile targetLockProfile;

    enum PlayerControllerMode
    {
        Default,
        Aiming,
        TargetLock,
    };
    PlayerControllerMode currentPlayerControllerMode;

    PlayerTargetSelector targetSelector;


    protected override void ChildAwake()
    {
        playerMode = GetComponent<PlayerMode>();
        targetSelector = GetComponent<PlayerTargetSelector>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        dash.action.Enable();
        run.action.Enable();
        aim.action.Enable();

        targetSelector.onTargetAcquired.AddListener(OnTargetAcquired);
        targetSelector.onTargetReleased.AddListener(OnTargetReleased);
    }


    protected override void ChildStart()
    {
        playerMode.SetPlayerModeProfile(defaultMovement);

        SetPlayerControllerMode(PlayerControllerMode.Default);
    }

    void Update()
    {
        UpdateAiming();
        UpdateRunning();

        Vector3 direction = CalculateDirectionFromInput();
        bool mustJump = jump.action.WasPerformedThisFrame();
        Vector3 xzPlaneVelocity = entityMovement.Move(direction, mustJump);

        if (dash.action.WasPerformedThisFrame()) { entityMovement.ApplyLocalExtraMovement(Vector3.forward ,dashMovementInfo) ; }

        Vector3 orientationDirection = CalculateOrientationFromSettings(xzPlaneVelocity);
        entityMovement.Orientate(orientationDirection);

        entityMovement.Animate();
    }

    private void UpdateRunning()
    {
        if (run.action.WasPressedThisFrame())
        {
            entityMovement.SetToRun();
        }

        if (run.action.WasReleasedThisFrame())
        {
            entityMovement.RestoreSpeed();    
        }
    }

    bool isAiming = false;
    bool isTargetLocking = false;
    bool isAimingAtTarget = false;
    private void UpdateAiming()
    {
        if (aim.action.WasPressedThisFrame())
        {  
            isAiming = true;
        }

        if (aim.action.WasReleasedThisFrame())
        {
            
            isAiming = false;
        }

        targetLockProfile.orientationTarget = acquiredTarget;
        if (isAiming)
        {
            if (acquiredTarget)
            {
                
                SetPlayerControllerMode(PlayerControllerMode.TargetLock);
            }
            else
            {
                SetPlayerControllerMode(PlayerControllerMode.Aiming);
            }
        }
        else
        {
            SetPlayerControllerMode(PlayerControllerMode.Default);
        }
    }

    Transform acquiredTarget;
    private void OnTargetAcquired(Transform target)
    {
        acquiredTarget = target;
    }

    private void OnTargetReleased(Transform target)
    {
        acquiredTarget = null;
    }

    private Vector3 CalculateOrientationFromSettings(Vector3 xzPlaneVelocity)
    {
        Vector3 orientationDirection = Vector3.zero;

        switch (orientationMode)
        {
            case OrientationMode.CameraForward:
                orientationDirection = Camera.main.transform.forward;
                break;
            case OrientationMode.MovementForward:
                orientationDirection = xzPlaneVelocity;
                break;
            case OrientationMode.LookToTarget:
                orientationDirection = target.position - transform.position;
                orientationDirection.y = 0;
                break;
        }

        return orientationDirection;
    }

    private Vector3 CalculateDirectionFromInput()
    {
        Vector2 rawMoveValue = move.action.ReadValue<Vector2>();
        Vector3 xzPlanetMovement = (Vector3.right * rawMoveValue.x) + (Vector3.forward * rawMoveValue.y);
        Vector3 xzDirection = Vector3.zero;

        switch (movementMode)
        {
            case MovementMode.PlayerDirection:
                xzDirection = xzPlanetMovement;
                break;
            case MovementMode.CameraDirection:
                {
                    Transform mainCameraTransform = Camera.main.transform;
                    Vector3 xzPlanetMovementForCamera = mainCameraTransform.TransformDirection(xzPlanetMovement);
                    float oldMagnitude = xzPlanetMovementForCamera.magnitude;

                    xzPlanetMovementForCamera = Vector3.ProjectOnPlane(xzPlanetMovementForCamera, Vector3.up);
                    xzPlanetMovementForCamera = xzPlanetMovementForCamera.normalized * oldMagnitude;

                    xzDirection = xzPlanetMovementForCamera;
                }
                break;
        }

        return xzDirection;
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        dash.action.Disable();
        run.action.Disable();
        aim.action.Disable();

        targetSelector.onTargetAcquired.RemoveListener(OnTargetAcquired);
        targetSelector.onTargetReleased.RemoveListener(OnTargetReleased);
    }

    

    internal void SetMovementMode(MovementMode movementMode)
    {
        this.movementMode = movementMode;
    }


    internal void SetOrientationMode(OrientationMode orientationMode, Transform orientationTarget)
    {
        this.orientationMode = orientationMode;
        this.target = orientationTarget;
    }


    void SetPlayerControllerMode(PlayerControllerMode mode)
    {
        if (currentPlayerControllerMode != mode)
        {         
            switch (mode)
            {
                case PlayerControllerMode.Default: playerMode.SetPlayerModeProfile(defaultMovement); break;
            
                case PlayerControllerMode.Aiming: playerMode.SetPlayerModeProfile(aimingProfile); break;
            
                case PlayerControllerMode.TargetLock: playerMode.SetPlayerModeProfile(targetLockProfile); break;
            }
            currentPlayerControllerMode = mode;
        }
    }

}
