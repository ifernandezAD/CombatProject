using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum MovementMode
    {
        PlayerDirection,
        CameraDirection,
    }

    [Header("Movement")]
    [SerializeField] float speed = 4f;
    [SerializeField] MovementMode movementMode = MovementMode.PlayerDirection;

    [Header("Input")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;

    CharacterController characterController;



    private void Awake()
    {
        characterController = GetComponentInChildren<CharacterController>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
    }

    void Update()
    {
        Vector2 rawMoveValue = move.action.ReadValue<Vector2>();
        Vector3 xzPlanetMovement = (Vector3.right * rawMoveValue.x) + (Vector3.forward * rawMoveValue.y);

        switch (movementMode)
        {
            case MovementMode.PlayerDirection:
                characterController.Move(xzPlanetMovement * speed * Time.deltaTime);
                break;
            case MovementMode.CameraDirection:
                {
                    Transform mainCameraTransform = Camera.main.transform;
                    Vector3 xzPlanetMovementForCamera = mainCameraTransform.TransformDirection(xzPlanetMovement);
                    float oldMagnitude = xzPlanetMovementForCamera.magnitude;
                    xzPlanetMovementForCamera = Vector3.ProjectOnPlane(xzPlanetMovementForCamera, Vector3.up);
                    xzPlanetMovementForCamera = xzPlanetMovementForCamera.normalized * oldMagnitude;

                    characterController.Move(xzPlanetMovementForCamera * speed * Time.deltaTime);
                }
                break;
        }


    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
    }

}
