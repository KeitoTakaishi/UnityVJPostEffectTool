using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PostEffect
{
    public class RadiationBlur : MonoBehaviour
    {
        public Vector2 center = new Vector2(0.5f, 0.5f);
        [Range(0, 100)]
        public float power = 0f;
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
            material.SetVector("_BlurCenter", center);
            material.SetFloat("_BlurPower", power);
            Graphics.Blit(source, destination, material);
        }
    }
}
