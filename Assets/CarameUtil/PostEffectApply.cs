using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PostEffect;
using System.Linq;

public enum SwitchModes
{
    HumanMode,
    AutoMode,
    MomentaryHumanMode

};

public enum PosteffecTypes
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

[RequireComponent(typeof(InvertColor))]
[RequireComponent(typeof(Zoom))]
[RequireComponent(typeof(RGBShift))]
[RequireComponent(typeof(Glitch))]
[RequireComponent(typeof(GridFlash))]
[RequireComponent(typeof(VerticalSymmetry))]
[RequireComponent(typeof(HorizontalSymmetry))]
[RequireComponent(typeof(Mosaic))]
[RequireComponent(typeof(Tile))]
[RequireComponent(typeof(Feedback))]
[RequireComponent(typeof(SobelEdge))]
public class PostEffectApply : MonoBehaviour
{
    #region serialized data 
    [SerializeField] private float effectSpan = 120;
    private Material curMat;
    #endregion

    #region private data
    private List<Material> materials;
    private List<bool> isEffectOn;
    Material throughMaterial;
    Material invertColorMaterial;
    Material zoomMaterial;
    Material rgbShiftMaterial;
    Material glitchMaterial;
    Material gridFlashMaterial;
    Material horizontalSymmetryMaterial;
    Material verticalSymmetryMaterial;
    Material mosaicMaterial;
    Material tileMaterial;
    Material feedbackMaterial;
    Material edgeMaterial;
    BasePostEffect postEffect, temp;
    #endregion



    #region effect parameters used only MomentaryHumanMode
    [SerializeField] [Range(0, 240)] int invertColorTime = 50;
    [SerializeField] [Range(0, 120)] int zoomTime = 50;
    [SerializeField] [Range(0, 300)] float zoomPower = 10.0f;
    [SerializeField] [Range(0, 100)] int rgbshiftTime = 10;
    [SerializeField] [Range(0.0f, 0.1f)] float offSet = 0.01f;
    [SerializeField] [Range(0, 240)] int glitchTime = 50;
    [SerializeField] [Range(0, 240)] int gridFlashTime = 50;
    [SerializeField] [Range(0, 240)] int horizontalSymmetryTime = 50;
    [SerializeField] [Range(0, 240)] int verticalSymmetryTime = 50;
    [SerializeField] [Range(0, 240)] int mosaicTime = 50;
    [SerializeField] [Range(0, 240)] int mosaicSize = 50;
    [SerializeField] [Range(0, 240)] int tileTime = 50;
    [SerializeField] [Range(0, 10)] int tileNum = 5;
    [SerializeField] [Range(0, 240)] int feedBackTime = 20;
    [SerializeField] [Range(0, 240)] int edgeTime = 20;


    #endregion

    #region enum
    [SerializeField] private SwitchModes switchMode = SwitchModes.HumanMode;
    [SerializeField] PosteffecTypes posteffectType = PosteffecTypes.INVERTCOLOR;
    
    public SwitchModes SwitchMode
    {
        get { return switchMode; }
        set { switchMode = value; }
    }

