using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameLogic : MonoBehaviour
{

   public static GameLogic instance { get; private set; }


    private void Awake()
    {
        instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
