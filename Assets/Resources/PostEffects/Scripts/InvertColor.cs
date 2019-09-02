using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class InvertColor : BasePostEffect
{
    const string SHADER_NAME = "Hidden/InvertColor";


    private void Awake()
    {
        shaderName = SHADER_NAME;
    }
}