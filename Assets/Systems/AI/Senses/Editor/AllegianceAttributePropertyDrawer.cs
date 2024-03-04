using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AllegianceAttribute))]
public class AllegianceAttributePropertyDrawer : PropertyDrawer
{
    string[] availableAllegiances;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        PopulateAvailableAllegiances();

        Rect labelPosition = position;
        labelPosition.width /= 2f;
        GUI.Label(labelPosition, property.displayName);

        Rect popupPosition = position;
        popupPosition.width /= 2f;
        popupPosition.x += position.width / 2f;
        int index = CalcIndex(property);
        if (index == -1)
        {
            index = 0;
            property.stringValue = availableAllegiances[0];
        }

        int allegianceIndex = EditorGUI.Popup(popupPosition, index, availableAllegiances);
        property.stringValue = availableAllegiances[allegianceIndex];

        property.serializedObject.ApplyModifiedProperties();
    }

    private void PopulateAvailableAllegiances()
    {
        string[] GUIDs = AssetDatabase.FindAssets("t:AllegianceDefinition");
        string assetPath = AssetDatabase.GUIDToAssetPath(GUIDs[0]);

        AllegianceDefinition definition = AssetDatabase.LoadAssetAtPath<AllegianceDefinition>(assetPath);
        availableAllegiances = (string[])definition.allegiances.Clone();
    }

    int CalcIndex(SerializedProperty property)
    {
        return Array.FindIndex(availableAllegiances, x => x == property.stringValue);
    }
}
