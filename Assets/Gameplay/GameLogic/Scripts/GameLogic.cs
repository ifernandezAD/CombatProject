using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }  

    public Transform playerTransform { get; private set; }

    [Header("Debug")]
    [SerializeField] bool debugEnterRagnarokMode;

    [Header("Ragnarok Mode")]
    [SerializeField] GameObject adversaryPrefab;
    [SerializeField] Transform adversarySpawnParent;
    [SerializeField] GameObject ragnarokVfxContainer;
 
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
    }

    private void OnEnable()
    {
        RedSkyController.instance.onMaxRedSkyReached.AddListener(EnterRagnarokMode);
    }

    void EnterRagnarokMode()
    {
        EnablePlayerAudibleDetection();
        InstantiateAdversary();

        ragnarokVfxContainer.SetActive(true);
    }

    #region Ragnarok Mode

    private void EnablePlayerAudibleDetection()
    {
        AudibleByConstantEmission audibleByAdversary = playerTransform.GetComponent<AudibleByConstantEmission>();
        audibleByAdversary.enabled = true;
    }

    void InstantiateAdversary()
    {
        Transform[] children = adversarySpawnParent.transform.Cast<Transform>()
                                  .Where(child => child.parent == adversarySpawnParent.transform)
                                  .ToArray();

        int randomPortalIndex = Random.Range(0, children.Length);

        Instantiate(adversaryPrefab, children[randomPortalIndex].transform.position, Quaternion.identity);
    }
    #endregion

    private void OnDisable()
    {
        RedSkyController.instance.onMaxRedSkyReached.RemoveListener(EnterRagnarokMode);
    }
}
