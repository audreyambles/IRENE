using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class RendererSettingsHelper : MonoBehaviour
{
    [SerializeField] UniversalRendererData rendererData;
    public UniversalRendererData rd;

    public void Awake()
    {
        var blurFeature = rendererData.rendererFeatures.OfType<SharpenUrp>().FirstOrDefault();
        if (blurFeature == null) return;
        blurFeature.settings.Sharpness = 0;
        rendererData.SetDirty();
    }
}
