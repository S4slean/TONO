using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TextureScaler : MonoBehaviour
{
    private Transform self;
    private Renderer rd;
    private MaterialPropertyBlock _propBlock;

    public void Awake()
    {
        self = transform;
        _propBlock = new MaterialPropertyBlock();
        rd = GetComponent<Renderer>();
    }

    public void Update()
    {
        rd.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_Scale X", self.localScale.x);
        _propBlock.SetFloat("_Scale Y", self.localScale.y);
        _propBlock.SetFloat("_Scale z", self.localScale.z);
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
            //scaler.UpdateMaterial();
        }

        serializedObject.ApplyModifiedProperties();

        
    }
}

