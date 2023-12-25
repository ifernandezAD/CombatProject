using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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

    [Header("Vertical Movement")] 
    [SerializeField] InputActionReference jump;

    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.MovementForward;
   
    
    [SerializeField] Transform target;

    [Header("Events")]
    public UnityEvent<PlayerController> onPlayerDeath;

    EntityMovement entityMovement;

    private void Awake()
    {
        entityMovement = GetComponent<EntityMovement>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
    }

    void Update()
    {
        Vector3 direction = CalculateDirectionFromInput();
        bool mustJump = jump.action.WasPerformedThisFrame();
        Vector3 xzPlaneVelocity = entityMovement.Move(direction, mustJump);

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
    }
}
