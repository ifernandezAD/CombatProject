using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpSpeed = 5f;

    [Header("Orientation")]
    [SerializeField] float orientationSpeed = 360f;
    [SerializeField] float smoothingSpeed = 10f;

    CharacterController characterController;
    Animator animator;

    float verticalVelocity = 0f;
    float gravity = -9.8f;
    Vector3 smoothedLocalXZPlaneVelocity;

    private void Awake()
    {
        characterController = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    //ToDo: in the future look for way to communicate this order Move, orientate, animation
    Vector3 xzPlaneVelocity;
    public Vector3 Move(Vector3 direction, bool mustJump)
    {
        xzPlaneVelocity = UpdateMovementOnPlane(direction.normalized);
        UpdateVerticalMovement(mustJump);

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
        characterController.Move(xzPlaneVelocity * Time.deltaTime);
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

    private void UpdateVerticalMovement(bool mustJump)
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        if (characterController.isGrounded)
        { verticalVelocity = 0f; }

        if (mustJump && characterController.isGrounded)
        { verticalVelocity = jumpSpeed; }
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
}
