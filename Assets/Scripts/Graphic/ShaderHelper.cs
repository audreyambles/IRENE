using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
/// Retourne valeurs expos�es li�s aux post-process/shaders.
/// </summary>
public class ShaderHelper : MonoBehaviour
{
    //il y a du tri à faire, bcp de fonctions de gabriel à supprimer + mettre au propre et charger correctement tous les shaders, etc
    // utilisé pour apparences (saturation, luminosité et contraste), et contours

  public RenderPipelineAsset DEPTHSOBELRenderPipelineAsset;
  public RenderPipelineAsset NORMALSSOBELRenderPipelineAsset;
  public RenderPipelineAsset COULEURSOBELRenderPipelineAsset;
  public RenderPipelineAsset COULDEPTHSOBELRenderPipelineAsset;
  public RenderPipelineAsset COULNORMSOBELRenderPipelineAsset;
  public RenderPipelineAsset PROFNORMSOBELRenderPipelineAsset;
  public RenderPipelineAsset ALLSOBELRenderPipelineAsset;
  public RenderPipelineAsset NEUTRERenderPipelineAsset;

  public RendererSettingsHelper rendererSettingsHelper;

  public UniversalRendererData DEPTHrendererData;
  public UniversalRendererData NORMALrendererData;
  public UniversalRendererData COLORrendererData;
  public UniversalRendererData COLORDEPTHrendererData;
  public UniversalRendererData COLORNORMALrendererData;
  public UniversalRendererData DEPTHNORMALrendererData;
  public UniversalRendererData ALLrendererData;
  public UniversalRendererData NEUTRErendererData;

    public Color color;
    public Color orange;
    public Color violet;
    public float border = 0.000594468f;
    public TMP_Text labelContraste;
    public TMP_Text labelLuminosite;
    public TMP_Text labelSaturation;
    public TMP_Text labelBordureContours;

    //public ScriptableRendererFeature feature;


    #region variables
    [Header("Debug Use")]
    [SerializeField] private List<MeshRenderer> target;
    [SerializeField] private List<Material> targetM;
    [Header("Debug Use")]
    [SerializeField] private GameObject postprocess;
    [Header("Materials")]
    public List<Material> materials;
    public Material _OutlineMaterial;
    private Material OutlineMaterial;


    public Material DEPTHMaterial;
    public Material NORMALSMaterial;
    public Material COLORMaterial;
    public Material COLORNORMMaterial;
    public Material COLORDEPTHMaterial;
    public Material DEPTHNORMMaterial;
    public Material ALLMaterial;
    //public Material _TestMaterial;
    //private Material TestMaterial;
    //public List<Material> Defmaterials;
    public Material _DefaultMaterial;
    private Material DefaultMaterial;
    public Material _GradientMaterial;
    private Material GradientMaterial;
    [Header("Conteneurs pour shader parametres")]
    // pourquoi ??
    public GameObjectListOnOff ConteneurParam;
    [SerializeField] private GameObject outlineParam;
    [SerializeField] private GameObject sinParam;
    [SerializeField] private GameObject defaultParam;
    [SerializeField] private GameObject gradientParam;


    public Color maColor;


    public TMP_Dropdown choix;


    #endregion
    #region helper functions
    void Start()
    {
      //GraphicsSettings.renderPipelineAsset = NEUTRERenderPipelineAsset;
      Debug.Log("je suis dans le start de shader helper");
    //_OutlineMaterial = SOBELRenderPipelineAsset.defaultMaterial;
    // !! attention pour le moment il faut bien penser à changer le rebder pipeline asset renderer material + le outline contour de notre shader helper !!!
    // normalement tout fonctionne plus ou moins niveau fonctionnalités...
    // il faut ajouter un menu dans les contours pour selectionner le type de contour
    // je pense commencer
        DefaultMaterial = new Material(_DefaultMaterial);
        OutlineMaterial = new Material(_OutlineMaterial);
        GradientMaterial = new Material(_GradientMaterial);
        //TestMaterial = new Material(_TestMaterial);
        target = ConfigScene.Instance.mesh;
        //targetM = ConfigScene.Instance.materialsL;*/
       // target.material = DefaultMat;
        postprocess = FindObjectOfType<Volume>().gameObject;
        /*int i=0;
        foreach(MeshRenderer a in target)
        {
            a.material = targetM[i];
            i=i+1;
        }*/
        //resetParam();
    }
    public void Reinitialiser()
    {
      Debug.Log("je suis dans le réinistialiser de Shader helper");
        DefaultMaterial = new Material(_DefaultMaterial);
        OutlineMaterial = new Material(_OutlineMaterial);
        GradientMaterial = new Material(_GradientMaterial);
        //TestMaterial = new Material(_TestMaterial);

        foreach (MeshRenderer a in target)
        {
            a.material.CopyPropertiesFromMaterial(DefaultMaterial);
        }
        //resetParam();
    }


