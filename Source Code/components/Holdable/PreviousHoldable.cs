using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PreviousHoldable : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
       HoldableManager.instance.PreviousHoldable();
        gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.5f);
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
    }

}

