using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class VerticalSymmetry : BasePostEffect
{
    const string SHADER_NAME = "Hidden/VerticalSymmetry";


    private void Awake()
    {
        shaderName = SHADER_NAME;
    }

}
