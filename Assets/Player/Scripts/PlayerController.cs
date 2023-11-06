using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //[Header("Movement")]
    //public enum MovementMode 
    //{
    //    PlayerDirection,
    //    CameraDirection,
    //}
    //[SerializeField] float speed = 4f;
    //
   // [Header("Input"]
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference move;

    CharacterController characterController;
    

    private void Awake()
    {
        characterController =GetComponentInChildren<CharacterController>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rawMoveValue = move.action.ReadValue<Vector2>();
        Vector3 xzPlanetMovement = (Vector3.right * rawMoveValue.x) + (Vector3.forward * rawMoveValue.y);

        //characterController.Move(rawMoveValue * speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
    }

}
