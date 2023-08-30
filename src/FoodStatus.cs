using BepInEx.Logging;
using HarmonyLib;
using ItemDetailMoreStatus;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using uNature.Wrappers.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ItemDetailsDisplay;


namespace MoreStatus
{
    [HarmonyPatch(typeof(ItemDetailsDisplay), "RefreshDisplay")]
    public class FoodStatus
    {
        static  EffectInfo effectConverter = new EffectInfo();
       

        [HarmonyPatch(typeof(ItemDetailsDisplay), "RefreshDetail")]
        public class ItemDetailsDisplay_RefreshDetail
        {
            [HarmonyPrefix]
            public static void RefreshDetail(ItemDetailsDisplay __instance, int _rowIndex, DisplayedInfos _infoType)
            {
                Item m_lastItem = (Item)AccessTools.Field(typeof(ItemDetailsDisplay), "m_lastItem").GetValue(__instance);
                
                    if (m_lastItem.IsFood)
                    {
                        string effects_description = "";

                        foreach (var effect in m_lastItem.m_effects)
                        {
                            if (effect.Value.Effect.GetType() == typeof(AddStatusEffect))
                            {
                                var status_effect = (AddStatusEffect)effect.Value.Effect;
                                if (effectConverter.effectsInfo.ContainsKey(status_effect.Status.IdentifierName))
                                {
                                    var status_description = status_effect.Status.Description + "\n" ;

                                    status_description = status_description.Replace("[E1V1]", effectConverter.GetRecoveryRate(status_effect.Status.IdentifierName));
                                    
                                    effects_description += status_description;
                                }
                            }
                        }

                        //prevents from inserting same info multiple times when selecting same item again
                        if (!m_lastItem.m_localizedDescription.Contains(effects_description))
                        {
                            m_lastItem.m_localizedDescription = effects_description + "\n" + m_lastItem.m_localizedDescription;
                        }
                    }
            }
        }
    }
}
