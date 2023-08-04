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
        static  EffectInfo effectInfo = new EffectInfo();
       

        [HarmonyPatch(typeof(ItemDetailsDisplay), "RefreshDetail")]
        public class ItemDetailsDisplay_RefreshDetail
        {
            //mudar para prefix nao resolveu para atualizar a descricao antes.
            [HarmonyPrefix]
            public static void RefreshDetail(ItemDetailsDisplay __instance, int _rowIndex, DisplayedInfos _infoType)
            {

                Item m_lastItem = (Item)AccessTools.Field(typeof(ItemDetailsDisplay), "m_lastItem").GetValue(__instance);
                if (m_lastItem.IsPerishable && m_lastItem.CurrentDurability > 0 )
                {
                    if (m_lastItem.IsFood)
                    {
                       
                        foreach (var effect in m_lastItem.m_effects)
                        {
                            if (effect.Value.Effect.GetType() == typeof(AddStatusEffect))
                            {
                                var castTypeEffect = (AddStatusEffect)effect.Value.Effect;
                                if (effectInfo.effectsInfo.ContainsKey(castTypeEffect.Status.IdentifierName))
                                {
                                    var description = "\n" + castTypeEffect.Status.Description ;
                                    description = description.Replace("[E1V1]", effectInfo.GetRecoveryRate(castTypeEffect.Status.IdentifierName));
                                    
                                    
                                    //check if description contains info before inserting
                                    if (!m_lastItem.m_localizedDescription.Contains(description))
                                    {
                                        m_lastItem.m_localizedDescription += description;
                                    }

                                }
                                //m_lastItem.m_localizedDescription += castTypeEffect.Status.Description;
                            }
                        }
                    }
                }
            }
        }
    }
}
