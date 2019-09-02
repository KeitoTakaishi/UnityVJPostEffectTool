using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class Zoom : BasePostEffect
{
    const string SHADER_NAME = "Hidden/Zoom";
    int _pIdWidth, _pIdHeight;
    int _pIdStrength;
    int _pIdLoopNum;


    [SerializeField][Range(1, 30)]
    int _loopNum = 30;
    [SerializeField][Range (-100.0f, 100.0f)]
    float _strength = 1.0f;

    private void Awake()
    {
        shaderName = SHADER_NAME;
        _pIdWidth = Shader.PropertyToID("_Width");
        _pIdHeight = Shader.PropertyToID("_Height");
        _pIdStrength = Shader.PropertyToID("_Strength");
        _pIdHeight = Shader.PropertyToID("_LoopNum");

        _loopNum = 30;
        _strength = 3.0f;
    }


    void Start()
    {
       
    }

    void Update()
    {
        material.SetFloat(_pIdLoopNum, (float)_loopNum);
        material.SetFloat(_pIdStrength, _strength);
        material.SetFloat(_pIdWidth, Screen.width);
        material.SetFloat(_pIdHeight, Screen.height);
    }
}
