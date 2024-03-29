using UnityEngine;

public class TransmutationSpell : Spell
{
    [Header("Animations")]
    private readonly int transmutationHash = Animator.StringToHash("TransmutationSpell");

    [Header("Transmutation Spell References")]
    [SerializeField] private GameObject beerPrefab;
    [SerializeField] private GameObject beerDummy;
    [SerializeField] private Transform beerDummyOriginPosition;
    [SerializeField] private GameObject healingAura;


    protected override void BeginSpell() { ShowBeer(); }

    protected override void SetSpellAnimation(){animator.SetTrigger(transmutationHash);}
   
        
    public void ShowBeer(){beerPrefab.SetActive(true);}
    public void HideBeer(){beerPrefab.SetActive(false);}
      
    public void InstantiateBeerDummy(){Instantiate(beerDummy, beerDummyOriginPosition.position, beerDummyOriginPosition.rotation);}
    
    public void ShowHealingAura(){healingAura.SetActive(true);}         
    public void HideHealingAura(){healingAura.SetActive(false);}
    
    public void Healing(){entityLife.RecoverAllLife();}
    
       
    protected override void EndSpell()
    {
        entityWeapons.RecoverWeapon();
        EnablePlayerController();
        EnablePlayerWeaponController();
    }
}
