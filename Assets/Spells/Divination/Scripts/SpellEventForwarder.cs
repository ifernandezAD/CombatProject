using UnityEngine;

public class SpellEventForwarder : MonoBehaviour
{
    [Header("Spell References")]
    [SerializeField] private ConjurationSpell conjurationSpell;
    [SerializeField] private DivinationSpell divinationSpell;
    [SerializeField] private TransmutationSpell transmutationSpell;
    [SerializeField] private AbjurationSpell abjurationSpell;

    private PlayerController playerController;


    private void Awake()
    {
        conjurationSpell = GetComponentInParent<ConjurationSpell>();
        divinationSpell = GetComponentInParent<DivinationSpell>();
        transmutationSpell = GetComponentInParent<TransmutationSpell>();
        abjurationSpell = GetComponentInParent<AbjurationSpell>();

        playerController = GetComponentInParent<PlayerController>();
    }

    public void EnablePlayerController() { playerController.enabled = true; }
    public void DisablePlayerController() { playerController.enabled = false; }

    #region ConjurationSpell

    public void RequestShowCigar() { conjurationSpell.ShowCigar(); }
    public void RequestHideCigar() { conjurationSpell.HideCigar(); }
    public void RequestShowCigarParticle() { conjurationSpell.ShowCigarParticle(); }
    public void RequestHideCigarParticle() { conjurationSpell.HideCigarParticle(); }
    public void RequestPlayConjurationSound() { conjurationSpell.PlayConjurationSound(); }

    #endregion

    #region DivinationSpell

    public void RequestPlayDivinationSound() {divinationSpell.PlayDivinationSound(); }

    #endregion

    #region TransmutationSpell

    public void RequestShowBeer() { transmutationSpell.ShowBeer();}
    public void RequestHideBeer() { transmutationSpell.HideBeer();}

    public void RequestInstantiateBeerDummy() { transmutationSpell.InstantiateBeerDummy(); }

    public void RequestShowHealingAura() { transmutationSpell.ShowHealingAura(); }
    public void RequestHideHealingAura() { transmutationSpell.HideHealingAura(); }

    public void RequestHealing() { transmutationSpell.Healing(); }

    #endregion

    #region AbjurationSpell

    public void RequestActivateMagicSpell() { abjurationSpell.ActivateMagicShield(); }

    #endregion
}
