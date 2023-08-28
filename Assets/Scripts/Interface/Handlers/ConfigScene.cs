using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// IDK
/// </summary>
public class ConfigScene : MonoBehaviour
{
    

    [Header("Le modèle 3D de la scène")]
    public GameObject gameObjectOfModel;
    public GameObject gameObjectActif;
    public GameObject[] listeFruitsLegumes;
    public int boucleModel;

    [SerializeField]
    public List<MeshRenderer> mesh;
    [Header("Lumière de la scène")]
    public Light scenelight;
    // volume en lien avec le traitement post process (shader) ici utile pour que les couleurs de la scene soient agréables
    [Header("Volume ? de la scène pour post-process")]
    public Volume volume;

    [Header("Menus de la scène")]
    public TabView[] allTabs;
    public TabView menuUtilisateur;
    public TabView menuAdmin;
    //Correspond au menu parametres annexes de l'interface administrateur
    public GameObject menuParam;
    public TabGroup menuHandler;
    private bool InterfaceUtilisateurOn = true;
    //Events pour initialiser certaines variables de gameobject
    public UnityEvent OnStart;
    private CinemachineHelper cinehelp;
    private LightHandler lighthandler;

    private static ConfigScene _instance;

    public static ConfigScene Instance { get { return _instance; } }


    private void Awake()
    {
        gameObjectOfModel = GameObject.FindWithTag("Modele3D");
        listeFruitsLegumes = GameObject.FindGameObjectsWithTag("Objet");
        scenelight = FindObjectOfType<Light>();
        lighthandler = FindObjectOfType<LightHandler>();
        gameObjectActif = listeFruitsLegumes[0];
        for(int i=1; i<listeFruitsLegumes.Length; i++)
        {
            listeFruitsLegumes[i].SetActive(false);
        }
        boucleModel = 0;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        mesh = gameObjectOfModel.transform.GetComponentsInChildren<MeshRenderer>(true).ToList();
        allTabs = FindObjectsOfType<TabView>();
        volume = FindObjectOfType<Volume>();
        OnStart.Invoke();
    }

    private void Update()
    {
      //CONTROLLER DE BOUTONS
        /*if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            SwitchInterface();
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //lighthandler.StopDollyLightModel(2);

            SwitchModele();
        }

        /*if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }*/
    }

    public LightHandler getLight()
    {
        return lighthandler;
    }

    //AFFICHAGE INTERFACES
    /*private void ShowParametres()
    {
        menuParam.SetActive(!menuParam.activeInHierarchy);
    }
    private void SwitchInterface()
    {
        if(InterfaceUtilisateurOn)
        {
            menuHandler.changeTab(menuAdmin);
            ShowParametres();
        } else
        {
            menuHandler.changeTab(menuUtilisateur);
            ShowParametres();
        }
        InterfaceUtilisateurOn = !InterfaceUtilisateurOn;
    }*/

    public void SwitchModele()
    {
        boucleModel = boucleModel + 1;
        listeFruitsLegumes[boucleModel - 1].SetActive(false);
        if (boucleModel == listeFruitsLegumes.Length)
        {
            boucleModel = 0;
        }
        listeFruitsLegumes[boucleModel].SetActive(true);
        gameObjectActif = listeFruitsLegumes[boucleModel];
    }
}
