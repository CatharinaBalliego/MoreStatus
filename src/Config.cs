using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;

namespace MoreStatus
{
    public static class Config
    {
        private const string food_label = "Food Label";
        public static ConfigEntry<bool> Food_Info;
        private const string CTG_DETAILS = "Item Details Display Status Effect";
        public static ConfigEntry<bool> cfg_Dets_Status_Effect;

        public static void Init(ConfigFile config)
        {
            cfg_Dets_Status_Effect = config.Bind(CTG_DETAILS, "Display food status effects", true, "Item panel shows food status effects");
        }
    }

}
