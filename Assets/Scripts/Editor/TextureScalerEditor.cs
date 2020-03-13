using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureScaler))]
public class TextureScalerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update Material Scale"))
        {
            TextureScaler scaler = serializedObject.targetObject as TextureScaler;
            scaler.UpdateMaterial();
        }

        serializedObject.ApplyModifiedProperties();


    }
}
