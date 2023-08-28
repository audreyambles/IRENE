using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropdownPointer : MonoBehaviour, IPointerClickHandler
{
    public TMP_Dropdown dropDown;
    public Light lightCible;
    public bool depthCheck = false, normalCheck = false, ContourCheck = false;
    public bool LumieresPrincipales = true;
    public UnityEvent onClickEvent;
    private ShaderHelper s;

    public void Start()
    {
        s = FindObjectOfType<ShaderHelper>();
        Debug.Log("test");
    }

    public void Update()
    {
        /*
        if (gameObject.transform.Find("Item Checkmark") != null)
        {
            GameObject item = gameObject.transform.parent.Find("Item 0: Activer").gameObject;
            Image checkmark = item.transform.Find("Item Checkmark").GetComponent<Image>();
            if (lightCible != null)
            {
                if (LumieresPrincipales == true)
                {

                    if (lightCible.gameObject.activeInHierarchy == true)
                    {
                        checkmark.enabled = true;
                    }
                    else
                    {
                        checkmark.enabled = false;
                    }
                }
                else
                {
                    if (lightCible.enabled == true)
                    {
                        checkmark.enabled = true;
                    }
                    else
                    {
                        checkmark.enabled = false;
                    }
                }

            }
            //ca a ete code tres vite, mais on part du principe que s'il y a une aucune lumiere c'est les shaders qui sont check 
            else if (normalCheck == true | depthCheck == true)
            {
                if (s.getNormalsShader() == true && normalCheck == true)
                {
                    checkmark.enabled = true;

                }
                else if (s.getNormalsShader() != true && normalCheck == true)
                {
                    checkmark.enabled = false;
                }

                else if (s.getDepthShader() == true && depthCheck == true)
                {
                    checkmark.enabled = true;
                }
                else if (s.getDepthShader() != true && depthCheck == true)
                {
                    checkmark.enabled = false;
                }
            }
            else if (ContourCheck == true)
            {
                if(s.getCurrentMaterialShader() != s._DefaultMaterial.name)
                {
                    checkmark.enabled = true;
                } else
                {
                    checkmark.enabled = false;
                }
            }
            checkmark.gameObject.SetActive(true);
            }

        if(dropDown.options.Count > 1)
        {
            GameObject item1;
            if ((item1 = gameObject.transform.parent.Find("Item 1: Choisir couleur").gameObject) != null)
            {

                Image checkmark1 = item1.transform.Find("Item Checkmark").GetComponent<Image>();
                if (lightCible == ConfigScene.Instance.getLight().getLightTarget())
                {
                    checkmark1.enabled = true;

                }
                else
                {
                    checkmark1.enabled = false;

                }
            }

        }*/
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        onClickEvent.Invoke();
    }
}