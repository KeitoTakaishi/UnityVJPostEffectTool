using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Material m_material;


    [Range(1, 30)]
    public int loopNum = 30;
    [Range (-10.0f, 10.0f)]
    public float strength = 1.0f;
    void Start()
    {
        loopNum = 30;
        strength = 1.0f;
    }

    void Update()
    {
        m_material.SetFloat("_LoopNum", (float)loopNum);
        m_material.SetFloat("_Strength", strength);
        m_material.SetFloat("_Width", Screen.width);
        m_material.SetFloat("_Height", Screen.height);
    }
}
