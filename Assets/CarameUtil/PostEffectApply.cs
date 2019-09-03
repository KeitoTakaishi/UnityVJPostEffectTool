using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;

[RequireComponent(typeof(InvertColor))]
[RequireComponent(typeof(GridFlash))]
[RequireComponent(typeof(Glitch))]
[RequireComponent(typeof(HorizontalSymmetry))]
[RequireComponent(typeof(VerticalSymmetry))]
[RequireComponent(typeof(RGBShift))]
[RequireComponent(typeof(Zoom))]
[RequireComponent(typeof(Mosaic))]
[RequireComponent(typeof(Tile))]
[RequireComponent(typeof(Feedback))]
[RequireComponent(typeof(SobelEdge))]


public class PostEffectApply : MonoBehaviour
{

    #region serialized data 
    [SerializeField] private float effectSpan = 120;
    [SerializeField] private bool isAutoSwitch = false;
    private Material curMat;
    #endregion

    #region private data
    private List<Material> _materials;
    Material _throughMaterial;
    Material _invertColorMaterial;
    Material _zoomMaterial;
    Material _rgbShiftMaterial;
    Material _glitchMaterial;
    Material _gridFlashMaterial;
    Material _horizontalSymmetryMaterial;
    Material _verticalSymmetryMaterial;
    Material _mosaicMaterial;
    Material _tileMaterial;
    Material _feedbackMaterial;
    Material _edgeMaterial;
    BasePostEffect _postEffect, _temp;
    #endregion


    #region enum
    public enum POSTEFFECT_TYPE
    {
        TROUGH,
        INVERTCOLOR,
        ZOOM,
        RGBSHIFT,
        GLITCH,
        GRIDFLASH,
        VERTICALSYMMETRY,
        HORIZONTALSYMMETRY,
        MOSAIC,
        TILE,
        FEEDBACK,
        EDGE,
    }

    POSTEFFECT_TYPE _type = POSTEFFECT_TYPE.INVERTCOLOR;
    #endregion


    #region property
    public POSTEFFECT_TYPE Type
    {
        get { return _type; }
        set { _type = value; }
    }

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
        //Automatically
        if (isAutoSwitch)
        {
            if (Time.frameCount % effectSpan == 0)
            {
                curMat = _materials[Random.RandomRange(0, _materials.Count)];
            }
        }
        //human
        else
        {
            inputs();
            switch (_type)
            {
                case POSTEFFECT_TYPE.TROUGH:
                    curMat = _throughMaterial;
                    break;
                case POSTEFFECT_TYPE.INVERTCOLOR:
                    curMat = _invertColorMaterial;
                    break;
                case POSTEFFECT_TYPE.ZOOM:
                    curMat = _zoomMaterial;
                    break;
                case POSTEFFECT_TYPE.RGBSHIFT:
                    curMat = _rgbShiftMaterial;
                    break;
                case POSTEFFECT_TYPE.GLITCH:
                    curMat = _glitchMaterial;
                    break;
                case POSTEFFECT_TYPE.GRIDFLASH:
                    curMat = _gridFlashMaterial;
                    break;
                case POSTEFFECT_TYPE.VERTICALSYMMETRY:
                    curMat = _verticalSymmetryMaterial;
                    break;
                case POSTEFFECT_TYPE.HORIZONTALSYMMETRY:
                    curMat = _horizontalSymmetryMaterial;
                    break;
                case POSTEFFECT_TYPE.MOSAIC:
                    curMat = _mosaicMaterial;
                    break;
                case POSTEFFECT_TYPE.TILE:
                    curMat = _tileMaterial;
                    break;
                case POSTEFFECT_TYPE.FEEDBACK:
                    curMat = _feedbackMaterial;
                    break;
                case POSTEFFECT_TYPE.EDGE:
                    curMat = _edgeMaterial;
                    break;

            }
        }
       
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, curMat);
    }

    void init()
    {
        _throughMaterial = GetComponent<Through>().material;
        _invertColorMaterial = GetComponent<InvertColor>().material;
        _zoomMaterial = GetComponent<Zoom>().material;
        _rgbShiftMaterial = GetComponent<RGBShift>().material;
        _glitchMaterial = GetComponent<Glitch>().material;
        _gridFlashMaterial = GetComponent<GridFlash>().material;
        _horizontalSymmetryMaterial = GetComponent<HorizontalSymmetry>().material;
        _verticalSymmetryMaterial = GetComponent<VerticalSymmetry>().material;
        _mosaicMaterial = GetComponent<Mosaic>().material;
        _tileMaterial = GetComponent<Tile>().material;
        _feedbackMaterial = GetComponent<Feedback>().material;
        _edgeMaterial = GetComponent<SobelEdge>().material;
      


        _materials = new List<Material>();
        _materials.Add(_throughMaterial);
        _materials.Add(_invertColorMaterial);
        _materials.Add(_zoomMaterial);
        _materials.Add(_rgbShiftMaterial);
        _materials.Add(_glitchMaterial);
        _materials.Add(_gridFlashMaterial);
        _materials.Add(_horizontalSymmetryMaterial);
        _materials.Add(_verticalSymmetryMaterial);
        _materials.Add(_mosaicMaterial);
        _materials.Add(_tileMaterial);
        _materials.Add(_feedbackMaterial);
        _materials.Add(_edgeMaterial);
       

        if(curMat == null)
        {
            curMat = _edgeMaterial;
        }
    }


    void inputs()
    {
       
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _type = POSTEFFECT_TYPE.TROUGH;
            _postEffect = this.GetComponent<Through>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            _type = POSTEFFECT_TYPE.INVERTCOLOR;
            _postEffect = this.GetComponent<InvertColor>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            _type = POSTEFFECT_TYPE.ZOOM;
            _postEffect = this.GetComponent<Zoom>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            _type = POSTEFFECT_TYPE.RGBSHIFT;
            _postEffect = this.GetComponent<RGBShift>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            _type = POSTEFFECT_TYPE.GLITCH;
            _postEffect.GetComponent<Glitch>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            _type = POSTEFFECT_TYPE.GRIDFLASH;
            _postEffect = this.GetComponent<GridFlash>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            _type = POSTEFFECT_TYPE.VERTICALSYMMETRY;
            _postEffect = this.GetComponent<VerticalSymmetry>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            _type = POSTEFFECT_TYPE.HORIZONTALSYMMETRY;
            _postEffect = this.GetComponent<HorizontalSymmetry>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            _type = POSTEFFECT_TYPE.MOSAIC;
            _postEffect = this.GetComponent<Mosaic>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            _type = POSTEFFECT_TYPE.TILE;
            _postEffect = this.GetComponent<Tile>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            _type = POSTEFFECT_TYPE.FEEDBACK;
            _postEffect = this.GetComponent<Feedback>();
            _postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            _type = POSTEFFECT_TYPE.EDGE;
            _postEffect = this.GetComponent<SobelEdge>();
            _postEffect.IsActive = true;    
        }

        
        if(_temp != _postEffect)
        {
            if (_temp != null)
            {
                _temp.IsActive = false;
            }
        }

        _temp = _postEffect;
    }
}
