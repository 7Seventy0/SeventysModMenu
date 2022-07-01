using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public     class RWButton : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerStay(Collider collider)
    {
        MusicPlayer.instance.RW();
        
    }

    void OnTriggerEnter(Collider collider)
    {
        MusicPlayer.instance.isMessingWithPitch = true;
    }
    void OnTriggerExit(Collider collider)
    {
        MusicPlayer.instance.isMessingWithPitch = false;
    }

}

