using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
public class TabGroups : MonoBehaviour
{
    public List<TabButtons> tabButtons;
    public List<GameObject> tabs;

    Color idleColor = new Color(1,1,1);
    Color hoverColor = new Color(0.8f,0.8f,0.8f);
    Color selectedColor = new Color(0.4f, 0.4f, 0.4f);

    public TabButtons selectedTab;

    void Start()
    {
        tabs = new List<GameObject>();
        Invoke("SuperLateStart", 7);
    }
    void SuperLateStart()
    {
        foreach(GameObject tab in tabs)
        {
            tab.SetActive(false);
        }  
    }
    public void Subscribe(TabButtons tabButton)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButtons>();
        }

        tabButtons.Add(tabButton);
    }

    public void PopulateTabList(GameObject tab)
    {
        if(tabs == null)
        {
            tabs = new List<GameObject> ();
        }
        tabs.Add(tab);
        tab.gameObject.SetActive(true);
        //had to do this Bs for reasons i 100% know! trust.
    }

    public void OnTabEnter(TabButtons tabButton)
    {
        if (selectedTab == null || tabButton != selectedTab)
        {
            ResetTabs();
            //tabButton.image.color = hoverColor;
        }

       
    }

    public void OnTabExit(TabButtons tabButton)
    {
        ResetTabs();

    }

    public void OnTabSelected(TabButtons tabButton)
    {
        selectedTab = tabButton;
        ResetTabs();
        //tabButton.image.color = selectedColor;

        int index = tabButton.transform.GetSiblingIndex();
        for(int i = 0; i < tabs.Count; i++)
        {
            if(i == index)
            {
                tabs[i].SetActive(true);
            }
            else
            {
                tabs[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButtons tabButton in tabButtons)
        {
            if(selectedTab != null && tabButton == selectedTab)
            {
                continue;
            }
           // tabButton.image.color = idleColor;

        }
        
    }
}





    