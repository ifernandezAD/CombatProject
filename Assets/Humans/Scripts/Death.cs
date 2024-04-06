using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Death : MonoBehaviour
{
    protected EntityLife entityLife;

    private void Awake(){InternalAwake();}       
    protected virtual void InternalAwake(){entityLife = GetComponent<EntityLife>();}
           
    private void OnEnable(){ entityLife.onLifeDepleted.AddListener(ManageDeath);}
          
    protected abstract void ManageDeath();

    private void OnDisable() {entityLife.onLifeDepleted.RemoveListener(ManageDeath);}            
}
