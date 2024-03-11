using UnityEngine;

public class SpellEventForwarder : MonoBehaviour
{
    [Header("Spell References")]
    [SerializeField] private ConjurationSpell conjurationSpell;

    private PlayerController playerController;

    [Header("Transmutation Spell References")]
    [SerializeField] private GameObject beerPrefab;
    [SerializeField] private GameObject beerDummy;
    [SerializeField] private Transform beerDummyOriginPosition;
    [SerializeField] private GameObject healingAura;


    private void Awake()
    {
        conjurationSpell = GetComponentInParent<ConjurationSpell>();
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

    #region TransmutationSpell

    public void ShowBeer()
    {
        beerPrefab.SetActive(true);
    }

    public void InstantiateBeerDummy()
    {
        Instantiate(beerDummy, beerDummyOriginPosition.position, beerDummyOriginPosition.rotation);
    }

    public void HideBeer()
    {
        beerPrefab.SetActive(false);
    }

    public void ShowHealingAura()
    {
        healingAura.SetActive(true);
    }

    public void HideHealingAura()
    {
        healingAura.SetActive(false);
    }

    #endregion
}
