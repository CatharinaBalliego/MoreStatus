using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MoreStatus;
using SideLoader;
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


        internal void Awake()
        {
            Instance = this;
    
            new Harmony(GUID).PatchAll();

           // SL.OnPacksLoaded += Setup;
        }
        
        internal void Setup()
        {

            var packName = "More Status - Catharina";
            var pack = SL.GetSLPack(packName);
            var bundle = pack.AssetBundles["separatorbundle"];
           
            var canvasAsset = bundle.LoadAsset<GameObject>("separatorBundle");

            SeparatorGO = Instantiate(canvasAsset);

            DontDestroyOnLoad(SeparatorGO);

            SeparatorGO.AddComponent<GUIManager>(); 


            Log.LogMessage("more status finished set up");
        }
    }
}
