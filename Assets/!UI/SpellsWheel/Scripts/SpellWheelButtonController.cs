using UnityEngine;
using UnityEngine.InputSystem;

public class SpellWheelButtonController : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    [SerializeField] InputActionReference spellWheel;

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
        }
    }

    public void HoverEnter()
    {
        animator.SetBool("Hover", true);
        selected = true;
    }

    public void HoverExit()
    {
        animator.SetBool("Hover", false);
        selected = false;
    }

    private void OnDisable()
    {
        spellWheel.action.Disable();
    }
}
