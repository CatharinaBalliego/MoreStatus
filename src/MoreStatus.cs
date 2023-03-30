using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MoreStatus;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uNature.Core.ClassExtensions;
using UnityEngine;

// RENAME 'OutwardModTemplate' TO SOMETHING ELSE
namespace OutwardModTemplate
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("com.sinai.SideLoader", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
    public class MoreStatus : BaseUnityPlugin
    {
        public const string GUID = "catharina.morestatus";
        public const string NAME = "More Status";
        public const string VERSION = "1.0.0";

        public static ManualLogSource Log => Instance.Logger;

        public static MoreStatus Instance;
        public static GameObject gameObject;

        public FoodStatus foodStatus;

        
        

        //public static showInfo(int playerID) => GlobalHideInfo;

        //internal static bool GlobalHideInfo => OptionManager.m_pl

        // If you need settings, define them like so:
        public static ConfigEntry<bool> ExampleConfig;

        // Awake is called when your plugin is created. Use this to set up your mod.
        internal void Awake()
        {
            Instance = this;

            //Log = this.Logger;

            Log.LogMessage($"Hello world from {NAME} {VERSION}!");
            // Any config settings you define should be set up like this:
            //ExampleConfig = Config.Bind("ExampleCategory", "ExampleSetting", false, "This is an example setting.");

            // Harmony is for patching methods. If you're not patching anything, you can comment-out or delete this line.
            new Harmony(GUID).PatchAll();

            SL.OnPacksLoaded += Setup;

    
        }

        // Update is called once per frame. Use this only if needed.
        // You also have all other MonoBehaviour methods available (OnGUI, etc)
        internal void Update()
        {
           
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
