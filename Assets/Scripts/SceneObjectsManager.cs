using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SceneObjectsManager : MonoBehaviour
{
    [Header("Current 3D model in scene")]
    public GameObject gameObjectOfModel;
    public GameObject activeObject;
    public GameObject activeObjectPivot;
    public int idObject;

    [Header("All 3D model in scene")]
    public GameObject[] objectsSceneList;
    public GameObject[] pivotObjectsSceneList;
    //[SerializeField]
    public List<MeshRenderer> meshsOfObjectsScene;

    [Header("DropDown Manager")]
    List<string> namesObjectsDropDown;
    public TMP_Dropdown objectsDropDown;

    [Header("Space button function")]
    /*revoir si ça reste là ou dans un autre fichier */
    public bool switchModeles = false;

    /*revoir si ça reste là ou dans un autre fichier */
    //scene data
    private Volume volume;
    private VisualizationManager visualizationManager;
    private MenuController menuController;
    /* ********* */


        /*revoir si utilisation du singleton*/

    private static SceneObjectsManager _instance;

    public static SceneObjectsManager Instance { get { return _instance; } }


    private void Awake()
    {
        objectsDropDown = GameObject.Find("DropdownObjets").GetComponent<TMP_Dropdown>();
        gameObjectOfModel = GameObject.FindWithTag("Modele3D");
        objectsSceneList = GameObject.FindGameObjectsWithTag("Objet");

        objectsSceneList = objectsSceneList.OrderBy(go => go.name).ToArray();

        /* **** DROPDOWN OBJETS  **** */
        namesObjectsDropDown = new List<string>();
        foreach (GameObject objectInScene in objectsSceneList)
        {
            namesObjectsDropDown.Add(objectInScene.name);
        }


        if (objectsDropDown != null)
        {
            //la liste des noms ne doit pas être vide
            if (namesObjectsDropDown.Count > 0)
            {
                // add les noms des GameObjects au dropdown
                objectsDropDown.AddOptions(namesObjectsDropDown);
            }
            else
            {
                Debug.LogWarning("Aucun GameObject trouvé dans la scène.");
            }
        }
        else
        {
            Debug.LogError("Veuillez assigner un TextMeshProDropdown dans l'inspecteur Unity.");
        }

        // ************************************

        pivotObjectsSceneList = GameObject.FindGameObjectsWithTag("Pivot");

        pivotObjectsSceneList = pivotObjectsSceneList.OrderBy(go => go.name).ToArray();

        activeObject = objectsSceneList[0];
        activeObjectPivot = pivotObjectsSceneList[0];
        for (int i = 1; i < objectsSceneList.Length; i++)
        {
            objectsSceneList[i].SetActive(false);
        }
        idObject = 0;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        meshsOfObjectsScene = gameObjectOfModel.transform.GetComponentsInChildren<MeshRenderer>(true).ToList();
        volume = FindObjectOfType<Volume>();
        visualizationManager = FindObjectOfType<VisualizationManager>();
        menuController = FindObjectOfType<MenuController>();
    }

    private void Update()
    {
        //peut etre le gérer dans un handler / ou controller ?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchModele();
        }
    }

    public void SwitchModele()
    {
        idObject = idObject + 1;
        objectsSceneList[idObject - 1].SetActive(false);
        if (idObject == objectsSceneList.Length)
        {
            idObject = 0;
        }
        objectsSceneList[idObject].SetActive(true);
        activeObject = objectsSceneList[idObject];
        activeObjectPivot = pivotObjectsSceneList[idObject];

        visualizationManager.ResetAllVisualTreatments();
        menuController.ResetItemsVisualizationMenu();
    }
}
