using HarmonyLib;
using ItemDetailMoreStatus;
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
    [HarmonyPatch(typeof(ItemDetailsDisplay), "RefreshDisplay")]

    public class FoodStatus
    {
        [HarmonyPatch(typeof(ItemDetailsDisplay), "RefreshDetail")]
        public class ItemDetailsDisplay_RefreshDetail
        {
            [HarmonyPostfix]
            public static void RefreshDetail(ItemDetailsDisplay __instance, int _rowIndex, DisplayedInfos _infoType)
            {
                Item m_lastItem = (Item)AccessTools.Field(typeof(ItemDetailsDisplay), "m_lastItem").GetValue(__instance);
                if (m_lastItem.IsPerishable && m_lastItem.CurrentDurability > 0)
                {
                    ItemDetailRowDisplay row = (ItemDetailRowDisplay)AccessTools.Method(typeof(ItemDetailsDisplay), "GetRow").Invoke(__instance, new object[] { _rowIndex });
                    Text m_lblDataName = (Text)AccessTools.Field(typeof(ItemDetailRowDisplay), "m_lblDataName").GetValue(row);

                    List<ItemDetailRowDisplay> m_detailRows = (List<ItemDetailRowDisplay>)AccessTools.Field(typeof(ItemDetailsDisplay), "m_detailRows").GetValue(__instance);

                    if (m_lastItem.IsFood)
                    {
                        //row.SetInfo(LocalizationManager.Instance.GetLoc("ItemStat_Durability"), $"{m_lastItem.m_effects}");
                        row.SetInfo("Effect", "test");

                        foreach (var effect in m_lastItem.m_effects)
                        {
                            if(effect.Value.Effect.GetType() == typeof(AddStatusEffect))
                            {
                                var castTypeEffect = (AddStatusEffect)effect.Value.Effect;
                                ItemDetailMoreStatus.MoreStatus.Log.LogMessage($"IdentifierName: {castTypeEffect.Status.IdentifierName}");
                                ItemDetailMoreStatus.MoreStatus.Log.LogMessage($"Description: {castTypeEffect.Status.Description}");
                            }
                        }
                    }
                }
            }
        }
    }
}
