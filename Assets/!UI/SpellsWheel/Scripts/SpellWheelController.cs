using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellWheelController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] InputActionReference spellWheel;
    [SerializeField] GameObject postprocessEffects;

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
            ShowSpellWheelEffects();
        }
        else
        {
            animator.SetBool("OpenSpellWheel", false);
            HideSpellWheelEffects();
        }
    }

    void ShowSpellWheelEffects()
    {
        postprocessEffects.SetActive(true);
    }

    void HideSpellWheelEffects()
    {
        postprocessEffects.SetActive(false);
    }

    private void OnDisable()
    {
        spellWheel.action.Disable();
    }
}
