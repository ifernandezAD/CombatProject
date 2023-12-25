using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellWheelButtonController : MonoBehaviour
{
    public int SpellId;
    private Animator animator;
    public string spellName;
    public TextMeshProUGUI spellText;
    public Image selectedSpell;
    private bool selected = false;
    public Sprite icon;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (selected)
        {
            selectedSpell.sprite = icon;
            spellText.text = spellName;
        }
    }

    public void Selected()
    {
        selected = true;
        SpellWheelController.spellId = SpellId;
    }

    public void Deselected()
    {
        selected = false;
        SpellWheelController.spellId = 0;
    }

    public void HoverEnter()
    {
        animator.SetBool("Hover", true);
        spellText.text = spellName;
    }

    public void HoverExit()
    {
        animator.SetBool("Hover", false);
        spellText.text = "";
    }
}