    // peut etre que c'est ce qui affiche la bonne partie du menu ? pour les contours etc
    /*private void GenerationParam(GameObject param)
    {
      Debug.Log(" je suis dans génération param de shader heloper");
        ConteneurParam.SetActiveOnOffSingle(param);
    }

    private void resetParam()
    {
    Debug.Log(" je suis dans reset param de shader heloper");
        ConteneurParam.SetAllActive(false);
    }*/

    #endregion
    #region post-process
    //Post-processing URP, acc�s par profile du volume
    public void ChangeVolumeSaturation(float val)
    {
        Debug.Log(" je suis dans change volume saturation de shader heloper début");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.saturation.value = val;
        int valeurEntier = (int)val;
        string valeur = valeurEntier.ToString();
        labelSaturation.text = valeur + " %";
    }

    public void AddToVolumeSaturation(float val)
    {
      Debug.Log(" je suis dans ajouter au volume la saturation de shader heloper");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.saturation.value += val;
    }

    public void ChangeVolumeLuminosite(float val)
    {
      Debug.Log(" je suis dans add volume lumiere de shader heloper");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.postExposure.value = val;
        float valTemp = val * 100;
        int valEntier = (int)valTemp;
        string valeur = valEntier.ToString();
        labelLuminosite.text = valeur + " %";
    }

    public void ChangeFondScene(float val)
    {
        Debug.Log("suis dans la fonction changement couleur de fond");
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Camera cam = MainCamera.GetComponent<Camera>();
        //Color lerpedColor = Color.black;
        cam.backgroundColor = Color.Lerp(Color.white, Color.black, val);
    }


    public void ChangeVolumeContrast(float val)
    {
      Debug.Log(" je suis dans change volume contraste de shader heloper");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.contrast.value = val;
        int valeurEntier = (int)val;
        string valeur = valeurEntier.ToString();
        labelContraste.text = valeur + " %";
    }
    public void AddToVolumeContrast(float val)
    {
      Debug.Log(" je suis dans add volume contraste de shader heloper");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.contrast.value += val;
    }

    public void ChangeVolumeHue(float val)
    {
      Debug.Log(" je suis dans change volume teinte de shader heloper");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.hueShift.value = val;
    }
    public void AddToVolumeHue(float val)
    {
      Debug.Log(" je suis dans ajouter volume teinte de shader heloper");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.hueShift.value += val;
    }

    #endregion
    #region ChangeMaterials
    public void changeMaterialGradient()
    {
      Debug.Log("je suis dans le change material gradient");
        foreach (MeshRenderer target in target)
        {
            target.material = GradientMaterial;
        }
        //GenerationParam(gradientParam);

    }
    /*public void changeMaterialTest()
    {
        foreach (MeshRenderer target in target)
        {
            target.material = TestMaterial;
        }
        resetParam();
    }*/

    public void modifBorderShader(float value)
    {
        border = value;
        float valTemp = value * 10000;
        int valArrondie = (int) valTemp;
        string valeur = valArrondie.ToString();
        labelBordureContours.text = valeur;
        changeMaterialContour();
    }

    public void modifColorShader1()
    {
        color = Color.black;
        changeMaterialContour();
    }

    public void modifColorShader2()
    {
        color = Color.white;
        changeMaterialContour();
    }

    public void modifColorShader3()
    {
        color = Color.red;
        changeMaterialContour();
    }

    public void modifColorShader4()
    {
        color = Color.green;
        changeMaterialContour();
    }

    public void modifColorShader5()
    {
        color = Color.yellow;
        changeMaterialContour();
    }

    public void modifColorShader6()
    {
        color = Color.blue;
        changeMaterialContour();
    }

    public void modifColorShader7()
    {
        color = orange;
        changeMaterialContour();
    }

    public void modifColorShader8()
    {
        color = violet;
        changeMaterialContour();
    }

    public void modifColorShader(int value)
    {
        if (value == 0)
        {
            color = Color.white;
        }
        else if (value == 1)
        {
            color = Color.black;
        }
        else if (value == 2)
        {
            color = Color.red;
        }
        else if (value == 3)
        {
            color = Color.blue;
        }
        else if (value == 4)
        {
            color = Color.green;
        }
        else if (value == 5)
        {
            color = Color.yellow;
        }

        changeMaterialContour();
    }

