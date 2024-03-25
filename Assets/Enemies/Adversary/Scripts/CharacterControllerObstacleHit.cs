using UnityEngine;

public class CharacterControllerObstacleHit : MonoBehaviour
{
    [Header("Obstacle Hit Variables")]
    [SerializeField] float minRandomForce = 5f;
    [SerializeField] float maxRandomForce = 10f;
    [SerializeField] float minRandomTorque = 5f;
    [SerializeField] float maxRandomTorque = 10f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            Rigidbody enemyRigidbody = hit.collider.attachedRigidbody;

            if (enemyRigidbody != null) { ApplyRandomForce(enemyRigidbody); }
            else { Debug.LogWarning("No Rigidbody found on the hit object."); }
        }
    }

    private void ApplyRandomForce(Rigidbody rb)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized;

        float randomForce = UnityEngine.Random.Range(minRandomForce, maxRandomForce);
        rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);

        float torqueForce = UnityEngine.Random.Range(minRandomTorque, maxRandomTorque);
        Vector3 torque = UnityEngine.Random.insideUnitSphere.normalized * torqueForce;
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
