using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreStatus
{
    public class EffectInfo
    {
        public Dictionary<string,string> effectsInfo = new Dictionary<string,string>();
        public EffectInfo() {
            effectsInfo = new Dictionary<string, string>()
            {
                {"Stamina Recovery Small", "0.4"},
                {"Stamina Recovery 2", "0.6" },
                {"Stamina Recovery 3", "0.8" },
                {"Stamina Recovery 4", "1"   },
                {"Stamina Recovery 5", "1.2" },
                {"Health Recovery 1", "0.2" },
                {"Health Recovery 2", "0.25" },
                {"Health Recovery 3", "0.3" },
                {"Health Recovery 4", "0.4" },
                {"Health Recovery 5", "0.5" },
                {"Environment Heat Resistance", "15" }
            };

            
        }

        public string GetRecoveryRate(string effectName)
        {
            return effectsInfo.FirstOrDefault(e => e.Key == effectName).Value;
        }
    }
}
