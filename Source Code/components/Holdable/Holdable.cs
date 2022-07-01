using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    public int ID;
    public string holdableName;
    public string holdableDescription;
    public string holdableAuthor;
    public Vector3 displayLocalPos;
    public Vector3 displayLocalEuler;
    public Vector3 displayscale;
    public Vector3 inhandLocalPosRight;
    public Vector3 inhandLocalPosLeft;
    public Vector3 inhandLocalEulerRight;
    public Vector3 inhandLocalEulerLeft;
    public Vector3 inhandscale;
    public bool isHeld;
    public bool isInRightHand;
    
    public void Done()
    {
        transform.localPosition = displayLocalPos;
        transform.localEulerAngles = displayLocalEuler;
        transform.localScale = displayscale;
    }
    public void ApplyCosmetic(Transform hand)
    {
        transform.SetParent(hand.transform, false);
        transform.localEulerAngles = inhandLocalEulerRight;
        transform.localPosition = inhandLocalPosRight;
        transform.localScale = inhandscale;
        if(hand.name == "palm.01.R")
        {
            isInRightHand = true;
            transform.localEulerAngles = inhandLocalEulerRight;
            transform.localPosition = inhandLocalPosRight;
        }
        else if(hand.name == "palm.01.L")
        {
            isInRightHand= false;
            transform.localEulerAngles = inhandLocalEulerLeft;
            transform.localPosition = inhandLocalPosLeft;
        }
        isHeld = true;
        
    }
}

