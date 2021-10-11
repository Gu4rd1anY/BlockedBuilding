using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using SDG.Unturned;
using Rocket.Unturned.Chat;
using UnityEngine;
using Rocket.API.Collections;

namespace BlockedBuilding
{
    public class Plugin : RocketPlugin<Config>
    {
        public class PositionBlocked
        {
            public Vector3 Position;
            public float Radius;

            public PositionBlocked(Vector3 Position, float Radius)
            {
                this.Position = Position;
                this.Radius = Radius;
            }

            public PositionBlocked()
            {

            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                TranslationList list = new TranslationList
                {
                    { "block_msg", "На данной территории нельзя ничего ставить." },
                    { "create_point", "Вы создали точку. На вашем местоположении в радиусе: {0} запрещены постройки." }
                };
                return list;
            }
        }

        public static Plugin Instance;
        
        DateTime lastCalled = DateTime.Now;

        protected override void Load()
        {
            Instance = this;

        }

        protected override void Unload()
        {

        }

        public void FixedUpdate ()
        {
            if ((DateTime.Now - lastCalled).TotalSeconds >= 0.1)
            {
                foreach (SteamPlayer sp in Provider.clients)
                {
                    if (sp != null)
                    {
                        UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(sp);
                        if (player.HasPermission("admin.blockedbuilding")) return;
                        if (player.Player.equipment.isEquipped && player.Player.equipment.useable is UseableBarricade || player.Player.equipment.isEquipped && player.Player.equipment.useable is UseableStructure)
                        {
                            foreach (PositionBlocked position in Configuration.Instance.PositionBlockeds)
                            {
                                if (Vector3.Distance(position.Position, player.Position) <= position.Radius)
                                {
                                    player.Player.equipment.dequip();
                                    UnturnedChat.Say(player, Translate("block_msg"), Color.red);
                                }
                            }
                        }
                    }
                }
                lastCalled = DateTime.Now;
            }
        }
    }
}
