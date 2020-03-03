using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Malee.Editor;

[CustomEditor(typeof(CursorIcon))]
public class CursorIconPresetInspector : Editor
{
    ReorderableList cursorPreset;
    SerializedProperty texArray;

    private void OnEnable()
    {
        texArray = serializedObject.FindProperty("cursorIcons");
        cursorPreset = new ReorderableList(texArray);
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        serializedObject.Update();

        GUILayout.Space(8);
        cursorPreset.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

        if (EditorGUI.EndChangeCheck())
        {

        }
    }
}
