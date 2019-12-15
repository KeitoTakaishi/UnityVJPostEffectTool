using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PostEffect
{
    public class SliceGitch : MonoBehaviour
    {

        [SerializeField]
        [Range(1.0f, 1000.0f)]
        float _blockWidth = 1.0f;
        [SerializeField]
        [Range(1.0f, 1000.0f)]
        float _blockHeight = 500.0f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        float _threshould = 0.35f;
        [SerializeField]
        [Range(0.0f, 200.0f)]
        float _finess = 100.0f;
        [SerializeField]
        [Range(0.0f, 300.0f)]
        float _speed = 50.0f;


        Material material;
        [SerializeField]
        Shader shader;

        void Start()
        {
            if(material == null)
            {
                material = new Material(shader);
            }
        }

        void Update()
        {

        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            material.SetFloat("_BlockWidth", _blockWidth);
            material.SetFloat("_BlockHeight", _blockHeight);
            material.SetFloat("_Threshold", _threshould);
            material.SetFloat("_Finess", _finess);
            material.SetFloat("_Speed", _speed);
            Graphics.Blit(source, destination, material);
        }
    }
}
