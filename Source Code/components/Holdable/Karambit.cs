using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

public class Karambit : MonoBehaviour
{

    public List<Texture> textureList;
    public List<Material> materialList;
    
    string filepath;
    void Start()
    {
       
        textureList = new List<Texture>();
        materialList = new List<Material>();
        filepath = BepInEx.Paths.BepInExRootPath + "/SeventysKarambitSkins/";
        var folder = Directory.CreateDirectory(BepInEx.Paths.BepInExRootPath + "/SeventysKarambitSkins");
        Debug.Log("Made a new Karambit Skins Folder");
        LoadSkins();

       
    }

    float nextSwitch;
    float cooldown = 0.3f;
    bool leftstick;
    private readonly XRNode lNode = XRNode.LeftHand;
    Renderer renderer;
    int index;

    public void LoadSkins()
    {
        
        DirectoryInfo d = new DirectoryInfo(filepath);
        FileInfo[] Files = d.GetFiles("*.png");
        foreach (FileInfo file in Files)
        {
            StartCoroutine(LoadMusic(file.Name));

        }
    }
    IEnumerator LoadMusic(string skinname)
    {
        WWW request = GetAudioFromFile(filepath, skinname);
        yield return request;
        Texture skin = request.texture;
        skin.name = skinname;
        textureList.Add(skin);

        Material skinMaterial = new Material(Shader.Find("Standard"));
        skinMaterial.mainTexture = skin;
        skinMaterial.name = skinname;
        materialList.Add(skinMaterial);
    }
    WWW GetAudioFromFile(string path, string fileName)
    {
        string imageToLoad = path + fileName;
        WWW request = new WWW(imageToLoad);
        return request;
    }
    void Update()
    {
        if (renderer == null)
        {
             renderer = GetComponentInChildren<Renderer>();
        }
        InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftstick);
        if (leftstick)
        {
            if (Time.time > nextSwitch)
            {
                index++;
                if(index > materialList.Count)
                {
                    index = 0;
                }
                ChangeMaterial();
                nextSwitch = Time.time + cooldown;
            }
        }
    }
    public void ChangeMaterial()
    {
        
        renderer.material = materialList[index];
        Debug.Log("Changed " + gameObject.name + "'s skin to " + renderer.material.name);
        
    }
}

