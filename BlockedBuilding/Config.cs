using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;

namespace BlockedBuilding
{
    public class Config : IRocketPluginConfiguration
    {
        public List<Plugin.PositionBlocked> PositionBlockeds;
        public void LoadDefaults()
        {
            PositionBlockeds = new List<Plugin.PositionBlocked>();
        }
    }
}
