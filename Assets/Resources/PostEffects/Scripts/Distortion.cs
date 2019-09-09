using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class Distortion : BasePostEffect
{
    const string SHADER_NAME = "Hidden/Distortion";


    private void Awake()
    {
        shaderName = SHADER_NAME;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(this.IsActive)
        {
           //material.SetFloat("_pivotX",  0.5f + 0.5f*Mathf.Sin(Time.realtimeSinceStartup));
           //material.SetFloat("_pivotX", 0.5f);
           //material.SetFloat("_power", 0.5f);
        }
    }
}
