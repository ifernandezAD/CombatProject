using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : Death
{
    protected override void ManageDeath()
    {
        PortalManager.instance.gameObject.SetActive(false);
        LoadingScreen.LoadScene("GameOver_Scene");
    }
}
