using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public InterfaceCustomizationManager interfaceCustomizationManager;
    public GameObject VisualizationMenu;
    public GameObject InterfaceMenu;
    public GameObject UserMenu;

    public GameObject ObjectsMenu;

    public GameObject fontsMenu;
    public GameObject menuMenu;
    public GameObject backgroundMenu;
    public GameObject closeMenuButton;
    public GameObject resetMenuButton;

    public GameObject cameraMenu;
    public GameObject appearanceMenu;
    public GameObject outlinesMenu;
    public GameObject staticLightsMenu;
    public GameObject dynamicLightsMenu;

    public TMP_Dropdown interfaceMenuDropDown;

    public GameObject selectUserMenu;
    public GameObject createUserMenu;
    public GameObject closeUserMenuButton;
    public TMP_Dropdown userMenuDropDown;

    //load menus before opening the app

    #region private variables

    #endregion

    private void Awake()
    {
        interfaceMenuDropDown = GameObject.Find("DropdownInterface").GetComponent<TMP_Dropdown>();
        //userMenuDropDown = GameObject.Find("DropdownUserActions").GetComponent<TMP_Dropdown>();
    }

    void Start()
    {
        DesactiveSubMenuInterface();
        DesactiveSubMenuVisualization();
        DesactiveGameObject(ObjectsMenu); //TODO = adjusting the behavior of this object
        //OpenUserMenuDropdown(0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F12))  
        {
            ActiveOrDesactiveGameObject(interfaceCustomizationManager.moveMenu);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActiveOrDesactiveGameObject(InterfaceMenu);
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.O))
        {
            ActiveOrDesactiveGameObject(ObjectsMenu);
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.U))
        {
            ActiveOrDesactiveGameObject(UserMenu);
        }

    }

    public void ResetItemsVisualizationMenu()
    {
        appearanceMenu.SetActive(true);
        GameObject.Find("SliderSharpness").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderContrast").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderBrightness").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderSaturation").GetComponent<Slider>().value = 0;
        appearanceMenu.SetActive(false);
        
        outlinesMenu.SetActive(true);
        GameObject.Find("SliderOutlinesBorders").GetComponent<Slider>().value = 0.000594468f;
        //GameObject.Find("DropdownOutlinesChoice").GetComponent<Dropdown>().value = 3;
        outlinesMenu.SetActive(false);

        staticLightsMenu.SetActive(true);
        //GameObject.Find("DropdownLightingChoice").GetComponent<Dropdown>().value = 0;
        GameObject.Find("SliderLightingTemperature").GetComponent<Slider>().value = 10603;
        GameObject.Find("SliderLightingIntensity").GetComponent<Slider>().value = 3;
        staticLightsMenu.SetActive(false);

        dynamicLightsMenu.SetActive(true);
        GameObject.Find("SliderDynamicLightingIntensity").GetComponent<Slider>().value = 0;
        dynamicLightsMenu.SetActive(false);

        BackToVisualizationMenu();
    }

    public void BackToVisualizationMenu()
    {
        VisualizationMenu.SetActive(true);
        DesactiveSubMenuVisualization();
    }

    public void OpenCameraMenu()
    {
        DesactiveGameObject(VisualizationMenu);
        cameraMenu.SetActive(true);
    }

    public void OpenAppearanceMenu()
    {
        DesactiveGameObject(VisualizationMenu);
        appearanceMenu.SetActive(true);
    }

    public void OpenOutlinesMenu()
    {
        DesactiveGameObject(VisualizationMenu);
        outlinesMenu.SetActive(true);
    }

    public void OpenStaticLightsMenu()
    {
        DesactiveGameObject(VisualizationMenu);
        staticLightsMenu.SetActive(true);
    }

    public void OpenDynamicLightsMenu()
    {
        DesactiveGameObject(VisualizationMenu);
        dynamicLightsMenu.SetActive(true);
    }

    public void CloseMenuParam()
    {
        OpenInterfaceMenuDropdown(3);
        interfaceMenuDropDown.value = 3;
    }

    /*public void CloseMenuUser()
    {
        OpenUserMenuDropdown(2);
        userMenuDropDown.value = 2;
    }*/

    public void OpenInterfaceMenuDropdown(int value)
    {
        closeMenuButton.SetActive(true);
        resetMenuButton.SetActive(true);
        //0 = polices, 1 = menu, 2 = fond et 3 = fermer le menu
        if (value == 0)
        {
            fontsMenu.SetActive(true);
            menuMenu.SetActive(false);
            backgroundMenu.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if (value == 1)
        {
            menuMenu.SetActive(true);
            fontsMenu.SetActive(false);
            backgroundMenu.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if (value == 2)
        {
            backgroundMenu.SetActive(true);
            menuMenu.SetActive(false);
            fontsMenu.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if (value == 3)
        {
            DesactiveSubMenuInterface();
            pauseMenu.isOn = false;
        }
    }

    /*public void OpenUserMenuDropdown(int value)
    {
        closeUserMenuButton.SetActive(true);
        //0 = selection, 1 = creation, 2 = fermer le menu
        if (value == 0)
        {
            selectUserMenu.SetActive(true);
            createUserMenu.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if (value == 1)
        {
            selectUserMenu.SetActive(false);
            createUserMenu.SetActive(true);
            pauseMenu.isOn = true;
        }
        else if (value == 2)
        {
            DesactiveSubMenuUser();
            pauseMenu.isOn = false;
        }
    }*/

    public void DesactiveSubMenuInterface()
    {
        DesactiveGameObject(fontsMenu);
        DesactiveGameObject(menuMenu);
        DesactiveGameObject(backgroundMenu);
        DesactiveGameObject(closeMenuButton);
        DesactiveGameObject(resetMenuButton);
    }

    public void DesactiveSubMenuVisualization()
    {
        DesactiveGameObject(cameraMenu);
        DesactiveGameObject(appearanceMenu);
        DesactiveGameObject(outlinesMenu);
        DesactiveGameObject(staticLightsMenu);
        DesactiveGameObject(dynamicLightsMenu);
    }

    /*public void DesactiveSubMenuUser()
    {
        DesactiveGameObject(selectUserMenu);
        DesactiveGameObject(createUserMenu);
        DesactiveGameObject(closeUserMenuButton);
    }*/

    public void ActiveOrDesactiveGameObject(GameObject gameObject)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void DesactiveGameObject(GameObject gameObject)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

}
