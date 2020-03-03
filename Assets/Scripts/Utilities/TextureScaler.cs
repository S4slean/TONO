using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextureScaler : MonoBehaviour
{
    public Transform self;
    public Renderer rd;

    private void Update()
    {
        rd.material.SetFloat("Vector1_D5FBFB1E", self.localScale.x );
        rd.material.SetFloat("Vector1_D8985208", self.localScale.y);
        rd.material.SetFloat("Vector1_1AE57C88", self.localScale.z);
    }

}
