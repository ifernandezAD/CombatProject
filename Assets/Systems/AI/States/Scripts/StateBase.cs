using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    protected AI ai;
    protected Animator animator;

    public void Init(AI ai){this.ai = ai;}              
}
