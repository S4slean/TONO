using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    SerializedProperty tileListProp;

    private void OnEnable()
    {
        tileListProp = serializedObject.FindProperty("freeTiles");
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Get All Tiles"))
        {
            GridManager instance = serializedObject.targetObject as GridManager;
            instance.GetAllTiles();
        }

        EditorGUILayout.PropertyField(tileListProp);

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(target);
    }
}
