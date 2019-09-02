using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class Glitch : BasePostEffect
{
    
    const string SHADER_NAME = "Hidden/Glitch";
    int _pIdWidth;
    int _pIdHeight;
    int _pIdThreshould;
    int _pIdFiness;
    int _pIdSpeed;
   

    [SerializeField] [Range(1.0f, 1000.0f)]
    float _blockWidth = 1.0f;
    [SerializeField] [Range(1.0f, 1000.0f)]
    float _blockHeight = 500.0f;
    [SerializeField][Range(0.0f, 1.0f)]
    float _threshould = 0.35f;
    [SerializeField] [Range(0.0f, 200.0f)]
    float _finess = 100.0f;
    [SerializeField]
    [Range(0.0f, 300.0f)]
    float _speed = 50.0f;



    void Awake()
    {
        shaderName = SHADER_NAME;
        _pIdWidth = Shader.PropertyToID("_BlockWidth");
        _pIdHeight = Shader.PropertyToID("_BlockHeight");
        _pIdThreshould = Shader.PropertyToID("_Threshould");
        _pIdFiness = Shader.PropertyToID("_Fineness");
        _pIdSpeed = Shader.PropertyToID("_Speed");

    }

    void Start()
    {
       
        
    }

    void Update()
    {
        
    }

    private void OnRenderObject()
    {
        material.SetFloat(_pIdWidth, _blockWidth);
        material.SetFloat(_pIdHeight, _blockHeight);
        material.SetFloat(_pIdThreshould, _threshould);
        material.SetFloat(_pIdFiness, _finess);
        material.SetFloat(_pIdSpeed, _speed);
    }
}
