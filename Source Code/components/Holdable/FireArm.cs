using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;

public class FireArm : MonoBehaviour
{
     public float timeBetweenShots;
     public float bulletSpeed;
    public bool fullAuto;
    public bool bulletGravity = true;
    public GameObject bullet;
    Transform bulletSpawnPoint;
    Holdable holdable;
    public void Done()
    {

    }


    float nextFire;
    private readonly XRNode lNode = XRNode.LeftHand;
    private readonly XRNode rNode = XRNode.RightHand;
    bool rightTrigger;
    bool leftTrigger;
    bool canFire;
    void Update()
    {
        if(holdable == null)
        {
            holdable = GetComponent<Holdable>();
        }
        if(Time.time > nextFire && holdable.isHeld)
        {
           
            InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(CommonUsages.triggerButton, out leftTrigger);
            InputDevices.GetDeviceAtXRNode(rNode).TryGetFeatureValue(CommonUsages.triggerButton, out rightTrigger);
            if (fullAuto)
            {
                
                if (holdable.isInRightHand && rightTrigger)
                {
                    Fire();
                    nextFire = Time.time + timeBetweenShots;
                }
                else if(!holdable.isInRightHand && leftTrigger)
                {
                    Fire();
                    nextFire = Time.time + timeBetweenShots;
                }
            }
            else
            {
                if (holdable.isInRightHand && rightTrigger && canFire)
                {
                    Fire();
                    nextFire = Time.time + timeBetweenShots;
                    canFire = false;
                }
                if (!holdable.isInRightHand && leftTrigger && canFire)
                {
                    Fire();
                    nextFire = Time.time + timeBetweenShots;
                    canFire = false;
                }
            }
            if (holdable.isInRightHand && !rightTrigger)
            {
                canFire = true;
            }
            else if(!holdable.isInRightHand && !leftTrigger)
            {
                canFire = true;
            }
        }
    }

    void Fire()
    {
        Rigidbody rb;
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = gameObject.GetComponentInChildren<Tilemap>().transform;
        }
        GameObject bulletInstance = Instantiate(bullet,bulletSpawnPoint.position,bulletSpawnPoint.rotation) ;
        if(bulletInstance.GetComponent<Rigidbody>() != null)
        {
        rb = bulletInstance.GetComponent<Rigidbody>() ;

        }
        else
        {
            rb = bulletInstance.AddComponent<Rigidbody>();
        }
        bulletInstance.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.4f);
        bulletInstance.GetComponent<AudioSource>().PlayOneShot(bulletInstance.GetComponent<AudioSource>().clip);
        rb.useGravity = bulletGravity;
        rb.AddRelativeForce(bullet.transform.forward * bulletSpeed ,ForceMode.Impulse);
        
    }


}


