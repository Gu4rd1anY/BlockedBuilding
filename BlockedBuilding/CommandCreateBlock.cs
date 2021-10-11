using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Unturned.Player;
using Rocket.API;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;

namespace BlockedBuilding
{
    class CommandCreateBlock : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "createblock".ToLower();

        public string Help => "";

        public string Syntax => "Write: /<createblock | /cb> <radius>";

        public List<string> Aliases => new List<string> { "cb".ToLower() };

        public List<string> Permissions => new List<string> { "command.createblock", "command.cb" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length != 1)
            {
                UnturnedChat.Say(player, Syntax);
                return;
            }

            if (float.TryParse(command[0], out float radius))
            {
                if (radius < 1)
                {
                    UnturnedChat.Say(player, "Use numbers that are in the plus category.", Color.red);
                    return;
                }
                Plugin.Instance.Configuration.Instance.PositionBlockeds.Add(new Plugin.PositionBlocked(player.Position, radius));
                Plugin.Instance.Configuration.Save();
                UnturnedChat.Say(player, Plugin.Instance.Translate("create_point", radius), Color.yellow);
            }
            else
            {
                UnturnedChat.Say(player, Syntax);
                return;
            }
        }
    }
}
