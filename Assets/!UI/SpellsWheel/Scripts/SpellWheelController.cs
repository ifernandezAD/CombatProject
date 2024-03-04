using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellWheelController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugActivateButtons;

    [Header("References")]
    [SerializeField] InputActionReference spellWheel;
    [SerializeField] GameObject postprocessEffects;
    [SerializeField] GameObject lockedRotationCamera;
    Animator animator;
    Image[] spellIcons;

    [Header("Variables")]
    [SerializeField] float disabledButtonsAlpha = 0.5f;
    [SerializeField] float enabledButtonsAlpha = 1f;
    [SerializeField] float delayEnterSlowMotion = 0.5f;
    private bool spellWheelButtonPressed;
    private bool canCastSpells = true;

    private void OnValidate()
    {
        if (debugActivateButtons)
        {
            ActivateButtons();
            debugActivateButtons = false;
        }
    }

    private void OnEnable()
    {
        spellWheel.action.Enable();
        SpellWheelButtonController.spellNotified += DeactivateButtons;
        Spell.endSpellNotified += ActivateButtons;
    }

    private void Awake()
    {
        spellIcons = GetComponentsInChildren<Image>();
        animator = GetComponent<Animator>();
    }

    void Update() { UpdateOnButtonWheelPressed(); }

    private void UpdateOnButtonWheelPressed()
    {
        spellWheelButtonPressed = spellWheel.action.IsPressed();

        if (spellWheelButtonPressed)
        {
            animator.SetBool("OpenSpellWheel", true);
            lockedRotationCamera.SetActive(true);

            if (canCastSpells)
            {
                ShowSpellWheelEffects();
                Invoke(nameof(DoSlowMotion), delayEnterSlowMotion);
            }
        }
        else
        {
            animator.SetBool("OpenSpellWheel", false);
            lockedRotationCamera.SetActive(false);
            HideSpellWheelEffects();
            TimeManager.instance.ResetTime();
        }
    }

    void ShowSpellWheelEffects() { postprocessEffects.SetActive(true); }
    void HideSpellWheelEffects(){postprocessEffects.SetActive(false);}
           
    public void DeactivateButtons()
    {
        canCastSpells = false;
        HandleButtonsAppearance(disabledButtonsAlpha, false);
    }

    private void ActivateButtons()
    {
        canCastSpells = true;
        HandleButtonsAppearance(enabledButtonsAlpha, true);
    }

    private void HandleButtonsAppearance(float newAlpha, bool raycastTargetStatus)
    {
        foreach (Image image in spellIcons)
        {
            Color currentColor = image.color;
            currentColor.a = newAlpha;
            image.color = currentColor;

            image.raycastTarget = raycastTargetStatus;
        }
    }

    private void DoSlowMotion(){TimeManager.instance.DoSlowMotion();}
               
    private void OnDisable()
    {
        spellWheel.action.Disable();
        SpellWheelButtonController.spellNotified -= DeactivateButtons;
        Spell.endSpellNotified -= ActivateButtons;
    }
}
