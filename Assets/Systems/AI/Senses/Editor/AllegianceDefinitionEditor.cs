using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine.Editor;
using System;

[CustomEditor(typeof(AllegianceDefinition))]
public class AllegianceDefinitionEditor : Editor
{
    SerializedProperty allegiances;
    AllegianceDefinition t;

    private void OnEnable()
    {
        allegiances = serializedObject.FindProperty("allegiances");
        t = (AllegianceDefinition)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Hola", MessageType.None);

        ResizeRelationshipsIfNecessary();
        int allegiancesSize = allegiances.arraySize;

        EditorGUILayout.PropertyField(allegiances);

        Undo.RecordObject(t, "Allegiance Definition Editor");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("");
        for (int i = allegiancesSize - 1; i >= 0; i--)
        { GUILayout.Label(allegiances.GetArrayElementAtIndex(i).stringValue); }
        EditorGUILayout.EndHorizontal();

        int k = 0;
        for (int i = 0; i < allegiancesSize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(allegiances.GetArrayElementAtIndex(i).stringValue);
            for (int j = 0; j < allegiancesSize; j++)
            {
                if (j < (allegiancesSize - i))
                {
                    if (GUILayout.Button(t.relationships[k].ToString()))
                    {
                        t.relationships[k]++;

                        if ((int)t.relationships[k] >=
                            Enum.GetNames(typeof(AllegianceDefinition.Relationship)).Length)
                        {
                            t.relationships[k] = 0;
                        }
                    }
                    k++;
                }
                else
                {
                    GUILayout.Label("-");
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(t);
    }

    void ResizeRelationshipsIfNecessary()
    {
        int allegiancesSize = allegiances.arraySize;
        int triangularArraySize = CalcTriangularArraySize();

        if ((t.relationships == null) || (t.relationships.Length != triangularArraySize))
        {
            t.relationships = new AllegianceDefinition.Relationship[triangularArraySize];
        }
    }

    int CalcTriangularArraySize()
    {
        int allegiancesSize = allegiances.arraySize;

        int sqrSize = allegiancesSize * allegiancesSize;
        int sqrSizeWithoutDiagonal = sqrSize - allegiancesSize;
        int halfTriangleSize = sqrSizeWithoutDiagonal / 2;
        int finalSize = halfTriangleSize + allegiancesSize;

        return finalSize;
    }

}