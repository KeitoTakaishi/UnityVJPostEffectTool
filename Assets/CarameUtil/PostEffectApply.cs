using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectApply : MonoBehaviour
{

    [SerializeField]
    Material invertColorMaterial;
    [SerializeField]
    Material zoomMaterial;
    [SerializeField]
    Material rgbShiftMaterial;
    [SerializeField]
    Material glitchMaterial;
    [SerializeField]
    Material gridFlashMaterial;


    [SerializeField] private float effectSpan = 120;
    [SerializeField] private bool isEffectSwitch = false;
    private List<Material> materials;
    [SerializeField]private Material curMat;
    void Start()

    {
        materials = new List<Material>();
        materials.Add(invertColorMaterial);
        materials.Add(zoomMaterial);
        materials.Add(rgbShiftMaterial);
        materials.Add(glitchMaterial);
        materials.Add(gridFlashMaterial);

        //curMat = invertColorMaterial;
      
    }

    void Update()
    {
        if (isEffectSwitch)
        {
            if (Time.frameCount % effectSpan == 0)
            {
                curMat = materials[Random.RandomRange(0, materials.Count)];
            }
        }
       
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, curMat);
    }
}
