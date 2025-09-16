using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using Constants;
using GorillaExtensions;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GTPCPrivacy
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static AssetBundle bundle;
        public static GameObject assetBundleParent;
        private Button proButton;
        private TextMeshProUGUI screenText;
        private bool isHid = false;
        
        void Start() => GorillaTagger.OnPlayerSpawned(OnGameInitialized);

        void OnGameInitialized()
        {

            bundle = LoadAssetBundle("GTPCPrivacy.AssetBundles.hider"); 

            isHid = true;

            assetBundleParent = Instantiate(bundle.LoadAsset<GameObject>("hider"));
            

            foreach (TextMeshProUGUI textComponent in assetBundleParent.GetComponentsInChildren<TextMeshProUGUI>())
            {
                textComponent.font = VRRig.LocalRig.playerText1.font;
            }

            proButton = assetBundleParent.GetComponentInChildren<Button>();

            screenText = assetBundleParent.transform.Find("Canvas/Panel/Text (TMP)").GetComponent<TextMeshProUGUI>();  
            screenText.transform.localPosition = new Vector3(174.6985f, -150f, 0);
            
            proButton.transform.position = new Vector3(977.6f, 193.896f, 4f);
            proButton.transform.localPosition = new Vector3(30f, -346.104f, 4f);
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        void FixedUpdate() // used to be update but fixedupdate made it a bit better
        {
                proButton.onClick.AddListener(() =>
                    {
                        isHid = !isHid;
                        assetBundleParent.transform.Find("Canvas/Panel").gameObject.SetActive(isHid);
                    }
                );
                if (!assetBundleParent.transform.Find("Canvas/Panel").gameObject.activeInHierarchy)
                {
                    proButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "HIDE GAME FROM PC";
                }
                else if (assetBundleParent.transform.Find("Canvas/Panel").gameObject.activeInHierarchy)
                {
                    proButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "SHOW GAME FROM PC"; 
                }
        }
        
        }

    }

