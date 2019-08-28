using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBShift : MonoBehaviour
{
    [SerializeField] private Material m_Material;


    [SerializeField] [Range(-1.0f, 1.0f)]
    float offSet1;

    [SerializeField]
    [Range(-1.0f, 1.0f)]
    float offSet2;

    void Start()
    {
        
    }

    void Update()
    {
        m_Material.SetFloat("_offSet1", offSet1);
        m_Material.SetFloat("_offSet2", offSet2);
    }
}
