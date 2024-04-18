using DG.Tweening;
using UnityEngine;

public class ShowHideWallOnCollision : MonoBehaviour
{
    public Material material;
    public string floatPropertyName = "Transparency"; 
    public float targetValue = 1f;
    public float transitionDuration = 1f;
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInside = true;
            // Start transition from current value to target value
            DOTween.To(() => material.GetFloat(floatPropertyName), x => material.SetFloat(floatPropertyName, x), targetValue, transitionDuration);
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }
}
