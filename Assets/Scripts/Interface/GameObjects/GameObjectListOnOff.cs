using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Contr�le l'activation des GameObjects d�finit dans la liste.  ??????????
/// </summary>
public class GameObjectListOnOff : MonoBehaviour
{

    [Header("GameObjects :")]
    public List<GameObject> ToggleGameobjects;
    [Tooltip("Si on souhaite prendre automatiquement le mod�le de l'ange en cible")]
    public bool prendreModeleAnge = false;
    [Header("Etat des GameObjects au d�marrage :")]
    public bool DefaultOnOff;
    [Header("Debug use")]
    public bool toggleBool;
    // Start is called before the first frame update
    void Start()
    {
        if(prendreModeleAnge == true)
        {
            foreach(MeshRenderer m in ConfigScene.Instance.mesh)
            {
                ToggleGameobjects.Add(m.gameObject);
            }
        }

        SetAllActive(DefaultOnOff);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetActiveOnOffSingle(GameObject a)
    {
        if (ToggleGameobjects.Contains(a))
        {
            SetAllActive(false);
            ToggleGameobjects[ToggleGameobjects.IndexOf(a)].SetActive(true);
        }
    }
    public void SetActiveOnOffCumulative(GameObject a)
    {
        if (ToggleGameobjects.Contains(a))
        {
            if(a.activeInHierarchy == false)
            {

                ToggleGameobjects[ToggleGameobjects.IndexOf(a)].SetActive(true);
            } else
            {
                ToggleGameobjects[ToggleGameobjects.IndexOf(a)].SetActive(false);
                //A besoin d'un canal alpha pour fonctionner, ce qui n'est pas le cas si la surface du mat�riel est opaque.
                //ToggleGameobjects[ToggleGameobjects.IndexOf(a)].GetComponent<FadeInOut>().StartCoroutine(ToggleGameobjects[ToggleGameobjects.IndexOf(a)].GetComponent<FadeInOut>().FadeOutMaterial(0.1f));
            }
        }
    }
    public void SetAllActive(bool ab)
    {
        foreach (GameObject a in ToggleGameobjects)
        {
            a.SetActive(ab);
        }
    }
}
