using UnityEngine;

public class ShowHideWallOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject visuals;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            visuals.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            visuals.SetActive(false);
        }
    }
}
