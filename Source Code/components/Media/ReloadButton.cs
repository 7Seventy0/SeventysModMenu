using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class ReloadButton : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.5f);
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        MusicPlayer.instance.Reload();

    }
}

