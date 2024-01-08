using UnityEngine;

public abstract class Sense : MonoBehaviour
{
    Senseable mySenseable;

    internal void SetMySenseable(Senseable senseable)
    {
        mySenseable = senseable;
    }

    protected Senseable GetMySenseable()
    {
        return mySenseable;
    }

    public abstract Senseable GetSenseable();
}
