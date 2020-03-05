using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
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

    private void Update()
    {
        rd.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_Scale X", self.localScale.x);
        _propBlock.SetFloat("_Scale Y", self.localScale.y);
        _propBlock.SetFloat("_Scale z", self.localScale.z);
        rd.SetPropertyBlock(_propBlock);
    }



}
