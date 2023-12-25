using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellWheelController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] InputActionReference spellWheel;

    private bool spellWheelButtonPressed;

    private void OnEnable()
    {
        spellWheel.action.Enable();
    }

    void Update()
    {
        spellWheelButtonPressed = spellWheel.action.IsPressed();

        if (spellWheelButtonPressed)
        {
            animator.SetBool("OpenSpellWheel", true);
        }
        else
        {
            animator.SetBool("OpenSpellWheel", false);
        }
    }

    private void OnDisable()
    {
        spellWheel.action.Disable();
    }
}
