using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

public class Bayonet : MonoBehaviour
{
    List<Texture> textureList;
    List<Material> materialList;
    string filepath;
    void Start()
    {

        textureList = new List<Texture>();
        materialList = new List<Material> ();
        var folder = Directory.CreateDirectory(BepInEx.Paths.BepInExRootPath + "/SeventysBayonetSkins");
        filepath = BepInEx.Paths.BepInExRootPath + "/SeventysBayonetSkins/";
        Debug.Log("Made a new Bayonet Skins Skins Folder");
        LoadSkins();

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material mat = renderer.material;
            materialList.Add(mat);
        }
    }

    float nextSwitch;
    float cooldown = 0.3f;
    bool leftstick;
    private readonly XRNode lNode = XRNode.LeftHand;
    Renderer renderer;
    int index;
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
                if (index > textureList.Count)
                {
                    index = 0;
                }
                ChangeSkin();
                nextSwitch = Time.time + cooldown;
            }
        }
    }

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

    }
    WWW GetAudioFromFile(string path, string fileName)
    {
        string imageToLoad = path + fileName;
        WWW request = new WWW(imageToLoad);
        return request;
    }
    Material material;
    public void ChangeSkin()
    {
        foreach(Material mat in materialList){
            mat.mainTexture = textureList[index];

        }
        Debug.Log("Changed " + gameObject.name + "'s skin to " + renderer.material.mainTexture.name);

    }
}

