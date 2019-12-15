using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PostEffect
{
    public class Distortion : MonoBehaviour
    {

        [Range(0, 10)]
        public float noiseScale = 0.1f;
        public Vector3 noiseSpeed = Vector3.one;
        [Range(0, 1)]
        public float power = 0.0f;
        private Vector3 noisePosition;

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

            float dt = Time.deltaTime;
            noisePosition.x += dt * noiseSpeed.x;
            noisePosition.y += dt * noiseSpeed.y;
            noisePosition.z += dt * noiseSpeed.z;
        }

            private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            material.SetFloat("_DistortionNoiseScale", noiseScale);
            material.SetVector("_DistortionNoisePosition", noisePosition);
            material.SetFloat("_DistortionPower", power);
            Graphics.Blit(source, destination, material);
        }
        
    }
}
