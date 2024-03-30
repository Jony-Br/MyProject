using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ArrayEditor))] 
public class MultiDimensionalArrayEditor : Editor
{
    private SerializedProperty boolArray;
    private int numRows = 10; 
    private int numCols = 10; 
    private Vector2[] scrollPos;

    private void OnEnable()
    {
        boolArray = serializedObject.FindProperty("boolArray");
        scrollPos = new Vector2[numRows];
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Двумірний, але насправді одномірний масив:");
        for (int row = 0; row < numRows; row++)
        {
            scrollPos[row] = EditorGUILayout.BeginScrollView(scrollPos[row], GUILayout.Width(EditorGUIUtility.currentViewWidth), GUILayout.Height(20)); 
            EditorGUILayout.BeginHorizontal();
            for (int col = 0; col < numCols; col++)
            {
                int index = row * numCols + col;
                if (index < boolArray.arraySize)
                {
                    boolArray.GetArrayElementAtIndex(index).boolValue = EditorGUILayout.Toggle(boolArray.GetArrayElementAtIndex(index).boolValue);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView(); 
        }

        serializedObject.ApplyModifiedProperties();
    }
}
