using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PostEffect
{
    public class Tile : MonoBehaviour
    {
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
            material.SetInt("_tileNum", 2);
            Graphics.Blit(source, destination, material);
        }
    }
}
