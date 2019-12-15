using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PostEffect
{
    public class Negative : MonoBehaviour
    {
        [Range(0.0f, 1.0f)]
        public float ratio;
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
            material.SetFloat("ratio", ratio);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }
    }
}
