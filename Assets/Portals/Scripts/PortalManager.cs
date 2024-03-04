using System.Linq;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class PortalManager : MonoBehaviour
{
    public static PortalManager instance { get; private set; }

    [Header("Debug")]
    [SerializeField] bool debugRandomPortalInstantiate;

    [Header("References")]
    [SerializeField] GameObject portalPrefab;
    [SerializeField] Transform portalSpawnParent;
    public Transform portalContainer;

    [Header("Countdown")]
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField] float remainingTime;

    private void OnValidate()
    {
        if (debugRandomPortalInstantiate)
        {
            InstantiateRandomPortal();
            debugRandomPortalInstantiate = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InstantiateRandomPortal();
    }

    private void Update()
    {
        UpdateCountdown();
    }

    private void UpdateCountdown()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //GameOver();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void InstantiateRandomPortal()
    {
        Transform[] children = portalSpawnParent.transform.Cast<Transform>()
                                  .Where(child => child.parent == portalSpawnParent.transform)
                                  .ToArray();

        int randomPortalIndex = Random.Range(0, children.Length);

        GameObject portalClone = Instantiate(portalPrefab, children[randomPortalIndex].transform.position, Quaternion.identity);
        portalClone.transform.parent = portalContainer.transform;
    }
}
