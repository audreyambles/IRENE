using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class modifFonts : MonoBehaviour
{

    public TMP_Dropdown choix;

    //les polices
    public TMP_FontAsset Luciole;
    public TMP_FontAsset OpenDys;
    public TMP_FontAsset Tiresias;
    public TMP_FontAsset Arial;
    public TMP_FontAsset Liberation;

    public TMP_Text m_TextComponent;

    public TMP_Text label_tc;

    public TMP_Text labelTaillePolice;
    public TMP_Text labelTailleBordures;

    //public GameObject gameObjUI;
    public GameObject[] listeGameObjectUI;
    public GameObject[] listeGameObjectMenu;
    public GameObject[] dropdownObject;
    //public TMP_Text labelDD;

    public Color couleurGrisClair;
    public Color couleurGrisFonce;

    public float oldTaille;

    //private TextMeshProUGUI txtMP;

    public void Awake()
    {

        listeGameObjectUI = GameObject.FindGameObjectsWithTag("Texte");
        dropdownObject = GameObject.FindGameObjectsWithTag("Dropdown");
        foreach (GameObject ddO in dropdownObject)
        {
            Debug.Log("la liste des textes : " + ddO.GetComponent<TMP_Dropdown>().itemText);
            //labelDD = ddO.GetComponent<TMP_Dropdown>().itemText;
        }
        listeGameObjectMenu = GameObject.FindGameObjectsWithTag("Menu");
        //listeGameObjectUI = FindGameObjectsOfType<TMP_Text>;
    }

    /*public void Update()
    {
        listeGameObjectUI = GameObject.FindGameObjectsWithTag("Texte");
    }*/


    public void changementPolice(TMP_FontAsset newfont)
    {
        //m_TextComponent = GetComponent<TMP_Text>();
        foreach (GameObject goUI in listeGameObjectUI)
        {
            m_TextComponent = goUI.GetComponent<TMP_Text>();
            //Debug.Log("test pour tmp changement : " + m_TextComponent + " " + m_TextComponent);
            m_TextComponent.font = newfont;
        }
        foreach (GameObject goDD in dropdownObject)
        {
            label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
            label_tc.font = newfont;
        }

        // Use a different material preset which was derived from this font asset and created using the Create Material Preset Context Menu.
        //m_TextComponent.fontSharedMaterial = FontMaterialA;

    }

    public void changementPoliceNumber()
    {
        if (choix.value == 0)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                //Debug.Log("test pour tmp changement : " + m_TextComponent + " " + m_TextComponent);
                m_TextComponent.font = Luciole;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Luciole;
            }
        }
        else if (choix.value == 1)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                //Debug.Log("test pour tmp changement : " + m_TextComponent + " " + m_TextComponent);
                m_TextComponent.font = OpenDys;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = OpenDys;
            }
        }
        else if (choix.value == 2)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                //Debug.Log("test pour tmp changement : " + m_TextComponent + " " + m_TextComponent);
                m_TextComponent.font = Tiresias;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Tiresias;
            }
        }
        else if (choix.value == 3)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                //Debug.Log("test pour tmp changement : " + m_TextComponent + " " + m_TextComponent);
                m_TextComponent.font = Arial;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Arial;
            }
        }
        else if (choix.value == 4)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                //Debug.Log("test pour tmp changement : " + m_TextComponent + " " + m_TextComponent);
                m_TextComponent.font = Liberation;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Liberation;
            }
        }

    }

    public void modifTailleFont(float value)
    {
        foreach (GameObject goUI in listeGameObjectUI)
        {
            m_TextComponent = goUI.GetComponent<TMP_Text>();
            m_TextComponent.fontSize = m_TextComponent.fontSize - oldTaille + value;   
        }
        foreach (GameObject goDD in dropdownObject)
        {
            label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
            label_tc.fontSize = label_tc.fontSize - oldTaille + value;
        }

        oldTaille = value;
        string valeur = value.ToString();
        labelTaillePolice.text = valeur;
    }

    public void modifGras(bool value)
    {
        if (value == true)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.fontStyle = FontStyles.Bold;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.fontStyle = FontStyles.Bold;
            }
        }
        else
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.fontStyle ^= FontStyles.Bold;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.fontStyle ^= FontStyles.Bold;
            }
        }
    }

    public void changeColorMenu(int value)
    {
        if (value == 0)
        {
            foreach (GameObject goUI in listeGameObjectMenu)
            {
                goUI.GetComponent<Image>().color = Color.white;
            }
        }
        else if (value == 1)
        {
            foreach (GameObject goUI in listeGameObjectMenu)
            {
                goUI.GetComponent<Image>().color = couleurGrisClair;
            }
        }
        else if (value == 2)
        {
            foreach (GameObject goUI in listeGameObjectMenu)
            {
                goUI.GetComponent<Image>().color = couleurGrisFonce;
            }
        }
    }

    public void changeColorMenu1()
    {

            foreach (GameObject goUI in listeGameObjectMenu)
            {
                goUI.GetComponent<Image>().color = Color.white;
            }

    }

    public void changeColorMenu2()
    {

        foreach (GameObject goUI in listeGameObjectMenu)
        {
            goUI.GetComponent<Image>().color = couleurGrisClair;
        }

    }

    public void changeColorMenu3()
    {

        foreach (GameObject goUI in listeGameObjectMenu)
        {
            goUI.GetComponent<Image>().color = couleurGrisFonce;
        }

    }

    public void changeBorderMenu(float value)
    {
        foreach (GameObject goUI in listeGameObjectMenu)
        {
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectDistance = new Vector2(value, value);
            string valeur = value.ToString();
            labelTailleBordures.text = valeur;
        }
    }
}