using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class O_CustomImageEffect : MonoBehaviour
{
    public Material imageEffect;
    public FlexibleColorPicker outlineColor;
    private float outlineWidth = 1f;
        

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        imageEffect.SetFloat("_OutlineWidth", outlineWidth);            // modifie la variable "_OutlineWidth" de type "Float" du matériel dans imageEffects
        imageEffect.SetColor("_OutlineColor", outlineColor.color);      // modifie la variable "_OutlineColor" de type "Color" du matériel dans imageEffects
    
        if (imageEffect != null)
            Graphics.Blit(src, dest, imageEffect);
        else
            dest = src;
    }

    
    public void valueOutlineWidth(float nb)  // permet de changer l'épaisseur des contours via un slider qui appel cette fonction
        {
            outlineWidth = nb;           
        }
}