    public PosteffecTypes PosteffectType
    {
        get { return posteffectType; }
        set { posteffectType = value; }
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
        if (SwitchMode == SwitchModes.AutoMode)
        {
            if (Time.frameCount % effectSpan == 0)
            {
                curMat = materials[Random.Range(0, materials.Count)];
            }
        }
        //human
        else if(SwitchMode == SwitchModes.HumanMode)
        {
            inputs();
            switch (PosteffectType)
            {
                case PosteffecTypes.TROUGH:
                    curMat = throughMaterial;
                    break;
                case PosteffecTypes.INVERTCOLOR:
                    curMat = invertColorMaterial;
                    break;
                case PosteffecTypes.ZOOM:
                    curMat = zoomMaterial;
                    break;
                case PosteffecTypes.RGBSHIFT:
                    curMat = rgbShiftMaterial;
                    break;
                case PosteffecTypes.GLITCH:
                    curMat = glitchMaterial;
                    break;
                case PosteffecTypes.GRIDFLASH:
                    curMat = gridFlashMaterial;
                    break;
                case PosteffecTypes.VERTICALSYMMETRY:
                    curMat = verticalSymmetryMaterial;
                    break;
                case PosteffecTypes.HORIZONTALSYMMETRY:
                    curMat = horizontalSymmetryMaterial;
                    break;
                case PosteffecTypes.MOSAIC:
                    curMat = mosaicMaterial;
                    break;
                case PosteffecTypes.TILE:
                    curMat = tileMaterial;
                    break;
                case PosteffecTypes.FEEDBACK:
                    curMat = feedbackMaterial;
                    break;
                case PosteffecTypes.EDGE:
                    curMat = edgeMaterial;
                    break;

            }
        }else if(SwitchMode == SwitchModes.MomentaryHumanMode)
        {
            inputs();
            switch(PosteffectType)
            {
                case PosteffecTypes.INVERTCOLOR:
                    if(!isEffectOn[1])
                    {
                        StartCoroutine("invertColorCoroutine");
                    }
                    break;

                case PosteffecTypes.ZOOM:
                    if(!isEffectOn[2])
                    {
                        StartCoroutine("zoomCoroutine");
                    }
                    break;
               
                case PosteffecTypes.RGBSHIFT:
                    if(!isEffectOn[3])
                    {
                        StartCoroutine("rgbshiftCoroutine");
                    }
                    break;

                case PosteffecTypes.GLITCH:
                    if(!isEffectOn[4])
                    {
                        StartCoroutine("glitchCoroutine");
                    }
                    break;

                case PosteffecTypes.GRIDFLASH:
                    if(!isEffectOn[5])
                    {
                        StartCoroutine("gridFlashCoroutine");
                    }
                    break;
                case PosteffecTypes.VERTICALSYMMETRY:
                    if(!isEffectOn[6])
                    {
                        StartCoroutine("verticalSymmetryCoroutine");
                    }
                    break;
                case PosteffecTypes.HORIZONTALSYMMETRY:
                    if(!isEffectOn[7])
                    {
                        StartCoroutine("horizontalSymmetryCoroutine");
                    }
                    break;
                case PosteffecTypes.MOSAIC:
                    if(!isEffectOn[8])
                    {
                        StartCoroutine("mosaicCoroutine");
                    }
                    break;
                case PosteffecTypes.TILE:
                    if(!isEffectOn[9])
                    {
                        StartCoroutine("tileCoroutine");
                    }
                    break;
                case PosteffecTypes.FEEDBACK:
                    if(!isEffectOn[10])
                    {
                        StartCoroutine("feedBackCoroutine");
                    }
                    break;
                case PosteffecTypes.EDGE:
                    if(!isEffectOn[11])
                    {
                        StartCoroutine("edgeCoroutine");
                    }
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
        throughMaterial = GetComponent<Through>().material;
        invertColorMaterial = GetComponent<InvertColor>().material;
        zoomMaterial = GetComponent<Zoom>().material;
        rgbShiftMaterial = GetComponent<RGBShift>().material;
        glitchMaterial = GetComponent<Glitch>().material;
        gridFlashMaterial = GetComponent<GridFlash>().material;
        horizontalSymmetryMaterial = GetComponent<HorizontalSymmetry>().material;
        verticalSymmetryMaterial = GetComponent<VerticalSymmetry>().material;
        mosaicMaterial = GetComponent<Mosaic>().material;
        tileMaterial = GetComponent<Tile>().material;
        feedbackMaterial = GetComponent<Feedback>().material;
        edgeMaterial = GetComponent<SobelEdge>().material;
      


        materials = new List<Material>();
        materials.Add(throughMaterial);
        materials.Add(invertColorMaterial);
        materials.Add(zoomMaterial);
        materials.Add(rgbShiftMaterial);
        materials.Add(glitchMaterial);
        materials.Add(gridFlashMaterial);
        materials.Add(horizontalSymmetryMaterial);
        materials.Add(verticalSymmetryMaterial);
        materials.Add(mosaicMaterial);
        materials.Add(tileMaterial);
        materials.Add(feedbackMaterial);
        materials.Add(edgeMaterial);

        isEffectOn = Enumerable.Repeat(false, materials.Count).ToList();

        curMat = throughMaterial;
        PosteffectType = PosteffecTypes.TROUGH;
    }


    void inputs()
    {
       
        if (Input.GetKeyDown(KeyCode.F1))
        {
            posteffectType = PosteffecTypes.TROUGH;
            postEffect = this.GetComponent<Through>();
            postEffect.IsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            posteffectType = PosteffecTypes.INVERTCOLOR;
            postEffect = this.GetComponent<InvertColor>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[1] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            posteffectType = PosteffecTypes.ZOOM;
            postEffect = this.GetComponent<Zoom>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[2] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            posteffectType = PosteffecTypes.RGBSHIFT;
            postEffect = this.GetComponent<RGBShift>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[3] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            posteffectType = PosteffecTypes.GLITCH;
            postEffect.GetComponent<Glitch>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[4] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            posteffectType = PosteffecTypes.GRIDFLASH;
            postEffect = this.GetComponent<GridFlash>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[5] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            posteffectType = PosteffecTypes.VERTICALSYMMETRY;
            postEffect = this.GetComponent<VerticalSymmetry>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[6] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            posteffectType = PosteffecTypes.HORIZONTALSYMMETRY;
            postEffect = this.GetComponent<HorizontalSymmetry>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[7] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            posteffectType = PosteffecTypes.MOSAIC;
            postEffect = this.GetComponent<Mosaic>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[8] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            posteffectType = PosteffecTypes.TILE;
            postEffect = this.GetComponent<Tile>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[9] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            posteffectType = PosteffecTypes.FEEDBACK;
            postEffect = this.GetComponent<Feedback>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[10] = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            posteffectType = PosteffecTypes.EDGE;
            postEffect = this.GetComponent<SobelEdge>();
            postEffect.IsActive = true;

            if(SwitchMode == SwitchModes.MomentaryHumanMode)
            {
                isEffectOn[11] = false;
            }
        }

        if(SwitchMode == SwitchModes.HumanMode)
        {
            if(temp != postEffect)
            {
                if(temp != null)
                {
                    temp.IsActive = false;
                }
            }

            temp = postEffect;
        }
    }


    #region Coroutines
    private IEnumerator invertColorCoroutine()
    {
        isEffectOn[1] = true;
        curMat = invertColorMaterial;
        for(int i = 0; i <  invertColorTime; i++)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator zoomCoroutine()
    {
        isEffectOn[2] = true;
        curMat = zoomMaterial;
        for(int i = zoomTime; i > 0; i--)
        {
            curMat.SetFloat("_Strength", i* zoomPower);
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator rgbshiftCoroutine() {
        isEffectOn[3] = true;
        curMat = rgbShiftMaterial;
        for(int i = rgbshiftTime; i > 0; i--)
        {

            curMat.SetFloat("_offSet1", Random.Range(-offSet, offSet));
            curMat.SetFloat("_offSet2", Random.Range(-offSet, offSet));
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator glitchCoroutine()
    {
        isEffectOn[4] = true;
        curMat = glitchMaterial;
        for(int i = glitchTime; i > 0; i--)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator gridFlashCoroutine()
    {
        isEffectOn[5] = true;
        curMat = gridFlashMaterial ;
        for(int i = gridFlashTime; i > 0; i--)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator verticalSymmetryCoroutine()
    {
        isEffectOn[6] = true;
        curMat = verticalSymmetryMaterial;
        for(int i = verticalSymmetryTime; i > 0; i--)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator horizontalSymmetryCoroutine()
    {
        isEffectOn[7] = true;
        curMat = horizontalSymmetryMaterial;
        for(int i = horizontalSymmetryTime; i > 0; i--)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator mosaicCoroutine()
    {
        isEffectOn[8] = true;
        curMat = mosaicMaterial;
        for(int i = 0; i < mosaicTime; i++)
        {
            curMat.SetFloat("_blockSize", i * mosaicSize);
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }
   
    private IEnumerator tileCoroutine()
    {
        isEffectOn[9] = true;
        curMat = tileMaterial;
        curMat.SetInt("_tileNum", (Random.Range(1, tileNum)));
        for(int i = 0; i < tileTime; i++)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator feedBackCoroutine()
    {
        isEffectOn[10] = true;
        curMat = feedbackMaterial;
        for(int i = 0; i < feedBackTime; i++)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    private IEnumerator edgeCoroutine()
    {
        isEffectOn[11] = true;
        curMat = edgeMaterial;
        for(int i = 0; i < edgeTime; i++)
        {
            yield return null;
        }
        curMat = throughMaterial;
        posteffectType = PosteffecTypes.TROUGH;
        yield break;
    }

    #endregion
}
