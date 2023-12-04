using UnityEngine;
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
    [SerializeField] float speed = 4f;
    [SerializeField] MovementMode movementMode = MovementMode.PlayerDirection;
    [SerializeField] InputActionReference move;

    [Header("Vertical Movement")]
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] InputActionReference jump;

    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.MovementForward;
    [SerializeField] float orientationSpeed = 360f;
    [SerializeField] float smoothingSpeed = 10f;

    [Header("Attack")]
    [SerializeField] InputActionReference primaryAttack;
    [SerializeField] InputActionReference secondaryAttack;

    [Header("WeaponSelection")]
    [SerializeField] InputActionReference changeWeapons;
    [SerializeField] InputActionReference[] selectWeapons;

    CharacterController characterController;
    float verticalVelocity = 0f;
    float gravity = -9.8f;
    Vector3 smoothedLocalXZPlaneVelocity;

    EntityWeapons entityWeapons;

    Animator animator;


    private void Awake()
    {
        characterController = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        entityWeapons = GetComponent<EntityWeapons>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        primaryAttack.action.Enable();
        secondaryAttack.action.Enable();
        changeWeapons.action.Enable();
        foreach (InputActionReference iar in selectWeapons)
        {
            iar.action.Enable();
        }
    }

    void Update()
    {
        Vector3 xzPlaneVelocity = UpdateMovementOnPlane();

        UpdateVerticalMovement();

        UpdateOrientation(xzPlaneVelocity);

        UpdateAnimation(xzPlaneVelocity);

        UpdateAttack();

        UpdateWeaponSelection();
    }

    void UpdateWeaponSelection()
    {
        Vector2 changeWeaponValue = changeWeapons.action.ReadValue<Vector2>();

        if (changeWeaponValue.y > 0f)
        {
            entityWeapons.SelectNextWeapon();
        }else if(changeWeaponValue.y <0f)
        {
            entityWeapons.SelectPreviousWeapon();
        }
        for (int i = 0; i < selectWeapons.Length; i++)
        {
            if (selectWeapons[i].action.WasPerformedThisFrame())
            {
                entityWeapons.SelectWeapon(i == 0 ? -1 : i-1);   
            }
        }
    }




     void UpdateAttack()
     {
         if (primaryAttack.action.WasPerformedThisFrame())
         {
            PerformAttack(entityWeapons.GetCurrentWeapon().GetPrimaryAttackType());       
         }
         if (secondaryAttack.action.WasPerformedThisFrame())
         {
            PerformAttack(entityWeapons.GetCurrentWeapon().GetPrimaryAttackType());       
         }
     }
     
     private void PerformAttack(Weapon.AttackType attackType)
     {
         switch (attackType)
         {
             case Weapon.AttackType.None:
                 break;
             case Weapon.AttackType.Melee:
                 entityWeapons.MeleeAttack();
                 break;
             case Weapon.AttackType.Shot:
                 entityWeapons.Shot();
                 break;
             case Weapon.AttackType.Burst:
                //entityweapons.Burst();
                 break;
             case Weapon.AttackType.ContinousShot:
                //entityweapons.ContinousShot();
                break;
         }
     }

    private void UpdateAnimation(Vector3 xzPlaneVelocity)
    {
        Vector3 localXZPlaneVelocity = transform.InverseTransformDirection(xzPlaneVelocity);
        Vector3 distance = localXZPlaneVelocity - smoothedLocalXZPlaneVelocity;
        float linearDistance = distance.magnitude;
        smoothedLocalXZPlaneVelocity += distance.normalized * Mathf.Min(smoothingSpeed * Time.deltaTime, linearDistance);

        animator.SetFloat("ForwardVelocity", smoothedLocalXZPlaneVelocity.z);
        animator.SetFloat("SidewardVelocity", smoothedLocalXZPlaneVelocity.x);
        animator.SetBool("IsGrounded", characterController.isGrounded);
        animator.SetFloat("VerticalVelocityNormalized", Mathf.Clamp01(Mathf.InverseLerp(jumpSpeed, -jumpSpeed, verticalVelocity)));
    }

    private void UpdateOrientation(Vector3 xzPlaneVelocity)
    {
        Vector3 desiredDirection = Vector3.zero;

        switch (orientationMode)
        {
            case OrientationMode.CameraForward:
                break;
            case OrientationMode.MovementForward:
                desiredDirection = xzPlaneVelocity;
                break;
            case OrientationMode.LookToTarget:
                break;
        }

        float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);
        float angleToApply = orientationSpeed * Time.deltaTime;
        angleToApply = Mathf.Min(angleToApply, Mathf.Abs(angularDistance)) * Mathf.Sign(angularDistance);

        Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }

    private void UpdateVerticalMovement()
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        if (characterController.isGrounded)
        { verticalVelocity = 0f; }

        if (jump.action.WasPerformedThisFrame() && characterController.isGrounded)
        { verticalVelocity = jumpSpeed; }
    }

    private Vector3 UpdateMovementOnPlane()
    {
        Vector2 rawMoveValue = move.action.ReadValue<Vector2>();
        Vector3 xzPlanetMovement = (Vector3.right * rawMoveValue.x) + (Vector3.forward * rawMoveValue.y);
        Vector3 xzPlaneVelocity = Vector3.zero;

        switch (movementMode)
        {
            case MovementMode.PlayerDirection:
                xzPlaneVelocity = xzPlanetMovement * speed;
                characterController.Move(xzPlanetMovement * speed * Time.deltaTime);
                break;
            case MovementMode.CameraDirection:
                {
                    Transform mainCameraTransform = Camera.main.transform;
                    Vector3 xzPlanetMovementForCamera = mainCameraTransform.TransformDirection(xzPlanetMovement);
                    float oldMagnitude = xzPlanetMovementForCamera.magnitude;

                    xzPlanetMovementForCamera = Vector3.ProjectOnPlane(xzPlanetMovementForCamera, Vector3.up);
                    xzPlanetMovementForCamera = xzPlanetMovementForCamera.normalized * oldMagnitude;

                    xzPlaneVelocity = xzPlanetMovementForCamera * speed;
                }
                break;
        }
        characterController.Move(xzPlaneVelocity * Time.deltaTime);
        return xzPlaneVelocity;
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        primaryAttack.action.Disable();
        secondaryAttack.action.Disable();
        changeWeapons.action.Disable();
        foreach (InputActionReference iar in selectWeapons)
        {
            iar.action.Disable();
        }
    }
}
