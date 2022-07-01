using BepInEx;
using System;
using UnityEngine;
using Utilla;
using WindowsInput;
using UnityEngine.InputSystem;
using System.Collections;
using System.Reflection;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.XR;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Linq;
namespace SeventysModMenu
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        
        bool inRoom;
        GameObject modmenuInstance;
        TabGroups tabGroups;
        GameObject anchor;
        GameObject bulletWithTrail;
        List<GameObject> bullets;
        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            StartCoroutine(SeventysStart());
            bullets = new List<GameObject>();
        }
        IEnumerator SeventysStart()
        {
            var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SeventysModMenu.Assets.modmenu");
            var bundleLoadRequest = AssetBundle.LoadFromStreamAsync(fileStream);
            yield return bundleLoadRequest;

            var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }

            var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("SeventysModMenu");
            yield return assetLoadRequest;
            yield return new WaitForSeconds(0.2f);
            GameObject modmenu = assetLoadRequest.asset as GameObject;
            modmenuInstance = Instantiate(modmenu);
            tabGroups = modmenuInstance.AddComponent<TabGroups>();
            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("GorillaSmg001");
            yield return assetLoadRequest;

            GameObject smg = assetLoadRequest.asset as GameObject;

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("GorillaKarambit");
            yield return assetLoadRequest;

            GameObject karambit = assetLoadRequest.asset as GameObject;

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("GorillaKatana12234");
            yield return assetLoadRequest;

            GameObject katana = assetLoadRequest.asset as GameObject;

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("SombraSMG");
            yield return assetLoadRequest;

            GameObject sombra = assetLoadRequest.asset as GameObject;
            yield return new WaitForSeconds(0.2f);
            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("GorillaNerf");
            yield return assetLoadRequest;

            GameObject nerfgun = assetLoadRequest.asset as GameObject;

            yield return new WaitForSeconds(0.2f);

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("BulletWithTrail");
            yield return assetLoadRequest;

            bulletWithTrail = assetLoadRequest.asset as GameObject;
            bullets.Add(bulletWithTrail);

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("NerfDart");
            yield return assetLoadRequest;

            GameObject nerfdart = assetLoadRequest.asset as GameObject;

            bullets.Add(nerfdart);

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("LaserBullet1");
            yield return assetLoadRequest;
             
            GameObject laser1 = assetLoadRequest.asset as GameObject;

            bullets.Add(laser1);


            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("GamerCube");
            yield return assetLoadRequest;

            GameObject gamercube = assetLoadRequest.asset as GameObject;
            Instantiate(gamercube);

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("GorillaBayonet");
            yield return assetLoadRequest;

            GameObject bayonet = assetLoadRequest.asset as GameObject;






  

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<Material>("ghostBit");
            yield return assetLoadRequest;
            Material ghostbit = assetLoadRequest.asset as Material;


            yield return new WaitForSeconds(1f);

            GameObject.Find("Player").AddComponent<HoldableDataBase>();
            GameObject.Find("Player").AddComponent<HoldableManager>();
            yield return new WaitForSeconds(0.2f);
            GameObject.Find("PreviousMediaButton").AddComponent<PrevMedia>();
            GameObject.Find("NextMediaButton").AddComponent<NextMedia>();
            GameObject.Find("pauseButton").AddComponent<PauseMedia>();

            yield return new WaitForSeconds(0.2f);
            GameObject.Find("ModMenuButton1").AddComponent<TabButtons>();
            GameObject.Find("ModMenuButton2").AddComponent<TabButtons>();
            GameObject.Find("ModMenuButton3").AddComponent<TabButtons>();

            GameObject.Find("VolumeUp").AddComponent<VolumeUp>();
            GameObject.Find("VolumeDown").AddComponent<VolumeDown>();
            GameObject.Find("ReloadButton").AddComponent<ReloadButton>();

            GameObject.Find("Skip5s").AddComponent<FFButton>();
            GameObject.Find("Rewind5s").AddComponent<RWButton>();

            yield return new WaitForSeconds(0.2f);
            GameObject.Find("PreviousHoldableButton").AddComponent<PreviousHoldable>();
            GameObject.Find("NextHoldableButton").AddComponent<NextHoldable>();
            GameObject.Find("EquipHoldableRight").AddComponent<SelectHoldable>().righthand = true;
            GameObject.Find("EquipHoldableLeft").AddComponent<SelectHoldable>().righthand = false;
            yield return new WaitForSeconds(0.5f);

            tabGroups.PopulateTabList(GameObject.Find("ModMenuMediaTab"));
            tabGroups.PopulateTabList(GameObject.Find("ModMenuRoomTab"));
            tabGroups.PopulateTabList(GameObject.Find("ModMenuHoldableTab"));
            anchor = GameObject.Find("HoldableAnchor");
            yield return new WaitForSeconds(0.2f);
            smg = Instantiate(smg);
            Holdable smgHoldable = smg.AddComponent<Holdable>();
            smgHoldable.holdableName = "Gorilla Smg";
            smgHoldable.holdableDescription = "I have yet to miss a shot.";
            smgHoldable.holdableAuthor = "Seventy";
            smgHoldable.ID = 82026933;
            smgHoldable.inhandLocalEulerRight = new Vector3(-167.899f, -12.29401f, 82.24f);
            smgHoldable.inhandLocalPosRight = new Vector3(-0.04093353f, 0.1725367f, -0.04228642f);
            smgHoldable.displayLocalPos = new Vector3(-0.012f, 0f, -0.029f);
            smgHoldable.displayLocalEuler = new Vector3(-90f, 0f, 0f);
            smgHoldable.inhandscale = new Vector3(10f, 10f, 10f);
            smgHoldable.displayscale = new Vector3(3f, 3f, 3f);
            smgHoldable.Done();
            smg.transform.SetParent(anchor.transform, false);
            HoldableDataBase.instance.AddToDatabase(smg, smgHoldable);
            FireArm smgFirearm = smg.AddComponent<FireArm>();
            smgFirearm.fullAuto = false;
            smgFirearm.bullet = bulletWithTrail;
            smgFirearm.bulletSpeed = 30f;
            smgFirearm.bulletGravity = true;
            smgFirearm.timeBetweenShots = 0.2f;
            yield return new WaitForSeconds(0.1f);
            smgFirearm.Done();
            smg.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            karambit = Instantiate(karambit);
            Holdable karambitHoldable = karambit.AddComponent<Holdable>();
            karambitHoldable.holdableName = "Karambit";
            karambitHoldable.holdableDescription = "Slice*";
            karambitHoldable.holdableAuthor = "Seventy";
            karambitHoldable.ID = 28882901;
            karambitHoldable.inhandLocalEulerRight = new Vector3(4.617f, 258.567f, 87.971f);
            karambitHoldable.inhandLocalPosRight = new Vector3(-0.07090556f, 0.1239289f, -0.01968596f);
            karambitHoldable.displayLocalPos = new Vector3(0.038f, 0.037f, -0.0277f);
            karambitHoldable.displayscale = new Vector3(0.5f, 0.5f, 0.5f);
            karambitHoldable.inhandscale = new Vector3(1f, 1f, 1f);
            karambitHoldable.Done();
            karambit.transform.SetParent(anchor.transform, false);
            Karambit _karambit = karambit.AddComponent<Karambit>();
            yield return new WaitForSeconds(0.1f);
            _karambit.materialList.Add(karambit.GetComponentInChildren<Renderer>().material);
            _karambit.materialList.Add(ghostbit);

            HoldableDataBase.instance.AddToDatabase(karambit,karambitHoldable);
            karambit.SetActive(false);
            yield return new WaitForSeconds(0.2f);





            yield return new WaitForSeconds(0.1f);
            katana = Instantiate(katana);
            Holdable katanaHoldable = katana.AddComponent<Holdable>();
            katanaHoldable.holdableName = "Shinobu Kocho's Katana - Demon Slayer";
            katanaHoldable.holdableDescription = "Requested by 幻幽霊#0012";
            katanaHoldable.holdableAuthor = "Seventy";
            katanaHoldable.ID = 7291625;
            katanaHoldable.inhandLocalEulerRight = new Vector3(-70.902f, -391.219f, -70.44f);
            katanaHoldable.inhandLocalPosRight = new Vector3(-0.16f, 0.126f, -0.611f);
            katanaHoldable.displayLocalPos = new Vector3(0f, -0.0541f, -0.023f);
            katanaHoldable.displayscale = new Vector3(3f, 3f, 3f);
            katanaHoldable.inhandscale = new Vector3(10f, 10f, 10f);
            katanaHoldable.Done();
            katana.transform.SetParent(anchor.transform, false);
            HoldableDataBase.instance.AddToDatabase(katana, katanaHoldable);
            katana.SetActive(false);

            bayonet = Instantiate(bayonet);
            Holdable bayonetHoldable = bayonet.AddComponent<Holdable>();
            bayonetHoldable.holdableName = "Bayonet";
            bayonetHoldable.holdableDescription = "Requested by Waika";
            bayonetHoldable.holdableAuthor = "Seventy";
            bayonetHoldable.ID = 732267225;
            bayonetHoldable.inhandLocalEulerRight = new Vector3(-116.349f, 141.019f, -60.48199f);
            bayonetHoldable.inhandLocalPosRight = new Vector3(-0.0111f, 0.0652f, -0.1114f);
            bayonetHoldable.displayscale = new Vector3(0.015f, 0.015f, 0.015f);
            bayonetHoldable.inhandscale = new Vector3(0.02f, 0.02f, 0.02f);
            bayonetHoldable.Done();
            bayonet.transform.SetParent(anchor.transform, false);
            bayonet.AddComponent<Bayonet>();
            HoldableDataBase.instance.AddToDatabase(bayonet, bayonetHoldable);
            bayonet.SetActive(false);

            yield return new WaitForSeconds(0.2f);
            sombra = Instantiate(sombra);
            Holdable sombraHoldable = sombra.AddComponent<Holdable>();
            sombraHoldable.holdableName = "Sombras SMG";
            sombraHoldable.holdableDescription = "¡Apagando las luces!";
            sombraHoldable.holdableAuthor = "Seventy";
            sombraHoldable.ID = 2179;
            sombraHoldable.inhandLocalEulerRight = new Vector3(3.986f, 257.407f, 69.787f);
            sombraHoldable.inhandLocalPosRight = new Vector3(-0.06813887f, -0.03322548f, 0.1978144f);
            sombraHoldable.displayLocalPos = new Vector3(0f, -0.0898f, -0.0215f);
            sombraHoldable.displayscale = new Vector3(1f, 1f, 1f);
            sombraHoldable.inhandscale = new Vector3(3f, 3f, 3f);
            sombraHoldable.Done();
            sombra.transform.SetParent(anchor.transform, false);

            HoldableDataBase.instance.AddToDatabase(sombra, sombraHoldable);
            FireArm sombraFirearm = sombra.AddComponent<FireArm>();
            sombraFirearm.bullet = bulletWithTrail;
            sombraFirearm.bulletSpeed = 70f;
            sombraFirearm.fullAuto = true;
            sombraFirearm.timeBetweenShots = 0.05f;
            yield return new WaitForSeconds(0.1f);
            sombraFirearm.Done();
            sombra.SetActive(false);

            yield return new WaitForSeconds(0.2f);
            nerfgun = Instantiate(nerfgun);
            Holdable nerfHoldable = nerfgun.AddComponent<Holdable>();
            nerfHoldable.holdableName = "Nerf gun";
            nerfHoldable.holdableDescription = "Pew";
            nerfHoldable.holdableAuthor = "Seventy";
            nerfHoldable.ID = 281937;
            nerfHoldable.inhandLocalEulerRight = new Vector3(69.034f, 152.251f, -15.577f);
            nerfHoldable.inhandLocalPosRight = new Vector3(-0.0345f, 0.1018f, -0.0421f);
            nerfHoldable.displayLocalPos = new Vector3(0f, 0f, -0.0129f);
            nerfHoldable.displayLocalEuler = new Vector3(0f, 90f, 0f);
            nerfHoldable.displayscale = new Vector3(1f, 1f, 1f);
            nerfHoldable.inhandscale = new Vector3(2f, 2f, 2f);
            nerfHoldable.Done();
            nerfgun.transform.SetParent(anchor.transform, false);

            HoldableDataBase.instance.AddToDatabase(nerfgun, nerfHoldable);
            FireArm nerfFirearm = nerfgun.AddComponent<FireArm>();
            nerfFirearm.bullet = nerfdart;
            nerfFirearm.bulletSpeed = 70f;
            nerfFirearm.fullAuto = false;
            nerfFirearm.timeBetweenShots = 0.15f;
            yield return new WaitForSeconds(0.1f);
            nerfFirearm.Done();
            nerfgun.SetActive(false);

            LoadHoldable();

            yield return new WaitForSeconds(0.2f);

            GorillaTagger.Instance.gameObject.AddComponent<MusicPlayer>();

            modmenuInstance.transform.SetParent(GameObject.Find("palm.01.L").transform);
            modmenuInstance.transform.localPosition = new Vector3(0.051f, -0.157f, 0.182f);
            modmenuInstance.transform.localEulerAngles = new Vector3(-7.751f, 86.913f, -0.43f);
            // GorillaGameManager.instance.currentRoom room;
            ready = true;
        }
        bool ready;
        string path;
        void LoadHoldable()
        {
            path = BepInEx.Paths.BepInExRootPath + "/SeventysHoldables/";
            var folder = Directory.CreateDirectory(BepInEx.Paths.BepInExRootPath + "/SeventysHoldables");
            Debug.Log("Made a new Holdable Folder");
            StartCoroutine(LoadFR());
        }

        public IEnumerator LoadFR()
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.holdable");
            foreach (FileInfo file in Files)
            {
                AssetBundle holdableBundle = AssetBundle.LoadFromFile(file.FullName);



                if (holdableBundle == null)
                {
                    Debug.Log("Failed to load Holdable!");
                    yield break;
                }


                GameObject holdableObject = holdableBundle.LoadAsset<GameObject>("playermodel.ParentObject");
                GameObject __instance = Instantiate(holdableObject);
                holdableBundle.Unload(false);
                holdableDataText = __instance.GetComponent<Text>().text;
                Holdable holdable = __instance.AddComponent<Holdable>();
                string[] data = holdableDataText.Split('$');
                holdable.holdableName = data[0];
                holdable.holdableAuthor = data[1];
                holdable.holdableDescription = data[2];
                bool isFirearm = bool.Parse(data[3]);
                int bulletIndex = int.Parse(data[4]);
                holdable.inhandscale = StringToVector3(data[5]);
                yield return new WaitForEndOfFrame();
                holdable.inhandLocalPosRight = StringToVector3(data[6]);
                yield return new WaitForEndOfFrame();
                holdable.inhandLocalEulerRight = StringToVector3(data[7]);
                yield return new WaitForEndOfFrame();
                holdable.inhandLocalPosLeft = StringToVector3(data[8]);
                yield return new WaitForEndOfFrame();
                holdable.inhandLocalEulerLeft = StringToVector3(data[9]);
                yield return new WaitForEndOfFrame();
                holdable.displayscale = StringToVector3(data[10]);
                yield return new WaitForEndOfFrame();
                holdable.displayLocalPos = StringToVector3(data[11]);
                yield return new WaitForEndOfFrame();
                holdable.displayLocalEuler = StringToVector3(data[12]);
                yield return new WaitForEndOfFrame();
                holdable.ID = int.Parse(data[17]);
                holdable.Done();
                __instance.transform.SetParent(anchor.transform, false);
                __instance.name = data[0];

                if (isFirearm)
                {
                    FireArm arm = __instance.AddComponent<FireArm>();
                    arm.bullet = bullets[bulletIndex];
                    arm.bulletGravity = bool.Parse(data[13]);
                    arm.timeBetweenShots = float.Parse( data[14]);
                    arm.bulletSpeed = float.Parse( data[15]);
                    arm.fullAuto = bool.Parse(data[16]);
                    arm.Done();
                }

                HoldableDataBase.instance.AddToDatabase(__instance, holdable);
                __instance.SetActive(false);


            }
        }
        static public string holdableDataText;
        public static Vector3 StringToVector3(string sVector)
        {
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }
            string[] sArray = sVector.Split(',');
            Vector3 vector = new Vector3(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]));
            return vector;
        }

        public string RoomInfoString()
        {
                int playersinRoom = GorillaTagManager.instance.currentRoom.PlayerCount;
            return string.Format("ROOM INFO\n<size=10>In Room {0}/10\nInfected{1}/10\nRoom Code: {2}", playersinRoom);

        }
        private readonly XRNode lNode = XRNode.LeftHand;
        bool leftGrip;
        bool LeftSecondary;
        bool menuActive = true;
        float nextmenuopen;
        float menuopencooldown = 0.2f;


        void Update()
        {
            InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out leftGrip);
            InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out LeftSecondary);
            if (Time.time > nextmenuopen)
            {
                if(leftGrip && LeftSecondary && ready)
                {
                    menuActive = !menuActive;
                    modmenuInstance.SetActive(menuActive);
                  nextmenuopen = Time.time+menuopencooldown;
                }
            }

          
        }


     
    }
}
