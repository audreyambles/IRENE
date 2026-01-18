using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;
using System;
using Cinemachine;
using UnityEngine.EventSystems;

public class VisualizationManager : MonoBehaviour
{
    #region variables

    [Header("Appearence variables")]
    public TMP_Text sharpnessLabel;
    public TMP_Text contrasteLabel;
    public TMP_Text luminosityLabel;
    public TMP_Text saturationLabel;

    [Header("Outlines variables")]
    public RenderPipelineAsset DEPTHSOBELRenderPipelineAsset;
    public RenderPipelineAsset NORMALSSOBELRenderPipelineAsset;
    public RenderPipelineAsset COLORSOBELRenderPipelineAsset;
    public RenderPipelineAsset NEUTRALRenderPipelineAsset;

    public RendererSettingsHelper rendererSettingsHelper;

    public UniversalRendererData DEPTHrendererData;
    public UniversalRendererData NORMALrendererData;
    public UniversalRendererData COLORrendererData;
    public UniversalRendererData NEUTRALrendererData;

    public Material DEPTHMaterial;
    public Material NORMALSMaterial;
    public Material COLORMaterial;

    public TMP_Dropdown outlinesTypeDropDown;
    public TMP_Text outlinesBordersLabel;
    public int colorOutlinesUserVar;
    public float thicknessOutlinesUserVar; 

    public Color colorOutlines;
    private Color orange;
    private Color violet;
    public float borderOutlines = 0.000594468f;

    public Material _OutlineMaterial;

    public float contrasteUserVar;
    public float intensityUserVar;
   
    //warning to class name
    private CameraController cameraController;
    private LightController lightController;
    private Volume postprocess;
    private MenuController menuController;

    #endregion

    public void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        lightController = FindObjectOfType<LightController>();
        postprocess = FindObjectOfType<Volume>();
        menuController = FindObjectOfType<MenuController>();

