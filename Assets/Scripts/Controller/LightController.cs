using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    // tout ce qui concerne la lumière (menu lumière et menu jeux de lumière)

    public TMP_Dropdown lightSourceDropDown;
    public TMP_Text labelStaticLightTemperature;
    public TMP_Text labelStaticLightIntensity;
    public TMP_Text labelDynamicLightIntensity;

    public Light mainLight;
    public Light topLight;
    public Light downLight;

    public Light lightTarget;

    public Light DynamicLight;

    public Tuple<Vector3, float, float> defaultValuePrincipal { get; set; }
    public Tuple<Vector3, float, float> defaultValueHaut { get; set; }
    public Tuple<Vector3, float, float> defaultValueBas { get; set; }
    public GameObject dollyLightModelParent;

    public bool dollyLightModelSpeed = false;

    public float staticLightIntensity;
    public float staticLightTemperature;

    public float initialStaticLightIntensity;
    public float initialStaticLightTemperature;

    public Slider slideLum;
    public Slider slideTemp;

    public Slider slidIntJL;

    void Start()
    {
        lightTarget = mainLight;
        initialStaticLightIntensity = staticLightIntensity;
        initialStaticLightTemperature = staticLightTemperature;
    }

    private void Update()
    {
        ModifLightTarget();
    }

    public void SetPositionX(float val)
    {
        Vector3 pos = mainLight.gameObject.transform.position;
        pos.x = val;
        mainLight.gameObject.transform.position = pos;
    }
    public void SetPositionY(float val)
    {
        Vector3 pos = mainLight.gameObject.transform.position;
        pos.y = val;
        mainLight.gameObject.transform.position = pos;
    }

    public void SetRotationX(float val)
    {
        Quaternion pos = mainLight.gameObject.transform.rotation;
        pos.x = val;
        mainLight.gameObject.transform.rotation = pos;
    }
    public void SetRotationY(float val)
    {
        Quaternion pos = mainLight.gameObject.transform.rotation;
        pos.y = val;
        mainLight.gameObject.transform.rotation = pos;
    }

    public void ModifLightTarget()
    {
        lightTarget.intensity = staticLightIntensity;
        lightTarget.colorTemperature = staticLightTemperature;
    }
}
