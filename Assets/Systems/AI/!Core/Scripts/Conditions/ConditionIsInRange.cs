using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionIsInRange : ConditionBase
{
    internal override bool IsMeet()
    {
        float weaponRange = decissionMaker.ai.entityWeapons.GetCurrentWeapon().GetPrimaryRange();
        float distance = Vector3.Distance(decissionMaker.ai.transform.position,decissionMaker.ai.target.transform.position);

        return distance < weaponRange;          
    }
}
