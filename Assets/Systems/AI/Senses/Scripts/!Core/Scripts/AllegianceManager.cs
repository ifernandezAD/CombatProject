using UnityEngine;
using UnityEditor;

public class AllegianceManager : MonoBehaviour
{
    [SerializeField] AllegianceDefinition definition;

    static AllegianceManager instance;

    [Header("Debug (must be in PlayMode)")]
    [SerializeField] [Allegiance] string debugAllegiance1;
    [SerializeField] [Allegiance] string debugAllegiance2;
    [SerializeField] bool debugRelationship;

    private void OnValidate()
    {
        if (debugRelationship)
        {
            debugRelationship = false;
            Debug.Log(GetAllegianceRelationship(debugAllegiance1, debugAllegiance2));
        }
    }

    private void Awake()
    {
        instance = this;
    }

    static public AllegianceDefinition.Relationship GetAllegianceRelationship(string allegiance1, string allegiance2)
    {
        return instance.InternalGetAllegianceRelationship(allegiance1, allegiance2);
    }

    private AllegianceDefinition.Relationship InternalGetAllegianceRelationship(string allegiance1, string allegiance2)
    {
        return definition.CalcRelationship(allegiance1, allegiance2);
    }

#if UNITY_EDITOR
    [MenuItem("Allegiances/Test/Change Allegiance In Runtime")]
    public static void TestChangeAllegianceInRuntime()
    {
        AllegianceManager.instance.definition.ChangeAllegiancesInRuntime(
            instance.definition.allegiances[0],
            instance.definition.allegiances[1],
            (AllegianceDefinition.Relationship)UnityEngine.Random.Range(0, 3));
    }

#endif
}


