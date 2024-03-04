using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu()]
public class AllegianceDefinition : ScriptableObject
{
    public enum Relationship
    {
        Friends,
        Enemies,
        Neutrals,
    }

    public string[] allegiances;
    public Relationship[] relationships;

    [DoNotSerialize] Dictionary<string, int> nameToIndexHash;
    [DoNotSerialize] Dictionary<(int allegianceIndex1, int allegianceIndex2), int> allegianceIndexesToInt;


    private void InitHashes()
    {
        if (nameToIndexHash == null)
        {
            nameToIndexHash = new();
            for (int i = 0; i < allegiances.Length; i++)
            { nameToIndexHash.Add(allegiances[i], i); }

            allegianceIndexesToInt = new();
            int k = 0;
            for (int j = 0; j < allegiances.Length; j++)
            {
                for (int i = allegiances.Length - 1; i >= j; i--)
                {
                    //Debug.Log($"({i}, {j}), {k}, {relationships[k]}");
                    if (i == j)
                    { allegianceIndexesToInt.Add((i, j), k); }
                    else
                    {
                        allegianceIndexesToInt.Add((i, j), k);
                        allegianceIndexesToInt.Add((j, i), k);
                    }
                    k++;
                }

            }
        }
    }

    internal Relationship CalcRelationship(string allegiance1, string allegiance2)
    {
        InitHashes();
        int index1 = nameToIndexHash[allegiance1];
        int index2 = nameToIndexHash[allegiance2];
        int k = allegianceIndexesToInt[(index1, index2)];

        //Debug.Log($"CalcRelationship - ({index1}, {index2}), {k}, {relationships[k]}");

        return relationships[k];
    }
}
