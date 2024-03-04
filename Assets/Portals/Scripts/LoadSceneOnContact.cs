using UnityEngine;

public class LoadSceneOnContact : MonoBehaviour
{
    [SerializeField] bool alreadyCrossed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && alreadyCrossed == false)
        {
            alreadyCrossed = true;
            LoadingScreen.LoadScene("Level1");
        }
    }
}
