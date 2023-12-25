using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellWheelController : MonoBehaviour
{
    public Animator animator;
    private bool spellWheelSelected;
    public Image selectedSpell;
    public Sprite noImage;
    public static int spellId;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            spellWheelSelected = !spellWheelSelected;
        }

        if (spellWheelSelected)
        {
            animator.SetBool("OpenSpellWheel", true);
        }else
        {
            animator.SetBool("OpenSpellWheel", false);
        }

        switch (spellId)
        {
            case 0: //nothing is selected
                selectedSpell.sprite = noImage;
                break;
            case 1:              
                Debug.Log("Illusion");
                break;
            case 2: 
                Debug.Log("Hypnosis");
                break;
            case 3: 
                Debug.Log("Nigromancy");
                break;
            case 4: 
                Debug.Log("Magic Shield");
                break;
        }

    }
}
