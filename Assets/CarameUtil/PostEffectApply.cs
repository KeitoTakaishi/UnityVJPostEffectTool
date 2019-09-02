using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GridFlash))]
[RequireComponent(typeof(Glitch))]
[RequireComponent(typeof(HorizontalSymmetry))]
[RequireComponent(typeof(VerticalSymmetry))]
[RequireComponent(typeof(RGBShift))]
[RequireComponent(typeof(Zoom))]

public class PostEffectApply : MonoBehaviour
{
    #region serialized data 
    [SerializeField] private float effectSpan = 120;
    [SerializeField] private bool isEffectSwitch = false;
    [SerializeField]private Material curMat;
    #endregion

    #region private data
    private List<Material> _materials;
    Material _invertColorMaterial;
    Material _zoomMaterial;
    Material _rgbShiftMaterial;
    Material _glitchMaterial;
    Material _gridFlashMaterial;
    Material _horizontalSymmetryMaterial;
    Material _verticalSymmetryMaterial;
    #endregion


    private void Awake()
    {
       
    }

    void Start()
    {
        init();
    }

    void Update()
    {
        if (isEffectSwitch)
        {
            if (Time.frameCount % effectSpan == 0)
            {
                curMat = _materials[Random.RandomRange(0, _materials.Count)];
            }
        }
       
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, curMat);
    }

    void init()
    {
        _invertColorMaterial = new Material(Resources.Load<Material>("PostEffects/Materials/InvertColorMat"));
        _zoomMaterial = GetComponent<Zoom>().material;
        _rgbShiftMaterial = GetComponent<RGBShift>().material;
        _glitchMaterial = GetComponent<Glitch>().material;
        _gridFlashMaterial = GetComponent<GridFlash>().material;
        _horizontalSymmetryMaterial = GetComponent<HorizontalSymmetry>().material;
        _verticalSymmetryMaterial = GetComponent<VerticalSymmetry>().material;


        _materials = new List<Material>();
        _materials.Add(_invertColorMaterial);
        _materials.Add(_zoomMaterial);
        _materials.Add(_rgbShiftMaterial);
        _materials.Add(_glitchMaterial);
        _materials.Add(_gridFlashMaterial);
        _materials.Add(_horizontalSymmetryMaterial);
        _materials.Add(_verticalSymmetryMaterial);

        if(curMat == null)
        {
            curMat = _gridFlashMaterial;
        }
    }
}
