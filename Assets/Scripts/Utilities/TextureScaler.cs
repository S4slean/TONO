using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class TextureScaler : MonoBehaviour
{
    private Transform self;
    private Renderer rd;

    public void Awake()
    {
        self = transform;
        rd = GetComponent<Renderer>();
    }

    private void Update()
    {
        rd.material.SetFloat("_ScaleX", self.localScale.x );
        rd.material.SetFloat("_ScaleY", self.localScale.y);
        rd.material.SetFloat("_ScaleZ", self.localScale.z);
    }



}
