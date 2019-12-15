using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PostEffect
{
    public class Symmetry : MonoBehaviour
    {
        public int type = 0;
        [SerializeField] Shader shader;
        private Material material;

        void Start()
        {
            if(material == null)
            {
                material = new Material(shader);
            }
        }

        void Update()
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

        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }
    }
}