using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TextureScaler : MonoBehaviour
{
    private Transform self;
    private Renderer rd;
    private MaterialPropertyBlock _propBlock;

    private void Start()
    {
        UpdateMaterial();
    }

    public void UpdateMaterial()
    {
        self = transform;
        _propBlock = new MaterialPropertyBlock();
        rd = GetComponent<Renderer>();

        rd.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_ScaleX", self.localScale.x);
        _propBlock.SetFloat("_ScaleY", self.localScale.y);
        _propBlock.SetFloat("_ScaleZ", self.localScale.z);
        rd.SetPropertyBlock(_propBlock);
    }
}

[CustomEditor(typeof(TextureScaler))]
public class TextureScalerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Update Material Scale"))
        {
            TextureScaler scaler = serializedObject.targetObject as TextureScaler;
            scaler.UpdateMaterial();
        }

        serializedObject.ApplyModifiedProperties();

        
    }
}

