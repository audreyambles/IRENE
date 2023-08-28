using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menuScript : MonoBehaviour
{
    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject menuPolice;
    public GameObject menuMenu;
    public GameObject menuTheme;

    // penser à charger les menus avant l'ouverture de l'appli sinon les modifs ne sont pas prises en compte (component disabled)


    void Start()
    {
        if(menuPolice.activeSelf)
        {
            menuPolice.SetActive(false);
        }
        if(menuMenu.activeSelf)
        {
            menuMenu.SetActive(false);
        }
        if(menuTheme.activeSelf)
        {
            menuTheme.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            if(Menu1.activeSelf)
            {
                Menu1.SetActive(false);
            }
            else
            {
                Menu1.SetActive(true);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Menu2.activeSelf)
            {
                Menu2.SetActive(false);
                //pauseMenu.isOn = false;
            }
            else
            {
                Menu2.SetActive(true);
                //pauseMenu.isOn = true;
            }
            
        }
    }

    public void deplaceMenu1Gauche()
    {
        var newMenu = Menu1.GetComponent<RectTransform>();
        newMenu.anchoredPosition = new Vector2(-1541, 0);
    }

    public void deplaceMenu1Droite()
    {
        var newMenu = Menu1.GetComponent<RectTransform>();
        newMenu.anchoredPosition = new Vector2(0.001220703f, 0);
    }

    // quand on avait un bouton
    /*public void closeMenuParam()
    {
        Menu2.SetActive(false);
        pauseMenu.isOn = false;
    }

    public void OpenMenuParam()
    {
        if(!Menu2.activeSelf)
        {
            Menu2.SetActive(true);
            pauseMenu.isOn = true;
        }
    }*/

    //actuellement on a le menu sous forme de dropdown
    public void openMenuDropdown(int value)
    {
        //0 = polices, 1 = menu, 2 = fond et 3 = fermer le menu
        if (value == 0)
        {
            menuPolice.SetActive(true);
            menuMenu.SetActive(false);
            menuTheme.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if(value == 1)
        {
            menuMenu.SetActive(true);
            //à faire, activer le bouton du menu mais ne fonctionne pas pour le moment
            /*GameObject gmb = GameObject.FindGameObjectWithTag("BoutonActif");
            gmb.SetActive(true);
            gmb.GetComponent<Button>().Select();*/
            menuPolice.SetActive(false);
            menuTheme.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if(value == 2)
        {
            menuTheme.SetActive(true);
            menuMenu.SetActive(false);
            menuPolice.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if(value == 3)
        {
            menuTheme.SetActive(false);
            menuMenu.SetActive(false);
            menuPolice.SetActive(false);
            pauseMenu.isOn = false;
        }
    }
}
