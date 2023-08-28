using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Regroupe les tab
/// </summary>
public class TabGroup : MonoBehaviour
{
    [Header("Onglets")]
    public List<TabView> tabs;
    public TabView defaultMenu;
    public TabView currentView;

    public void Start()
    {
        var currentTabs = FindObjectsOfType<TabView>();
        tabs = currentTabs.ToList();
        foreach (TabView t in currentTabs)
        {
            if(t != defaultMenu)
            {
                t.gameObject.SetActive(false);
            } else
            {
                defaultMenu.gameObject.SetActive(true);
            }
        }
        currentView = defaultMenu;
    }

    public void resetView()
    {
        if(currentView != null){
            currentView = null;
        }
    }

    public void changeTab(TabView tab)
    {
        if(currentView != null)
            currentView.gameObject.SetActive(false);
        resetView();
        
        currentView = tab;

        currentView.gameObject.SetActive(true);


    }
}
