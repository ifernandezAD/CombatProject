using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpellEventForwarder : MonoBehaviour
{
    private PlayerController playerController;
    private EntityWeapons entityWeapons;

    [Header("SimpleAudioEvents")]
    [SerializeField] private SimpleAudioEvent voiceAudioEvent;
    [SerializeField] private SimpleAudioEvent smokeAudioEvent;
    private AudioSource audioSource;

    [Header("Conjuration Spell References")]
    [SerializeField] private GameObject cigarPrefab;
    [SerializeField] private GameObject cigarParticle;

    [Header("Transmutation Spell References")]
    [SerializeField] private GameObject beerPrefab;
    [SerializeField] private GameObject beerDummy;
    [SerializeField] private Transform beerDummyOriginPosition;
    [SerializeField] private GameObject healingAura;


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        entityWeapons = GetComponentInParent<EntityWeapons>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void EnablePlayerController()
    {
        playerController.enabled = true;
    }

    public void DisablePlayerController()
    {
        playerController.enabled = false;
    }

    public void RequestRemoveWeapon()
    {
        entityWeapons.RemoveWeapon();
    }

    public void RequestRecoverWeapon()
    {
        entityWeapons.RecoverWeapon();
    }

    public void PlaySpellShoutSound()
    {
        voiceAudioEvent.Play(audioSource);
    }

    #region ConjurationSpell

    public void PlaySpellCigarSound()
    {
        smokeAudioEvent.Play(audioSource);
    }

    public void ShowCigar()
    {
        cigarPrefab.SetActive(true);
    }

    public void ShowCigarParticle()
    {
        cigarParticle.SetActive(true);
    }

    public void HideCigar()
    {
        cigarPrefab.SetActive(false);
    }

    public void HideCigarParticle()
    {
        cigarParticle.SetActive(false);
    }

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
