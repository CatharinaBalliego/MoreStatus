using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MoreStatus;
using SideLoader;
using System;
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
        public static ManualLogSource Log => Instance.Logger;

        public static GameObject gameObject;

        public FoodStatus foodStatus;

        // If you need settings, define them like so:
        //public static ConfigEntry<bool> ExampleConfig;

        internal void Awake()
        {
            Instance = this;
            
            Log.LogMessage($"Hello world from {NAME} {VERSION}!");

            // Any config settings you define should be set up like this:
            //Don't forget!
            //ExampleConfig = Config.Bind("ExampleCategory", "ExampleSetting", false, "This is an example setting.");
    
            new Harmony(GUID).PatchAll();

            //SL.OnPacksLoaded += Setup;

        }
        
        internal void Setup()
        {
            

            //var packName = "More Status - Catharina";
            //var pack = SL.GetSLPack(packName);
            var clonedGaberries = ResourcesPrefabManager.Instance.GetItemPrefab(4000010);

            var template = new SL_Item()
            {
                Target_ItemID = 4000010, // gaberries
                Description = clonedGaberries.Description
            };

            
            template.Description += "test";
            template.SLPackName = "More Status - Catharina";
            template.SubfolderName = "Items";

            template.Apply();

        }
        
       
    }
}
