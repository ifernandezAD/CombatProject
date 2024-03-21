using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdversarySpawner : MonoBehaviour
{
    public static AdversarySpawner instance { get; private set; }

    [Header("Debug")]
    [SerializeField] bool debugRandomAdversaryInstantiate;

    [Header("References")]
    [SerializeField] GameObject adversaryPrefab;
    [SerializeField] Transform adversarySpawnParent;


    private void OnValidate()
    {
        if (debugRandomAdversaryInstantiate)
        {
            InstantiateAdversary();
            debugRandomAdversaryInstantiate = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        RedSkyController.instance.onMaxRedSkyReached.AddListener(InstantiateAdversary);
    }


    void InstantiateAdversary()
    {
        Transform[] children = adversarySpawnParent.transform.Cast<Transform>()
                                  .Where(child => child.parent == adversarySpawnParent.transform)
                                  .ToArray();

        int randomPortalIndex = Random.Range(0, children.Length);

        Instantiate(adversaryPrefab, children[randomPortalIndex].transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        RedSkyController.instance.onMaxRedSkyReached.RemoveListener(InstantiateAdversary);
    }
}
