using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEditor;

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

    [Header("Events")]
    public UnityEvent<PlayerController> onPlayerDeath;

    EntityMovement entityMovement;

    protected override void ChildAwake()
    {
        entityMovement = GetComponent<EntityMovement>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        dash.action.Enable();
        run.action.Enable();
    }

    protected override void ChildStart()
    {
        
    }

    void Update()
    {
        Vector3 direction = CalculateDirectionFromInput();
        bool mustJump = jump.action.WasPerformedThisFrame();
        Vector3 xzPlaneVelocity = entityMovement.Move(direction, mustJump);

        if (dash.action.WasPerformedThisFrame()) { entityMovement.ApplyLocalExtraMovement(Vector3.forward ,dashMovementInfo) ; }

        Vector3 orientationDirection = CalculateOrientationFromSettings(xzPlaneVelocity);
        entityMovement.Orientate(orientationDirection);

        entityMovement.Animate();
    }

    private Vector3 CalculateOrientationFromSettings(Vector3 xzPlaneVelocity)
    {
        Vector3 orientationDirection = Vector3.zero;

        switch (orientationMode)
        {
            case OrientationMode.CameraForward:
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
    }
}
