using UnityEngine;

public abstract class Barrel : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShot;
    [SerializeField] bool debugStartContinuousShooting;
    [SerializeField] bool debugStopContinuousShooting;
    [SerializeField] float horizontalDispersionAngle = 5f;
    [SerializeField] float verticalDispersionAngle = 5f;

    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            Shot();
        }

        if (debugStartContinuousShooting)
        {
            debugStartContinuousShooting = false;
            StartContinuousShooting();
        }

        if (debugStopContinuousShooting)
        {
            debugStopContinuousShooting = false;
            StopContinuousShooting();
        }
    }

    public void Shot()
    {
        float horizontalDispersion = Random.Range(-horizontalDispersionAngle, horizontalDispersionAngle);
        float verticalDispersion = Random.Range(-verticalDispersionAngle, verticalDispersionAngle);

        Vector3 direction =
            Quaternion.AngleAxis(horizontalDispersion, transform.up) *
            Quaternion.AngleAxis(verticalDispersion, transform.right) *
            transform.forward;

        Shot(direction);
    }

    public abstract void Shot(Vector3 direction);
    public abstract void StartContinuousShooting();
    public abstract void StopContinuousShooting();

}
