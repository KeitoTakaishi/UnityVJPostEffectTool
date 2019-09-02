using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class GridFlash : BasePostEffect
{
    const string SHADER_NAME = "Hidden/GridFlash";


    private void Awake()
    {
        shaderName = SHADER_NAME;
    }
}
