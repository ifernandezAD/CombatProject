using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(CivilianAnimationAttribute))]
public class CivilianAnimationAttributePropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the target object
        object targetObject = property.serializedObject.targetObject;

        // Traverse the inheritance hierarchy to find the field
        string[] availableCivilianAnimations = FindAnimationsField(targetObject);

        // If the animations array is found
        if (availableCivilianAnimations != null)
        {
            // Fetching the current value
            string currentValue = property.stringValue;

            // Finding the index of the current value in the available animations array
            int currentIndex = Array.IndexOf(availableCivilianAnimations, currentValue);

            // Displaying the dropdown
            int newIndex = EditorGUI.Popup(position, "Animation", currentIndex, availableCivilianAnimations);

            // If the selected index has changed, update the property value
            if (newIndex != currentIndex)
            {
                property.stringValue = availableCivilianAnimations[newIndex];
            }
        }
        else
        {
            EditorGUI.LabelField(position, "Animations field not found");
        }
    }

    // Method to find the animations field in the inheritance hierarchy
    private string[] FindAnimationsField(object targetObject)
    {
        // Loop through the type hierarchy to find the field
        Type targetType = targetObject.GetType();
        while (targetType != null)
        {
            // Check if the field is found
            System.Reflection.FieldInfo animationsField = targetType.GetField("roamingAnimations", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (animationsField != null && animationsField.FieldType == typeof(string[]))
            {
                // If found, return the value of the field
                return (string[])animationsField.GetValue(targetObject);
            }

            // Move up the inheritance chain
            targetType = targetType.BaseType;
        }

        // Field not found, return null
        return null;
    }
}
