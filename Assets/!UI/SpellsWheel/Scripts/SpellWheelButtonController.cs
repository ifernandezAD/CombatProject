using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellWheelButtonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputActionReference spellWheel;
    [SerializeField] GameObject shineParticle;
    [SerializeField] Spell spellToActivate;
    [SerializeField] TextMeshProUGUI spellNameUI;
    [SerializeField] TextMeshProUGUI spellDescriptionUI;

    Animator animator;

    public static event Action spellNotified;   
    private bool selected;

    [Header("Variables")]
    [SerializeField] string spellName = "Name";
    [SerializeField] string spellDescription = "This spell does x thing";


    [Header("Glow Color")]
    [SerializeField] Color glowColor = Color.white;
    [SerializeField, Range(0f, 50f)] float glowIntensity = 20f;

    [Header("Mouse")]
    private float mouseX;
    private float mouseY;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() { spellWheel.action.Enable(); }

    void Update()
    {
        UpdateMouseDelta();

        if (spellWheel.action.WasReleasedThisFrame() && selected)
        {
            spellNotified?.Invoke();
            spellToActivate.enabled = true;
        }
    }

    private void UpdateMouseDelta()
    {
        // Get the mouse delta (change in mouse position since last frame)
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // Convert the mouse delta to world space
        Vector3 mouseDeltaWorld = Camera.main.transform.TransformDirection(new Vector3(mouseDelta.x, mouseDelta.y, 0));

        // Get the position of the transform
        Vector3 transformPosition = transform.position;

        // Calculate the relative position vector between the transform and the mouse position
        Vector3 relativePosition = transformPosition + mouseDeltaWorld;

        // Now you have the relative position vector between the transform and the mouse position
        Debug.Log("Relative Position: " + relativePosition);
    }

    public void HoverEnter()
    {
        animator.SetBool("Hover", true);
        selected = true;

        ChangeMaterialGlowColor();
        ChangeSpellTexts();
        ShowHoverVfx();
    }

    public void HoverExit()
    {
        animator.SetBool("Hover", false);
        selected = false;

        EmptySpellTexts();
        HideHoverVfx();
    }

    private void ShowHoverVfx() { shineParticle.SetActive(true); }
    private void HideHoverVfx() { shineParticle.SetActive(false); }

    private void ChangeSpellTexts()
    {
        spellNameUI.text = spellName;
        spellDescriptionUI.text = spellDescription;
    }

    private void EmptySpellTexts()
    {
        spellNameUI.text = "";
        spellDescriptionUI.text = "";
    }

    private void ChangeMaterialGlowColor()
    {
        Material nameMaterial = spellNameUI.materialForRendering;
        Material descriptionMaterial = spellDescriptionUI.materialForRendering;

        if (nameMaterial.HasProperty(ShaderUtilities.ID_GlowColor))
        {
            Color nameColor = glowColor * glowIntensity;
            nameMaterial.SetColor(ShaderUtilities.ID_GlowColor, nameColor);
        }

        if (descriptionMaterial.HasProperty(ShaderUtilities.ID_GlowColor))
        {
            Color descriptionColor = glowColor * glowIntensity;
            descriptionMaterial.SetColor(ShaderUtilities.ID_GlowColor, descriptionColor);
        }
    }
    private void OnDisable() { spellWheel.action.Disable(); }
}



