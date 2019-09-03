using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;


public class Mosaic : BasePostEffect
{
    const string SHADER_NAME = "Hidden/Mosaic";
    int _pIdBlockSize;
    int _pIdHeight, _pIdWidth;
    [SerializeField][Range(1.0f, 100.0f)]
    float _blockSize = 1.0f;

    private void Awake()
    {
        shaderName = SHADER_NAME;
        _pIdBlockSize = Shader.PropertyToID("_blockSize");
        _pIdHeight = Shader.PropertyToID("_height");
        _pIdWidth = Shader.PropertyToID("_width");
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (this.IsActive)
        {
            material.SetFloat(_pIdBlockSize, _blockSize);
            material.SetFloat(_pIdHeight, Screen.height);
            material.SetFloat(_pIdWidth, Screen.width);
        }
    }
}
