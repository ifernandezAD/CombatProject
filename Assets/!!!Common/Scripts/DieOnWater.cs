using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoadingScreen.LoadScene("GameOver_Scene");
        }
    }   
}
