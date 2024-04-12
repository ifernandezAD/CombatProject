using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    public Transform playerTransform { get; private set; }

    [SerializeField] InputActionReference escapeGame;

    [Header("Debug")]
    [SerializeField] bool debugEnterRagnarokMode;

    [Header("Ragnarok Mode")]
    [SerializeField] GameObject adversaryPrefab;
    [SerializeField] Transform adversarySpawnParent;
    [SerializeField] GameObject ragnarokVfxContainer;
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

    #endregion

    private void OnDisable()
    {
        escapeGame.action.Disable();

        RedSkyController.instance.onMaxRedSkyReached.RemoveListener(EnterRagnarokMode);
    }
}
