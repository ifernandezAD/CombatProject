using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFirstLevel : MonoBehaviour
{
    public void ReloadGame()
    {
        LoadingScreen.LoadScene("Level1");
    }
}
