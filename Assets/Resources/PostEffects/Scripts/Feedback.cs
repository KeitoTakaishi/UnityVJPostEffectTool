using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

public class Feedback : BasePostEffect
{
    const string SHADER_NAME = "Hidden/Feedback";
    int[] _pIdRenderTexture = new int[5];
    int _pIdFineness;
    int _pIdFrequcence;
    int _pIdAmp;
    private List<RenderTexture> _renderTextures;
    
   
    [SerializeField] private Camera cam;
    [SerializeField] private int stepNum = 2;
    [SerializeField] [Range(0.0f, 10.0f)] float _fineness;
    [SerializeField] [Range(0.0f, 10.0f)] float _frequcence;
    [SerializeField] [Range(0.0f, 10.0f)] float _amp;

    private void Awake()
    {
        shaderName = SHADER_NAME;
        _pIdRenderTexture[0] = Shader.PropertyToID("_renderTexture1");
        _pIdRenderTexture[1] = Shader.PropertyToID("_renderTexture2");
        _pIdRenderTexture[2] = Shader.PropertyToID("_renderTexture3");
        _pIdRenderTexture[3] = Shader.PropertyToID("_renderTexture4");
        _pIdRenderTexture[4] = Shader.PropertyToID("_renderTexture5");
        _pIdFineness = Shader.PropertyToID("_fineness");
        _pIdFrequcence = Shader.PropertyToID("_frequence");
        _pIdAmp = Shader.PropertyToID("_amp"); 



        _renderTextures = new List<RenderTexture>();
       for(int i = 0; i < 5; i++)
        {
            _renderTextures.Add(new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32));
            _renderTextures[i].Create();
        }
    }


    void Start()
    {
       
    }

    public override void Update()
    {
        Debug.Log(Time.frameCount);
        if (Time.frameCount % 5 == 0)
        {
            cam.GetComponent<Camera>().targetTexture = _renderTextures[0];
            material.SetTexture(_pIdRenderTexture[0], _renderTextures[0]);
        }
        else if (Time.frameCount % 5 == 1){
            cam.GetComponent<Camera>().targetTexture = _renderTextures[1];
            material.SetTexture(_pIdRenderTexture[1], _renderTextures[1]);
        }
        else if (Time.frameCount % 5 == 2)
        {
            cam.GetComponent<Camera>().targetTexture = _renderTextures[2];
            material.SetTexture(_pIdRenderTexture[2], _renderTextures[2]);
        }
        else if (Time.frameCount % 5 == 3)
        {
            cam.GetComponent<Camera>().targetTexture = _renderTextures[3];
            material.SetTexture(_pIdRenderTexture[3], _renderTextures[3]);
        }
        else if (Time.frameCount % 5 == 4)
        {
            cam.GetComponent<Camera>().targetTexture = _renderTextures[4];
            material.SetTexture(_pIdRenderTexture[4], _renderTextures[4]);
        }
        


        material.SetFloat(_pIdFineness, _fineness);
        material.SetFloat(_pIdFrequcence, _frequcence);
        material.SetFloat(_pIdAmp, _amp);
    }
}
