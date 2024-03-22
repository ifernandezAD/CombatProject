using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    [SerializeField] bool isRagnarokMode;

    public Transform playerTransfrom { get; private set; }

    [Header("Debug")]
    [SerializeField] bool debugEnterRagnarokMode;

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

        playerTransfrom = GameObject.FindWithTag("Player").transform;
    }

    void EnterRagnarokMode()
    {
        AudibleByConstantEmission audibleByAdversary = playerTransfrom.GetComponent<AudibleByConstantEmission>();
        audibleByAdversary.enabled = true;
    }
}
