using HarmonyLib;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static ItemDetailsDisplay;

namespace MoreStatus
{
    [HarmonyPatch(typeof(ItemDetailsDisplay), "RefreshDisplay")] // o metodo está errado.

    public class FoodStatus
    {
        // static AccessTools.FieldRef<Item, string> food =
        //AccessTools.FieldRefAccess<Item, string>("Description");
        [HarmonyPostfix]
        public static void RefreshDisplay(ItemDetailsDisplay __instance, IItemDisplay _itemDisplay)
        {


            if (Config.cfg_Dets_Status_Effect.Value)
            {
                if (_itemDisplay.RefItem.IsPerishable && _itemDisplay.RefItem.CurrentDurability > 0)
                {
                    List<ItemDetailRowDisplay> m_detailRows = (List<ItemDetailRowDisplay>)AccessTools.Field(typeof(ItemDetailsDisplay), "m_detailRows").GetValue(__instance);
                    ItemDetailRowDisplay row = (ItemDetailRowDisplay)AccessTools.Method(typeof(ItemDetailsDisplay), "GetRow").Invoke(__instance, new object[] { m_detailRows.Count });
                    //ItemDetailRowDisplay row = (ItemDetailRowDisplay)AccessTools.Method(typeof(ItemDetailsDisplay), "GetRow").Invoke(__instance, new object[] { m_detailRows.Count });
                    row.SetInfo(LocalizationManager.Instance.GetLoc("ItemStat_Effect"), $"{_itemDisplay.RefItem.m_effects}");
                    //  row.SetInfo(LocalizationManager.Instance.GetLoc("ItemStat_Durability"), $"{_itemDisplay.RefItem.CurrentDurability}/{_itemDisplay.RefItem.MaxDurability}");

                }
            }
        }
        //public static void test()
        //{
        //    StatusData item = new StatusData(this.StatusData);
        //    this.m_statusStack.Add(item);
        //    this.m_totalData = new StatusData.EffectData[this.StatusData.EffectsData.Length];
        //    for (int i = 0; i < this.m_totalData.Length; i++)
        //    {
        //        this.m_totalData[i].Data = new string[this.StatusData.EffectsData[i].Data.Length];
        //    }
        //}


    }

    [HarmonyPatch(typeof(ItemDetailsDisplay), "RefreshDetail")]
    public class ItemDetailsDisplay_RefreshDetail
    {
        [HarmonyPostfix]
        public static void RefreshDetail(ItemDetailsDisplay __instance, int _rowIndex, DisplayedInfos _infoType)
        {
            //if (!Config.cfg_Dets_Status_Effect.Value)
            //{
            //    return;
            //}

            Item m_lastItem = (Item)AccessTools.Field(typeof(ItemDetailsDisplay), "m_lastItem").GetValue(__instance);

            if (m_lastItem.IsPerishable && m_lastItem.CurrentDurability > 0)
            {
                ItemDetailRowDisplay row = (ItemDetailRowDisplay)AccessTools.Method(typeof(ItemDetailsDisplay), "GetRow").Invoke(__instance, new object[] { _rowIndex });
                Text m_lblDataName = (Text)AccessTools.Field(typeof(ItemDetailRowDisplay), "m_lblDataName").GetValue(row);
                if (m_lastItem.IsFood)
                {
                    row.SetInfo(LocalizationManager.Instance.GetLoc("ItemStat_Effect"), $"{m_lastItem.m_effects}");
                }
            }
        }
    }

}
