using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TabButtons : MonoBehaviour
{
    public TabGroups tabGroups;

    public void OnTriggerEnter(Collider collider)
    {
        tabGroups.OnTabSelected(this);
    }

    void Start()
    {
        gameObject.layer = 18;
        tabGroups = FindObjectOfType<TabGroups>();
       tabGroups.Subscribe(this);
        
    }

}



    