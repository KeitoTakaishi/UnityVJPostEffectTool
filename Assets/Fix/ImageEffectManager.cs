using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PostEffect;
public class ImageEffectManager : MonoBehaviour
{
    [SerializeField] PostEffect.Negative negative;
    [SerializeField] PostEffect.Symmetry symmetry;
    [SerializeField] PostEffect.Distortion distortion;
    [SerializeField] PostEffect.RadiationBlur radiationBlur;
    [SerializeField] PostEffect.SobleEdge sobelEdge;
    [SerializeField] PostEffect.Tile tile;
    [SerializeField] PostEffect.SliceGitch sliceGlitch;
    [SerializeField] PostEffect.ScanLine scanLine;
    [SerializeField] PostEffect.WhiteNoiseGlitch whiteNoiseGlitch;
    [SerializeField] PostEffect.Displacement displacement;
    
    [SerializeField] float negativeEffectTime;
    [SerializeField] float symmetryEffectTime;
    [SerializeField] float distortionEffectTime;
    [SerializeField] float radiationBlurEffectTime;
    [SerializeField] float displacementEffectTime;

    bool isNegative = false;
    bool isEdge = false;
    bool isTile = false;
    bool isSliceGlitch = false;
    bool isScanLine = false;
    bool isWhiteNoiseGlitch = false;
    bool isDisplacement = false;


    [SerializeField] float maxDistortionPower;
    [SerializeField] float maxradiationBlurPower;


    IEnumerator NegativeEffect()
    {
        float duration = negativeEffectTime;
        float start = isNegative ? 1 : 0;
        float end = 1f - start;
        float sign = isNegative ? -1.0f : 1.0f;
        isNegative = !isNegative;

        while(duration > 0f)
        {
            duration = Mathf.Max(duration - Time.deltaTime, 0);
            negative.ratio = start+ sign *  Easing.easeInCubic(1f - duration / negativeEffectTime);
            yield return null;
        }

    }

    void SymmetryEffect()
    {
        float r = Random.Range(0.0f, 3.0f);
        if(r < 1.0)
        {
            symmetry.type = 0;
        }else if(r >= 1.0f && r < 2.0f)
        {
            symmetry.type = 1;
        } else
        {
            symmetry.type = 2;
        }
    }

    IEnumerator DistortionEffect()
    {
        float duration = distortionEffectTime;
        while(duration > 0f)
        {
            duration = Mathf.Max(duration - Time.deltaTime, 0);
            distortion.power = maxDistortionPower * Easing.easeInQuad(duration / distortionEffectTime);
            yield return null;
        }
    }

    IEnumerator RadiationBlurEffect()
    {
        float duration = radiationBlurEffectTime;
        while(duration > 0f)
        {
            duration = Mathf.Max(duration - Time.deltaTime, 0);
            //radiationBlur.power = Easing.Ease(EaseType.QuadOut, maxRadiationBlurPower, 1, 1f - duration / effectTime);
            radiationBlur.power = maxradiationBlurPower * Easing.easeInQuad(duration / radiationBlurEffectTime);
            yield return null;
        }
    }

    IEnumerator DisplacementEffectEffect()
    {
        float duration = displacementEffectTime; 
        float start = isDisplacement ? 1 : 0;
        float end = 1f - start;
        float sign = isDisplacement ? -1.0f : 1.0f;
        isDisplacement = !isDisplacement;

        while(duration > 0f)
        {
            duration = Mathf.Max(duration - Time.deltaTime, 0);
            displacement.power = start + sign * Easing.easeInQuad(duration / displacementEffectTime);
            yield return null;
        }

    }

    


    private void ResetEffect()
    {
        sobelEdge.enabled = false;
        tile.enabled = false;
        sliceGlitch.enabled = false;
        scanLine.enabled = false;
        whiteNoiseGlitch.enabled = false;
    }

    void Start()
    {
        ResetEffect();
    }

    //reset作る
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine("NegativeEffect");
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            //StartCoroutine("SymmetryEffect
            SymmetryEffect();
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            StartCoroutine("DistortionEffect");
        }
        else if(Input.GetKeyDown(KeyCode.F4))
        {
            StartCoroutine("RadiationBlurEffect");
        } 
        else if(Input.GetKeyDown(KeyCode.F5))
        {
            //StartCoroutine("RadiationBlurEffect");
            isEdge = !isEdge; 
            if(isEdge)
            {
                sobelEdge.enabled = true;
            } else
            {
                sobelEdge.enabled = false;
            }
        } else if(Input.GetKeyDown(KeyCode.F6))
        {
            isTile = !isTile;
            if(isTile)
            {
                tile.enabled = true;
            } else
            {
                tile.enabled = false;
            }
        } 
        else if(Input.GetKeyDown(KeyCode.F7))
        {
            isSliceGlitch = !isSliceGlitch;
            if(isSliceGlitch)
            {
                sliceGlitch.enabled = true;
            } else
            {
                sliceGlitch.enabled = false;
            }
        } 
        else if(Input.GetKeyDown(KeyCode.F8))
        {
            isScanLine = !isScanLine;
            if(isScanLine)
            {
                float r = Random.Range(0.0f, 1.0f);
                if(r < 0.5f)
                {
                    scanLine.type = 1;
                } else
                {
                    scanLine.type = 2;
                }
                scanLine.enabled = true;
            } else
            {
                scanLine.type = 0;
                scanLine.enabled = false;
            }
        }
        else if(Input.GetKeyDown(KeyCode.F9))
        {
            isWhiteNoiseGlitch = !isWhiteNoiseGlitch;
            if(isWhiteNoiseGlitch)
            {
                whiteNoiseGlitch.enabled = true;
            } else
            {
                whiteNoiseGlitch.enabled = false;
            }
        } 
        else if(Input.GetKeyDown(KeyCode.F10))
        {
            StartCoroutine("DisplacementEffectEffect");
        }
    }
}
