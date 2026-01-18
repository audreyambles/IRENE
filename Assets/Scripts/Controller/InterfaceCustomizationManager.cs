using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceCustomizationManager : MonoBehaviour
{
    //TODO = review slider hover buttons color not good
    //TODO = also need to restrict text color modification functions, etc.

    [Header("Fonts type")]
    public TMP_FontAsset Luciole;
    public TMP_FontAsset OpenDys;
    public TMP_FontAsset Tiresias;
    public TMP_FontAsset Arial;
    public TMP_FontAsset Liberation;

    [Header("Menu variables")]
    public GameObject moveMenu;

    #region private variables
    //Lists variables
    private GameObject[] uiElementsList;
    private GameObject[] menusList;
    private GameObject[] buttonsList;
    private GameObject[] dropdownsList;
    private GameObject[] slidersList;
    //Texts variables
    private TMP_Text m_TextComponent;
    private TMP_Text fontsSizeLabel;
    private TMP_Text bordersMenuSizeLabel;
    private TMP_Text bordersButtonSizeLabel;
    //Dropdown variables
    public TMP_Dropdown fontsTypeDropDown;
    //Color variables
    private Color lightGray;
    private Color darkGray;
    //Other variables
    private MenuController menuController;
    private float oldSize;
    public bool boldCheck = false;
    public int fontSizeUserVar;
    public int colorMenuUserVar;
    public int menuPositionUserVar;
    public int borderMenuUserVar;
    public int borderButtonUserVar;
    public float backgroundColorUserVar;
    #endregion


    public void Awake()
    {
        fontsTypeDropDown = GameObject.Find("DropdownFontChoice").GetComponent<TMP_Dropdown>();
        fontsSizeLabel = GameObject.Find("LabelFontSizeValue").GetComponent<TMP_Text>();
        bordersMenuSizeLabel = GameObject.Find("LabelBordersMenuSizeValue").GetComponent<TMP_Text>();
        bordersButtonSizeLabel = GameObject.Find("LabelBordersButtonSizeValue").GetComponent<TMP_Text>();

        uiElementsList = GameObject.FindGameObjectsWithTag("Texte");
        dropdownsList = GameObject.FindGameObjectsWithTag("Dropdown");
        menusList = GameObject.FindGameObjectsWithTag("Menu");
        buttonsList = GameObject.FindGameObjectsWithTag("Bouton");
        slidersList = GameObject.FindGameObjectsWithTag("Slider");

        lightGray = new Color(0.8117647f, 0.8117647f, 0.8117647f, 1);
        darkGray = new Color(0.3686275f, 0.3686275f, 0.3686275f, 1);

        menuController = FindObjectOfType<MenuController>();
        moveMenu = GameObject.Find("MenuVisualization");
    }

    public void ResetInterface()
    {
        //fonts
        fontsTypeDropDown.value = 0;
        SetFontType();
        SetFontSize(1);
        if (boldCheck == true)
        {
            SetFontStyle(false);
        }
        SetColorMenuWhite();

        //menu
        MoveRightMenu();

        menuController.fontsMenu.SetActive(true);
        GameObject.Find("SliderFontSize").GetComponent<Slider>().value = 1;
        GameObject.Find("ToggleFontBold").GetComponent<Toggle>().isOn = false;
        menuController.fontsMenu.SetActive(false);

        menuController.menuMenu.SetActive(true);
        GameObject.Find("SliderBordersMenuSize").GetComponent<Slider>().value = 1;
        GameObject.Find("SliderBordersButtonSize").GetComponent<Slider>().value = 1;
        menuController.menuMenu.SetActive(false);

        menuController.backgroundMenu.SetActive(true);
        GameObject.Find("SliderBackgroundColor").GetComponent<Slider>().value = 0;
        menuController.backgroundMenu.SetActive(false);

        menuController.OpenInterfaceMenuDropdown(menuController.interfaceMenuDropDown.value);
        SetBordersMenu(1);
        SetBordersButtons(1);

        SetSceneBackground(0);
    }

    #region FONTS

    public void SetFontType()
    {
        if (fontsTypeDropDown.value == 0)
        {
            SetTexts(uiElementsList, dropdownsList, Luciole);
        }
        else if (fontsTypeDropDown.value == 1)
        {
            SetTexts(uiElementsList, dropdownsList, OpenDys);
        }
        else if (fontsTypeDropDown.value == 2)
        {
            SetTexts(uiElementsList, dropdownsList, Tiresias);
        }
        else if (fontsTypeDropDown.value == 3)
        {
            SetTexts(uiElementsList, dropdownsList, Arial);
        }
        else if (fontsTypeDropDown.value == 4)
        {
            SetTexts(uiElementsList, dropdownsList, Liberation);
        }
    }

    public void SetFontSize(float value)
    {
        SetTexts(uiElementsList, dropdownsList, value);
        oldSize = value;
        fontSizeUserVar = (int)value;
        string valueToString = value.ToString();
        fontsSizeLabel.text = valueToString;
    }

    public void SetFontStyle(bool value)
    {
        if (value == true)
        {
            SetTexts(uiElementsList, dropdownsList, FontStyles.Bold);
            boldCheck = true;
        }
        else
        {
            SetTexts(uiElementsList, dropdownsList, FontStyles.Normal);
            boldCheck = false;
        }
    }

    private void SetTexts(GameObject[] textsLabelList, GameObject[] textsDropdownList, TMP_FontAsset fontType)
    {
        foreach (GameObject elements in textsLabelList)
        {
            m_TextComponent = elements.GetComponent<TMP_Text>();
            m_TextComponent.font = fontType;
        }
        foreach (GameObject elements in textsDropdownList)
        {
            m_TextComponent = elements.GetComponent<TMP_Dropdown>().itemText;
            m_TextComponent.font = fontType;
        }
    }

    private void SetTexts(GameObject[] textsLabelList, GameObject[] textsDropdownList, float value)
    {
        foreach (GameObject elements in textsLabelList)
        {
            m_TextComponent = elements.GetComponent<TMP_Text>();
            m_TextComponent.fontSize = m_TextComponent.fontSize - oldSize + value;
        }
        foreach (GameObject elements in textsDropdownList)
        {
            m_TextComponent = elements.GetComponent<TMP_Dropdown>().itemText;
            m_TextComponent.fontSize = m_TextComponent.fontSize - oldSize + value;
        }
    }

    private void SetTexts(GameObject[] textsLabelList, GameObject[] textsDropdownList, FontStyles fontStyle)
    {
        foreach (GameObject elements in textsLabelList)
        {
            m_TextComponent = elements.GetComponent<TMP_Text>();
            m_TextComponent.fontStyle = fontStyle;
        }
        foreach (GameObject elements in textsDropdownList)
        {
            m_TextComponent = elements.GetComponent<TMP_Dropdown>().itemText;
            m_TextComponent.fontStyle = fontStyle;
        }
    }

    #endregion

    #region MENU

    public void SetColorMenuWhite()
    {
        SetColorMenu(Color.white, Color.white, Color.black, Color.black, Color.white, Color.black);
        colorMenuUserVar = 1;
    }

    public void SetColorMenuLightGray()
    {
        SetColorMenu(lightGray, Color.white, Color.black, Color.black, Color.white, Color.black);
        colorMenuUserVar = 2;
    }

    public void SetColorMenuDarkGray()
    {
        SetColorMenu(darkGray, Color.black, Color.white, Color.white, Color.white, Color.black);
        colorMenuUserVar = 3;
    }

    public void SetColorMenuBlack()
    {
        SetColorMenu(Color.black, Color.black, Color.white, Color.white, Color.white, darkGray);
        colorMenuUserVar = 4;
    }

    private void SetColorMenu(Color backgroundMenuColor, Color backgroundItemsColor, Color textColor, Color bordersColor, Color sliderNormalsColor, Color sliderSelectedColor)
    {
        foreach (GameObject element in menusList)
        {
            element.GetComponent<Image>().color = backgroundMenuColor;
            Outline outline = element.GetComponent<Outline>();
            outline.effectColor = bordersColor;
        }

        foreach (GameObject element in uiElementsList)
        {
            element.GetComponent<TMP_Text>().color = textColor;
        }

        foreach (GameObject element in buttonsList)
        {
            element.GetComponent<Image>().color = backgroundItemsColor;
            Outline outline = element.GetComponent<Outline>();
            outline.effectColor = bordersColor;
        }

        foreach (GameObject element in dropdownsList)
        {
            element.GetComponent<Image>().color = backgroundItemsColor;
            Outline outline = element.GetComponent<Outline>();
            outline.effectColor = bordersColor;
        }

        foreach (GameObject element in slidersList)
        {
            Slider slider = element.GetComponent<Slider>();
            ColorBlock colorBlock = slider.colors;
            colorBlock.normalColor = sliderNormalsColor;
            colorBlock.highlightedColor = sliderSelectedColor;
            colorBlock.pressedColor = sliderSelectedColor;
            colorBlock.selectedColor = sliderSelectedColor;
            slider.colors = colorBlock;
        }
    }

    public void SetBordersMenu(float value)
    {
        SetBorders(menusList, value, bordersMenuSizeLabel);
        borderMenuUserVar = (int)value;
    }

    public void SetBordersButtons(float value)
    {
        SetBorders(buttonsList, value, bordersButtonSizeLabel);
        SetBorders(dropdownsList, value, bordersButtonSizeLabel);
        borderButtonUserVar = (int)value;
    }

    private void SetBorders(GameObject[] gameObjectList, float value, TMP_Text textLabel)
    {
        foreach (GameObject element in gameObjectList)
        {
            Outline outline = element.GetComponent<Outline>();
            outline.effectDistance = new Vector2(value, value);
            string valueToString = value.ToString();
            textLabel.text = valueToString;
        }
    }

    public void MoveLeftMenu()
    {
        MoveMenu(new Vector2(-1541, 540));
        menuPositionUserVar = 1;
    }

    public void MoveRightMenu()
    {
        MoveMenu(new Vector2(1, 540));
        menuPositionUserVar = 2;
    }

    private void MoveMenu(Vector2 positionXY)
    {
        var newMenu = moveMenu.GetComponent<RectTransform>();
        newMenu.anchoredPosition = positionXY;
    }
    #endregion

    #region BACKGROUND

    public void SetSceneBackground(float val)
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Camera cam = mainCamera.GetComponent<Camera>();
        cam.backgroundColor = Color.Lerp(Color.white, Color.black, val);
        backgroundColorUserVar = val;
    }

    #endregion
}
