using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RendererSettingsHelper : MonoBehaviour
{
    [SerializeField] UniversalRendererData rendererData;
    public UniversalRendererData rd;

    public TMP_Text labelNettete;

    // concerne la netteté et le flou pour l'expé avec les voyants => ce sont des assets à download en plus dans le projet (sharpen et blur)


    public void Awake()
    {
        var blurFeature = rendererData.rendererFeatures.OfType<SharpenUrp>().FirstOrDefault();

        if (blurFeature == null) return;


        blurFeature.settings.Sharpness = 0;

        rendererData.SetDirty();
    }

    public void changeSharpen(float value)
    {
        var blurFeature = rd.rendererFeatures.OfType<SharpenUrp>().FirstOrDefault();
        Debug.Log("je suis dans la modif de netteté");
        Debug.Log(blurFeature);

        if (blurFeature == null) return;

        blurFeature.settings.Sharpness = value;
        int val = (int)(value * 100);
        string valeur = val.ToString();
        labelNettete.text = valeur + " %";

        rd.SetDirty();

    }
}
