using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class RGBShift : BasePostEffect
{
    const string SHADER_NAME = "Hidden/RGBShift";
    int _pIdOffset1;
    int _pIdOffset2;
    int _pIdMode;

    [SerializeField] [Range(-1.0f, 1.0f)]
    float offSet1;
    [SerializeField]
    [Range(-1.0f, 1.0f)]
    float offSet2;
    [SerializeField]
    [Range(0, 2)]
    int _mode;


    private void Awake()
    {
        shaderName = SHADER_NAME;
        _pIdOffset1 = Shader.PropertyToID("_offSet1");
        _pIdOffset2 = Shader.PropertyToID("_offSet2");
        _pIdMode = Shader.PropertyToID("_mode");
    }

    void Start()
    {
    }

    void Update()
    {
        material.SetFloat(_pIdOffset1, offSet1);
        material.SetFloat(_pIdOffset2, offSet2);
        material.SetInt(_pIdMode, _mode);
    }
}
