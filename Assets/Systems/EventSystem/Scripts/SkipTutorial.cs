using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkipTutorial : MonoBehaviour
{
    [SerializeField] InputActionReference skipTutorial;

    private void OnEnable()
    {
        skipTutorial.action.Enable();
    }

    private void Update()
    {
        if (skipTutorial.action.WasPerformedThisFrame())
        {
            LoadingScreen.LoadScene("Level1");
        }
    }

    private void OnDisable()
    {
        skipTutorial.action.Disable();
    }
}
