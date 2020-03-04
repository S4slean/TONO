using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    private ReorderableList enemyListProp;

    private void OnEnable()
    {
        enemyListProp = new ReorderableList(serializedObject, serializedObject.FindProperty("enemyList"), true, true, true, true);
        enemyListProp.drawElementCallback =
            (Rect rect,int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = enemyListProp.serializedProperty.GetArrayElementAtIndex(index);

                SerializedProperty name = element.FindPropertyRelative("name");

                EditorGUI.PropertyField(rect, element);
            };
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Get All Ennemies"))
        {
            EnemyManager instance = serializedObject.targetObject as EnemyManager;
            instance.GetAllenemies();
        }

        enemyListProp.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(target);
    }
}
