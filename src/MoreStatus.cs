using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MoreStatus;
using SideLoader;
using System;
using System.Linq;
using uNature.Core.ClassExtensions;
using uNature.Wrappers.Linq;
using UnityEngine;


namespace ItemDetailMoreStatus
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("com.sinai.SideLoader", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
    public class MoreStatus : BaseUnityPlugin
    {
        public const string GUID = "catharina.morestatus";
        public const string NAME = "More Status";
        public const string VERSION = "1.0.0";

        public static MoreStatus Instance;
        public static GameObject SeparatorGO;
        public static ManualLogSource Log => Instance.Logger;

        public static GameObject GameObject;

        public FoodStatus foodStatus;

        // If you need settings, define them like so:
        //public static ConfigEntry<bool> ExampleConfig;

        internal void Awake()
        {
            Instance = this;
            
            //Log.LogMessage($"Hello world from {NAME} {VERSION}!");

            // Any config settings you define should be set up like this:
            //Don't forget!
            //ExampleConfig = Config.Bind("ExampleCategory", "ExampleSetting", false, "This is an example setting.");
    
            new Harmony(GUID).PatchAll();

            SL.OnPacksLoaded += Setup;

        }
        
        internal void Setup()
        {

            var packName = "More Status - Catharina";
            var pack = SL.GetSLPack(packName);
            var bundle = pack.AssetBundles["separatorbundle"];
           
            var canvasAsset = bundle.LoadAsset<GameObject>("separatorBundle");

            SeparatorGO = Instantiate(canvasAsset);

            DontDestroyOnLoad(SeparatorGO);

            Log.LogMessage("before get canvas"); // daqui pra baixo nao funciona, mas a  imagem apareceu

            var canvas = (Canvas)SeparatorGO.GetComponentByName("Canvas");
           // var canvas = SeparatorGO.GetComponent<Canvas>();
            canvas.sortingOrder = 999;

            Log.LogMessage("MSC: before adding component");
            SeparatorGO.AddComponent<GUIManager>(); 

            //var targetMgrHolder = GameObject.transform.Find("separator").gameObject;

            Log.LogMessage("more status finished set up");


        }
        
       
    }
}
