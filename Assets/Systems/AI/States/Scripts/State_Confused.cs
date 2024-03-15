using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Confused : StateBase
{

    private void Update()
    {
        //Cambia de Skin
        //Cambia su allegiance a player
        

        ai.StopEntity(true);
        ai.PlayConfusedAnimation();    
    }

}
