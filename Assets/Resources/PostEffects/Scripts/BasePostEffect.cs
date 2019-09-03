using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PostEffect
{
    public class BasePostEffect : MonoBehaviour
    {
        #region Public Property
        public string shaderName
        {
            get { return _shaderName; }
            set { _shaderName = value; }
        }

        public Shader shader
        {
            get {
                if(_shader == null)
                {
                    _shader = Shader.Find(shaderName);
                }
                return _shader;
            }
            set { _shader = value; }
        }
        public Material material
        {
            get {
               
                if (_material == null)
                {
                    _material = new Material(shader);
                }
                return _material;
                
            }
            set { _material = value; }
        }
        #endregion


        #region Serialized data members
        [SerializeField] private Material _material;
        private string _shaderName;
        private Shader _shader;
        #endregion


        public virtual void Update()
        {
            
        }
    }
}
