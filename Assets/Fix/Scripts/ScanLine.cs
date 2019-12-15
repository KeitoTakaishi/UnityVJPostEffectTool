using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PostEffect
{
    public class ScanLine : MonoBehaviour
    {
        [SerializeField]
        [Range(0.0f, 1.0f)]
        float max;
        public int type;

        [SerializeField]
        Shader shader;
        Material material;
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
            if(type == 0)
            {
                Shader.EnableKeyword("_");
                Shader.DisableKeyword("HORIZONTAL");
                Shader.DisableKeyword("VERTICAL");
            } else if(type == 1)
            {
                Shader.EnableKeyword("VERTICAL");
                Shader.DisableKeyword("HORIZONTAL");
                Shader.DisableKeyword("_");
            } else if(type == 2)
            {
                Shader.EnableKeyword("HORIZONTAL");
                Shader.DisableKeyword("VERTICAL");
                Shader.DisableKeyword("_");
            }

            material.SetFloat("max", max);
            Graphics.Blit(source, destination, material);
        }
    }
}