    public void changeMaterialContour()
    {
      //le dropdown est en cours
      // il faut que je pense à changer le renderPipeline ET le material associé
      // et là pour l'instant ça ne marche pas : on a un changement de render pipeline mais ça fait du bordel (alors que sans ça, ça marchait, il fallait le Materiel du RP = materiel du script)

      //string valeur = choix.options[choix.value].text;
      //string valeur = choix.value;
      if(choix.value==0)
      {

        GraphicsSettings.renderPipelineAsset = DEPTHSOBELRenderPipelineAsset;
            _OutlineMaterial = DEPTHMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = DEPTHrendererData;
      }
      else if(choix.value==1)
      {
        GraphicsSettings.renderPipelineAsset = NORMALSSOBELRenderPipelineAsset;
            //_OutlineMaterial.color = color;
            _OutlineMaterial = NORMALSMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = NORMALrendererData;
      }
      else if(choix.value==2)
      {
        GraphicsSettings.renderPipelineAsset = COULEURSOBELRenderPipelineAsset;
            
            _OutlineMaterial = COLORMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = COLORrendererData;
      }
      else if(choix.value==3)
      {
        GraphicsSettings.renderPipelineAsset = COULDEPTHSOBELRenderPipelineAsset;
            
            _OutlineMaterial = COLORDEPTHMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = COLORDEPTHrendererData;
      }
      else if(choix.value==4)
      {
        GraphicsSettings.renderPipelineAsset = COULNORMSOBELRenderPipelineAsset;
            
            _OutlineMaterial = COLORNORMMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = COLORNORMALrendererData;
      }
      else if(choix.value==5)
      {
        GraphicsSettings.renderPipelineAsset = PROFNORMSOBELRenderPipelineAsset;
            
            _OutlineMaterial = DEPTHNORMMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = DEPTHNORMALrendererData;
      }
      else if(choix.value==6)
      {
        GraphicsSettings.renderPipelineAsset = ALLSOBELRenderPipelineAsset;
        _OutlineMaterial = ALLMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = ALLrendererData;
      }
      else
      {
        GraphicsSettings.renderPipelineAsset = NEUTRERenderPipelineAsset;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = NEUTRErendererData;
      }

      //feature.SetActive(false);
      Debug.Log(" je suis dans change materiel contour de shader heloper");
  /*      foreach (MeshRenderer target in target)
        {
            target.material = OutlineMaterial;
            Debug.Log("matériel = " + target.material);
        }

        GenerationParam(outlineParam);
    }*/
  }

    public void changeMaterialBetweenNormalAndContour()
    {
      Debug.Log(" je suis dans change materiel entre contour et normal dans le shader heloper");
        if(target[1].sharedMaterial.name == DefaultMaterial.name)
        {
            foreach (MeshRenderer target in target)
            {

                target.material = OutlineMaterial;
            }

            //GenerationParam(outlineParam);

        } else
        {
            foreach (MeshRenderer target in target)
            {

                target.material = DefaultMaterial;
            }
            //resetParam();

        }
    }

    /*public void ReturnToDefaultMaterial()
    {
      Debug.Log(" je suis dans return le materiel par défaut de shader heloper");
        foreach (MeshRenderer target in target)
        {

            target.material = DefaultMaterial;

        }
        resetParam();
    }*/
    public void changeMaterial(Material m)
    {
      Debug.Log(" je suis dans change materiel (materiel m) de shader heloper");
        foreach (MeshRenderer target in target)
        {

            target.material = m;
        }
    }
    /// <summary>
    /// Change le material selon l'int de la liste voulu.
    /// </summary>
    /// <param name="m"></param>
    public void changeMaterial(int m)
    {
      Debug.Log(" je suis dans change materiel (int m) de shader heloper");
        foreach (MeshRenderer target in target)
        {
            target.material = materials[m];
        }

        //GenerationParam(sinParam);
    }
    #endregion
    #region ShaderVariables
    //R�glage des valeurs du shader par des variables expos�es (voir les r�f�rences dans Shader Graph)
    public void AccessColorShader(Color c)
    {
      Debug.Log(" je suis dans acces color shadr de shader heloper");
      //Debug.Log(c);
      //Debug.Log("avant " + _OutlineMaterial.color);
      //Debug.Log(OutlineMaterial.color);
      //OutlineMaterial.SetColor("_Color", c);
      _OutlineMaterial.SetColor("_Color", c);
      //Debug.Log("après " + _OutlineMaterial.color);

      //feature.SetActive(true);

      //Debug.Log(OutlineMaterial.color);
      //changeMaterialContour();
      //Debug.Log(SOBELRenderPipelineAsset.);
      //Debug.Log("material de render pip : " + SOBELRenderPipelineAsset.BlitMaterialFeature);
      //Debug.Log("COLOR material de render pip : " + SOBELRenderPipelineAsset.defaultMaterial.color);
      //Debug.Log()
      //GraphicsSettings.renderPipelineAsset = SOBELRenderPipelineAsset;
      /*  foreach (MeshRenderer target in target)
        {
            target.material.SetColor("_OutlineColor", c);
        }*/
    }

