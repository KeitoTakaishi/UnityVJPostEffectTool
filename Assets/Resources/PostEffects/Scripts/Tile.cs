using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class Tile : BasePostEffect
{
    const string SHADER_NAME = "Hidden/Tile";
    int _pIdTileNum;

    [SerializeField]
    [Range(0, 10)]
    int _tileNum;
    

    private void Awake()
    {
        shaderName = SHADER_NAME;
        _pIdTileNum = Shader.PropertyToID("_tileNum");
        //_tileNum = 3;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (this.IsActive)
        {
            //material.SetInt(_pIdTileNum, _tileNum);
        }
    }
}
