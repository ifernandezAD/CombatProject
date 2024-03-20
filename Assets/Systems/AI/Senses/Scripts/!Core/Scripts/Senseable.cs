using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senseable : MonoBehaviour
{
    public bool isVisible;
    public bool isAudible;
    public bool isTouchable;

    [SerializeField][Allegiance] public string allegiance;

    public void DisableSenseables()
    {
        isVisible = false;
        isAudible = false;
        isTouchable = false;
    }
}
