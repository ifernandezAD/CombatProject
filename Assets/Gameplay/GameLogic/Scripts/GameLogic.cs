using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    public Transform playerTransform { get; private set; }

    [Header("Debug")]
    [SerializeField] bool debugEnterRagnarokMode;

    [Header("References")]
    [SerializeField] InputActionReference escapeGame;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource soundsAudioSource;

    [Header("Ragnarok Mode")]
    [SerializeField] GameObject adversaryPrefab;
    [SerializeField] Transform adversarySpawnParent;
    [SerializeField] GameObject ragnarokVfxContainer;
    [SerializeField] AudioClip ragnarokMusic;
    [SerializeField] AudioClip ragnarokBell;

    private State_Panic[] panickingObjects;

    [Header("Init")]
    [SerializeField] Transform panicPatrolPointsParent;

    private void OnValidate()
    {
        if (debugEnterRagnarokMode)
        {
            EnterRagnarokMode();
            debugEnterRagnarokMode = false;
        }
    }

    private void Awake()
    {
        instance = this;

        playerTransform = GameObject.FindWithTag("Player").transform;
        panickingObjects = FindObjectsOfType<State_Panic>();

        foreach (State_Panic panickingObject in panickingObjects)
        {
            panickingObject.InitParent(panicPatrolPointsParent);
        }
    }

    private void OnEnable()
    {
        escapeGame.action.Enable();

        RedSkyController.instance.onMaxRedSkyReached.AddListener(EnterRagnarokMode);
    }

    private void Update()
    {
        UpdateEscapeGame();
    }

    private void UpdateEscapeGame()
    {
        if (escapeGame.action.WasPerformedThisFrame())
        {
            Application.Quit();
        }
    }

    void EnterRagnarokMode()
    {
        InstantiateAdversary();
        SetCiviliansToPanic();
        SetRagnarokAudio();

        ragnarokVfxContainer.SetActive(true);
    }


    #region Ragnarok Mode

    void InstantiateAdversary()
    {
        Transform[] children = adversarySpawnParent.transform.Cast<Transform>()
                                  .Where(child => child.parent == adversarySpawnParent.transform)
                                  .ToArray();

        int randomPortalIndex = Random.Range(0, children.Length);

        Instantiate(adversaryPrefab, children[randomPortalIndex].transform.position, Quaternion.identity);
    }

    private void SetCiviliansToPanic()
    {
        foreach (State_Panic panickingObject in panickingObjects)
        {
            AI ai = panickingObject.transform.gameObject.GetComponent<AI>();
            ai.SetRoaming(false);
        }
    }

    private void SetRagnarokAudio()
    {        
        SetBellSound();
        DOVirtual.DelayedCall(0.5f, () => musicAudioSource.Stop());
        DOVirtual.DelayedCall(2, SetNewLevelMusic);
    }

    private void SetBellSound()
    {
        soundsAudioSource.clip = ragnarokBell;
        soundsAudioSource.Play();
    }

    private void SetNewLevelMusic()
    {
        musicAudioSource.clip = ragnarokMusic;
        musicAudioSource.Play();
    }

    #endregion

    private void OnDisable()
    {
        escapeGame.action.Disable();

        RedSkyController.instance.onMaxRedSkyReached.RemoveListener(EnterRagnarokMode);
    }
}
