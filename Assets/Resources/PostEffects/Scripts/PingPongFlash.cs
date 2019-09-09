using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class PingPongFlash : BasePostEffect
{
    const string SHADER_NAME = "Hidden/PingPongFlash";

    void Start()
    {
        shaderName = SHADER_NAME;    
    }

    void Update()
    {
        
    }
}