        orange = new Color(1, 0.524f, 0, 0);
        violet = new Color(0.6901961f, 0, 1, 0);
    }

    public void ResetAllVisualTreatments()
    {
        ResetPosition();
        ResetZoom();
        ResetAppearanceSettings();
        outlinesTypeDropDown.value = 3;
        SetOutlinesType();
        ResetStaticLights();
        ResetDynamicLight();
    }

    #region CAMERA
    public void ResetPosition()
    {
        cameraController.rotation = cameraController.cameraRotation;
        cameraController.resetPosition = true;
    }

    public void ResetZoom()
    {
        cameraController._cameraOffset = cameraController.startPosition;
        cameraController.transform.position = cameraController.objectTransform.position + cameraController._cameraOffset;
    }

    public void Navigate()
    {
        if (cameraController.resetPosition == true)
        {
            cameraController.x = 0;
            cameraController.y = 0;
            cameraController.resetPosition = false;
        }
        if (Input.GetMouseButton(0))
        {
            cameraController.x += Input.GetAxis("Mouse X") * cameraController.rotationSpeed;
            cameraController.y -= Input.GetAxis("Mouse Y") * cameraController.rotationSpeed;

            cameraController.y = Mathf.Clamp(cameraController.y, cameraController.minVerticalAngle, cameraController.maxVerticalAngle);

            cameraController.rotation = Quaternion.Euler(cameraController.y, cameraController.x, 0);

        }

    }

    public void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKey(KeyCode.KeypadPlus) && cameraController.IsCollision())
        {
            cameraController._cameraOffset.z = cameraController._cameraOffset.z + 0.01f;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            cameraController._cameraOffset.z = cameraController._cameraOffset.z - 0.01f;
        }
        if (scrollInput > 0 && cameraController.IsCollision())
        {
            cameraController._cameraOffset.z = cameraController._cameraOffset.z + 0.1f;
        }
        if (scrollInput < 0)
        {
            cameraController._cameraOffset.z = cameraController._cameraOffset.z - 0.1f;
        }
    }
    #endregion

    #region APPEARANCE

    public void ResetAppearanceMenu()
    {
        ResetAppearanceSettings();

        GameObject.Find("SliderSharpness").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderContrast").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderBrightness").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderSaturation").GetComponent<Slider>().value = 0;
    }

    public void ResetAppearanceSettings()
    {
        SetSharpen(0);
        SetVolumeSaturation(0);
        SetVolumeContrast(0);
        SetVolumeLuminosity(0);
    }

    public void SetVolumeSaturation(float val)
    {
        Debug.Log("Fonction ChangeVolumeSaturation");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.saturation.value = val;
        int valeurEntier = (int)val;
        string valeur = valeurEntier.ToString();
        saturationLabel.text = valeur + " %";
    }

    public void SetVolumeLuminosity(float val)
    {
        Debug.Log("Fonction ChangeVolumeLuminosite");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.postExposure.value = val;
        intensityUserVar = val;
        float valTemp = val * 100;
        int valEntier = (int)valTemp;
        string valeur = valEntier.ToString();
        luminosityLabel.text = valeur + " %";
    }

    public void SetVolumeContrast(float val)
    {
        Debug.Log("Fonction ChangeVolumeContraste");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.contrast.value = val;
        contrasteUserVar = val;
        int valeurEntier = (int)val;
        string valeur = valeurEntier.ToString();
        contrasteLabel.text = valeur + " %";
    }

    public void SetSharpen(float value)
    {
        var blurFeature = rendererSettingsHelper.rd.rendererFeatures.OfType<SharpenUrp>().FirstOrDefault();
        if (blurFeature == null) return;

        blurFeature.settings.Sharpness = value;

        int val = (int)(value * 100);
        string valeur = val.ToString();
        sharpnessLabel.text = valeur + " %";

        rendererSettingsHelper.rd.SetDirty();

    }


    #endregion

    #region OUTLINES

    public void SetOutlinesType()
    {
        if (outlinesTypeDropDown.value == 0)
        {
            GraphicsSettings.renderPipelineAsset = DEPTHSOBELRenderPipelineAsset;
            _OutlineMaterial = DEPTHMaterial;
            SetOutlinesProperties();
            rendererSettingsHelper.rd = DEPTHrendererData;
        }
        else if (outlinesTypeDropDown.value == 1)
        {
            GraphicsSettings.renderPipelineAsset = NORMALSSOBELRenderPipelineAsset;
            _OutlineMaterial = NORMALSMaterial;
            SetOutlinesProperties();
            rendererSettingsHelper.rd = NORMALrendererData;
        }
        else if (outlinesTypeDropDown.value == 2)
        {
            GraphicsSettings.renderPipelineAsset = COLORSOBELRenderPipelineAsset;
            _OutlineMaterial = COLORMaterial;
            SetOutlinesProperties();
            rendererSettingsHelper.rd = COLORrendererData;
        }
        else
        {
            GraphicsSettings.renderPipelineAsset = NEUTRALRenderPipelineAsset;
            
            rendererSettingsHelper.rd = NEUTRALrendererData;
        }
    }

    public void SetOutlinesProperties()
    {
        _OutlineMaterial.SetColor("_Color", colorOutlines);
        _OutlineMaterial.SetFloat("_Thickness", borderOutlines);
    }

    public void SetOutlinesBorders(float value)
    {
        borderOutlines = value;
        thicknessOutlinesUserVar = value;
        float valueTempBorder = value * Screen.width;
        int valRounded = (int)valueTempBorder;
        if (valRounded == 0) valRounded = 1;
        string valueToString = valRounded.ToString();
        outlinesBordersLabel.text = valueToString;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorBlack()
    {
        colorOutlines = Color.black;
        colorOutlinesUserVar = 1;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorWhite()
    {
        colorOutlines = Color.white;
        colorOutlinesUserVar = 2;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorRed()
    {
        colorOutlines = Color.red;
        colorOutlinesUserVar = 3;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorGreen()
    {
        colorOutlines = Color.green;
        colorOutlinesUserVar = 4;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorYellow()
    {
        colorOutlines = Color.yellow;
        colorOutlinesUserVar = 5;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorBlue()
    {
        colorOutlines = Color.blue;
        colorOutlinesUserVar = 6;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorOrange()
    {
        colorOutlines = orange;
        colorOutlinesUserVar = 7;
        SetOutlinesProperties();
    }

    public void SetOutlinesColorViolet()
    {
        colorOutlines = violet;
        colorOutlinesUserVar = 8;
        SetOutlinesProperties();
    }

    #endregion

    #region STATIC LIGHTS

    public void ResetStaticLights()
    {
        SetStaticLightIntensity(lightController.initialStaticLightIntensity);
        SetStaticLightTemperature(lightController.initialStaticLightTemperature);


        lightController.mainLight.intensity = lightController.initialStaticLightIntensity;
        lightController.topLight.intensity = lightController.initialStaticLightIntensity;
        lightController.downLight.intensity = lightController.initialStaticLightIntensity;

        lightController.mainLight.colorTemperature = lightController.initialStaticLightTemperature;
        lightController.topLight.colorTemperature = lightController.initialStaticLightTemperature;
        lightController.downLight.colorTemperature = lightController.initialStaticLightTemperature;

        lightController.lightTarget = lightController.mainLight;

        lightController.topLight.gameObject.SetActive(false);
        lightController.downLight.gameObject.SetActive(false);
        lightController.mainLight.gameObject.SetActive(true);

        lightController.slideLum.value = lightController.initialStaticLightIntensity;
        lightController.slideTemp.value = lightController.initialStaticLightTemperature;
    }

    public void SetStaticLightSource()
    {
        if (lightController.lightSourceDropDown.value == 0)
        {
            lightController.mainLight.gameObject.SetActive(true);
            lightController.topLight.gameObject.SetActive(false);
            lightController.downLight.gameObject.SetActive(false);
            lightController.lightTarget = lightController.mainLight;
        }
        else if (lightController.lightSourceDropDown.value == 1)
        {
            lightController.topLight.gameObject.SetActive(true);
            lightController.mainLight.gameObject.SetActive(false);
            lightController.downLight.gameObject.SetActive(false);
            lightController.lightTarget = lightController.topLight;
        }
        else if (lightController.lightSourceDropDown.value == 2)
        {
            lightController.downLight.gameObject.SetActive(true);
            lightController.topLight.gameObject.SetActive(false);
            lightController.mainLight.gameObject.SetActive(false);
            lightController.lightTarget = lightController.downLight;
        }
    }

    public void SetStaticLightIntensity(float value)
    {
        lightController.staticLightIntensity = value;
        int valueIntensityPourcentage = (int)value * 10;
        string valueToString = valueIntensityPourcentage.ToString();
        lightController.labelStaticLightIntensity.text = valueToString + " %";
    }

    public void SetStaticLightTemperature(float value)
    {
        lightController.staticLightTemperature = value;
        if (lightController.staticLightTemperature < 8608)
        {
            lightController.labelStaticLightTemperature.text = "Chaude";
        }
        else if (lightController.staticLightTemperature > 12099)
        {
            lightController.labelStaticLightTemperature.text = "Froide";
        }
        else
        {
            lightController.labelStaticLightTemperature.text = "Neutre";
        }

    }

    

    #endregion

    #region DYNAMIC LIGHTS

    public void ResetDynamicLight()
    {
        lightController.DynamicLight.gameObject.transform.parent.GetComponent<CinemachineDollyCart>().m_Position = 0;
        lightController.DynamicLight.gameObject.transform.parent.GetComponent<CinemachineDollyCart>().m_Speed = 0;
        SetIntensityDynamicLight(0);

        lightController.DynamicLight.enabled = false;

        lightController.slidIntJL.value = 0;

    }

    public void ActivateDynamicLight()
    {
        lightController.DynamicLight.enabled = !lightController.DynamicLight.enabled;

        if (lightController.DynamicLight.intensity == 0)
        {
            SetIntensityDynamicLight(3);
            lightController.DynamicLight.gameObject.transform.parent.GetComponent<CinemachineDollyCart>().m_Speed = 1;
        }
    }

    public void DesactivateDynamicLight()
    {
        lightController.DynamicLight.enabled = false;
    }

    public void SetIntensityDynamicLight(float value)
    {
        if (lightController.DynamicLight.enabled)
        {
            lightController.DynamicLight.intensity = value;
            int valueTempIntensity = (int)value;
            string valueToString = valueTempIntensity.ToString();
            lightController.labelDynamicLightIntensity.text = valueToString + " %";
        }
    }

    #endregion
}
