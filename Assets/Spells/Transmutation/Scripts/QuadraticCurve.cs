using UnityEngine;

public class QuadraticCurve : MonoBehaviour
{

    [SerializeField] Transform A;
    [SerializeField] Transform B;
    [SerializeField] Transform Control;

    public Vector3 Evaluate(float t)
    {
        Vector3 ac = Vector3.Lerp(A.position, Control.position, t);
        Vector3 cb = Vector3.Lerp(Control.position, B.position, t);

        return Vector3.Lerp(ac, cb, t);

    }

    private void OnDrawGizmos()
    {
        if (A == null || B == null || Control == null){return;}

        for (int i = 0; i < 20; i++)
        {
            Gizmos.DrawWireSphere(Evaluate(i / 20f), 0.1f);
        }        
    }
}