    public void AccessDepthPowerShader(float value)
    {
      Debug.Log(" je suis dans acces profondeur power shader de shader heloper");
        foreach(MeshRenderer target in target) {
        target.material.SetFloat("_OutlineDepthStrength", value);
        }
    }    public void AccessNormalsPowerShader(float value)
    {
      Debug.Log(" je suis dans acces normal power shader de shader heloper");
        foreach(MeshRenderer target in target) {
        target.material.SetFloat("_OutlineNormalStrength", value);
        }
    }

    public void AccessThicknessShader(float value)
    {
      Debug.Log(" je suis dans acces épaisseur shadr de shader heloper");
        foreach(MeshRenderer target in target) {
        target.material.SetFloat("_OutlineThickness", value);
        }
    }

    public void AccessDepthTighteningShader(float value)
    {
      Debug.Log(" je suis dans acces profondeur amincissement shadr de shader heloper");
        foreach(MeshRenderer target in target) {
        target.material.SetFloat("_OutlineDepthTightening", value);
        }
    }
    public void AccessNormalsTighteningShader(float value)
    {
      Debug.Log(" je suis dans acces normal amincissement shadr de shader heloper");
        foreach(MeshRenderer target in target) {
        target.material.SetFloat("_OutlineNormalTightening", value);
        }
    }
    public void NormalOnShader()
    {
      Debug.Log(" je suis dans normal on shadr de shader heloper");
        float v = target[0].material.GetFloat("_NormalsOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        foreach (MeshRenderer target in target) {
        target.material.SetFloat("_NormalsOn", v);
        }
    }

    public bool getNormalsShader()
    {
      Debug.Log(" je suis dans get normales shadr de shader heloper");
        float v = target[0].material.GetFloat("_NormalsOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        bool c = v == 1 ? false : true;
        return c;
    }
    public void DepthOnShader()
    {
      Debug.Log(" je suis dans profondeur shadr de shader heloper");
        float v = target[0].material.GetFloat("_DepthOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        foreach (MeshRenderer target in target) {
        target.material.SetFloat("_DepthOn", v);
        }
    }

    public bool getDepthShader()
    {
      Debug.Log(" je suis dans get profondeur shadr de shader heloper");
        float v = target[0].material.GetFloat("_DepthOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        bool c = v == 1 ? false : true;
        return c;

    }

    public void CoefDepthShader(float value)
    {
      Debug.Log(" je suis dans coef profondeur shadr de shader heloper");
        foreach(MeshRenderer target in target) {
        target.material.SetFloat("_CoefficientDepth", value);
        }
    }

    public void CoefNormalsShader(float value)
    {
      Debug.Log(" je suis dans coef normals shadr de shader heloper");
        foreach(MeshRenderer target in target) {
        target.material.SetFloat("_CoefficientNormals", value);
        }
    }

    public void ContourStrictShader()
    {
      Debug.Log(" je suis dans contours stricts shadr de shader heloper");
        float v = target[0].material.GetFloat("_ContourStrict");
        Debug.Log(v);
        v = resolveIntBool(v);
        foreach(MeshRenderer target in target)
        {
            target.material.SetFloat("_ContourStrict", v);
        }
    }

    public string getCurrentMaterialShader()
    {
      Debug.Log(" je suis dans get materiel current shader de shader heloper");
        return target[0].sharedMaterial.name;
    }


    /// <summary>
    /// Les proprietes du shader ne permettent pas de manipuler des bools pour leur variable bool, il faut donc utiliser un int : 1 = vrai, 0 = faux
    /// Cette methode est pour simplement inverser chaque fois qu'on souhaite activer/desactiver un bool du shader
    /// </summary>
    /// <param name="v">La valeur du slider associe</param>
    /// <returns></returns>
    public int resolveIntBool(float v)
    {
Debug.Log(" je suis dans resolve int bool de shader heloper");
        if (v == 1)
        {
            v = 0;
        }
        else
        {
            v = 1;
        }
        return (int) v;
    }
    public void AnimTimeShader(float value)
    {
      Debug.Log(" je suis dans anim time shadr de shader heloper");
        foreach (MeshRenderer target in target)
        {
        target.material.SetInt("_OutlineTimeAnim",(int) value);
        }
    }
    #endregion
}
