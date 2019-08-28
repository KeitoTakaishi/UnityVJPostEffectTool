using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
{
    [SerializeField] Material m_Material; 
    
    void Start()
    {
        
    }

    void Update()
    {
        m_Material.SetFloat("_Width", Screen.width);
        m_Material.SetFloat("_Height", Screen.height);
    }
}
