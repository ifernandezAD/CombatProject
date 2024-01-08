using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senseable : MonoBehaviour
{
    public enum Allegiance
    {
        Player,
        Allies,
        Axis,
        Neutral,
    };

    public bool isVisible;
    public bool isAudible;
    public bool isTouchable;

    public Allegiance allegiance = Allegiance.Neutral;
}
