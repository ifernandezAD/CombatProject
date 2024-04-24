using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Event : MonoBehaviour
{
    [SerializeField] string[] eventTextArray;
    [SerializeField] TextMeshProUGUI eventText;

    private void Awake()
    {
        //Tiraremos por el GetSiblingIndex
    }

    private void OnEnable()
    {
        
    }
}
