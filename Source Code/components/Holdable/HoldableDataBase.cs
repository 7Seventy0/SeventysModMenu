using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class HoldableDataBase : MonoBehaviour
{
    public static HoldableDataBase instance;
    public List<GameObject> displayedHoldables = new List<GameObject>();
    public List<Holdable> holdableList = new List<Holdable>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int HoldableCount()
    {

        return instance.displayedHoldables.Count;

    }

    public void AddToDatabase(GameObject holdableObjct, Holdable holdable)
    {
        holdableList.Add(holdable);
        displayedHoldables.Add(holdableObjct);
    }


    void Start()
    {

    }

    public GameObject GetHoldable(int index)
    {
        return displayedHoldables[index];
    }

}


