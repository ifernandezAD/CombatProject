using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [System.Serializable]
    public class ExtraMovementInfo
    {
        public float speed = 10f;
        public float steadyDuration = 0.3f;
        public float decelerationDuration =0.2f;
    };
    
    class ExtraMovement
    {
        public ExtraMovement(Vector3 worldDirection, ExtraMovementInfo info)
        {
            this.direction = worldDirection;
            extraMovementInfo = info;
        }
        public ExtraMovementInfo extraMovementInfo;
        Vector3 direction;
        float elapsedTime;

        public Vector3 UpdateVelocity()
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.InverseLerp(
                extraMovementInfo.steadyDuration,
                extraMovementInfo.steadyDuration + extraMovementInfo.decelerationDuration,
                elapsedTime);

            return direction * extraMovementInfo.speed * (1 - t);
        }

       public bool HasFinished() { return elapsedTime >= extraMovementInfo.steadyDuration + extraMovementInfo.decelerationDuration; }
    }

    [Header("Movement")]
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpSpeed = 5f;

    [Header("Orientation")]
    [SerializeField] float orientationSpeed = 360f;
    [SerializeField] float smoothingSpeed = 10f;

    [Header("Extra Movement Debug")]
    [SerializeField] bool debugExtraMovement = false;
    [SerializeField] bool debugExtraMovementLocal = true;
    [SerializeField] Vector3 debugExtraMovementDirection = Vector3.forward;
    [SerializeField] ExtraMovementInfo debugExtraMovementInfo;

    [Header("Stop Entity Debug")]
    [SerializeField] bool stopEntity;
    [SerializeField] float previousDebugSpeed;
    

    List <ExtraMovement> extraMovements =new();


    CharacterController characterController;
    Animator animator;

    float verticalVelocity = 0f;
    float gravity = -9.8f;
    Vector3 smoothedLocalXZPlaneVelocity;

   private void OnValidate()
   {
       if (debugExtraMovement)
       {
           debugExtraMovement = false;
           if (debugExtraMovementLocal)
           {
               ApplyLocalExtraMovement(debugExtraMovementDirection, debugExtraMovementInfo);
           }else
           {
                ApplyWorldExtraMovement(debugExtraMovementDirection, debugExtraMovementInfo);
           } 
       }
   }

    private void Awake()
    {         
        /*Debug*/  previousDebugSpeed = speed; /*Debug*/

        characterController = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
       

    }

    private void Update()
    {
        /*Debug*/ if (stopEntity) {speed = 0;}                       
        else{speed = previousDebugSpeed; } /*Debug*/

        extraMovements.RemoveAll(x => x.HasFinished());
    }

    //ToDo: in the future look for way to communicate this order Move, orientate, animation
    Vector3 xzPlaneVelocity;
    public Vector3 Move(Vector3 direction, bool mustJump)
    {
        direction.y = 0;
        xzPlaneVelocity = UpdateMovementOnPlane(direction.normalized);
        Vector3 verticalVelocity = UpdateVerticalMovement();
        Vector3 extraMovementsVelocity = UpdateExtraMovements();
        
        Vector3 totalMovement = (xzPlaneVelocity+verticalVelocity+extraMovementsVelocity) * Time.deltaTime;

        characterController.Move(totalMovement);

        UpdateJump(mustJump);

        return xzPlaneVelocity;
    }


    public void Orientate(Vector3 orientationDirection)
    {
        UpdateOrientation(orientationDirection);
    }

    public void Animate()
    {
        UpdateAnimation(xzPlaneVelocity);
    }

    private Vector3 UpdateMovementOnPlane(Vector3 direction)
    {
        Vector3 xzPlaneVelocity = direction * speed;
        //characterController.Move(xzPlaneVelocity * Time.deltaTime);
        return xzPlaneVelocity;
    }

    private void UpdateOrientation(Vector3 orientationDirection)
    {
        float angularDistance = Vector3.SignedAngle(transform.forward, orientationDirection, Vector3.up);
        float angleToApply = orientationSpeed * Time.deltaTime;
        angleToApply = Mathf.Min(angleToApply, Mathf.Abs(angularDistance)) * Mathf.Sign(angularDistance);

        Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }

    private Vector3 UpdateVerticalMovement()
    {
        verticalVelocity += gravity * Time.deltaTime;
        //characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        return Vector3.up * verticalVelocity;
    }

    private void UpdateJump(bool mustJump)
    {
       if (characterController.isGrounded)
        { verticalVelocity = 0f; }

        if (mustJump && characterController.isGrounded)
        { verticalVelocity = jumpSpeed; }  
    }
         

    private Vector3 UpdateExtraMovements()
    {
        Vector3 accumulatedExtraVelocity = Vector3.zero;

        foreach (ExtraMovement em in extraMovements)
        {
            accumulatedExtraVelocity += em.UpdateVelocity();
        }

        return accumulatedExtraVelocity;
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

    internal void ApplyLocalExtraMovement(Vector3 localDirection, ExtraMovementInfo dashMovementInfo)
    {
        ExtraMovement extraMovement = new ExtraMovement(transform.TransformDirection(localDirection), dashMovementInfo);
        extraMovements.Add(extraMovement);
    }

    internal void ApplyWorldExtraMovement(Vector3 worldDirection, ExtraMovementInfo dashMovementInfo)
    {
        ExtraMovement extraMovement = new ExtraMovement(worldDirection, dashMovementInfo);
        extraMovements.Add(extraMovement);
    }
}
