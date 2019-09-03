using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class SobelEdge : BasePostEffect
{
    const string SHADER_NAME = "Hidden/SobelEdge";
    int _pIdWidth;
    int _pIdHeight;

    private void Awake()
    {
        shaderName = SHADER_NAME;
        _pIdWidth = Shader.PropertyToID("_width");
        _pIdHeight = Shader.PropertyToID("_height"); 
    }
    void Start()
    {
        
    }

    void Update()
    {
        material.SetFloat(_pIdWidth, Screen.width);
        material.SetFloat(_pIdHeight, Screen.height);
    }
}
