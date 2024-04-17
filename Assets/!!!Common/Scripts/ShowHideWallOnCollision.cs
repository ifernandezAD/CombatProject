using UnityEngine;

public class ShowHideWallOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Player has touched the wall");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has touched the wall");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Player has ended touching the wall");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has ended touching the wall");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Player has touched the wall");
        if (hit.gameObject.tag == "Player")
        {
            Debug.Log("Player has touched the wall");
        }
    }

    
}
