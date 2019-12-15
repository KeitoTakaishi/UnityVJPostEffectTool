using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PostEffect
{
    public class SobleEdge : MonoBehaviour
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
            Graphics.Blit(source, destination, material);
        }
    }
}
