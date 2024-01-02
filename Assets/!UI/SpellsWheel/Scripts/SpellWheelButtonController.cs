using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellWheelButtonController : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    [SerializeField] InputActionReference spellWheel;
    [SerializeField] Image spellIcon;
    [SerializeField] GameObject shineParticle;

    [Header("Events")]
    public static Action onIllusionSpellNotified;
    public static Action onHypnosisSpellNotified;
    public static Action onNigromancySpellNotified;
    public static Action onShieldSpellNotified;

    public enum SpellType
    {
        Illusion,
        Hypnosis,
        Nigromancy,
        Shield,
    }

    [SerializeField] SpellType spellType = SpellType.Hypnosis;

    private bool selected;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        spellWheel.action.Enable();
    }

    void Update()
    {
        if (spellWheel.action.WasReleasedThisFrame() && selected)
        {
            Debug.Log("Spell is done with success!!");

            switch (spellType)
            {
                case SpellType.Illusion:
                    onIllusionSpellNotified?.Invoke();
                    break;
                case SpellType.Hypnosis:
                    onHypnosisSpellNotified?.Invoke();
                    break;
                case SpellType.Nigromancy:
                    onNigromancySpellNotified?.Invoke();
                    break;
                case SpellType.Shield:
                    onShieldSpellNotified?.Invoke();
                    break;
            }
        }
    }

    public void HoverEnter()
    {
        animator.SetBool("Hover", true);
        selected = true;
        ShowHoverVfx();
    }

    public void HoverExit()
    {
        animator.SetBool("Hover", false);
        selected = false;
        HideHoverVfx();
    }

    private void ShowHoverVfx()
    {
        spellIcon.color = Color.magenta;
        shineParticle.SetActive(true);
    }

    private void HideHoverVfx()
    {
        spellIcon.color = Color.black;
        shineParticle.SetActive(false);
    }

    private void OnDisable()
    {
        spellWheel.action.Disable();
    }
}
