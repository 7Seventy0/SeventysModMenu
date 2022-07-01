using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SelectHoldable : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 18;
    }
    public bool righthand;
    float nextuse;
    float cooldown = 1.3f;
    void OnTriggerEnter(Collider collider)
    {
        if(Time.time > nextuse)
        {
       HoldableManager.instance.SelectHoldable(righthand);
        gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.5f);
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
            nextuse = Time.time+cooldown;
        }
    }

}

