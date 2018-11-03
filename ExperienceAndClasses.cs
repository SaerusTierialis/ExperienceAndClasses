using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Reflection;

//needed for compiling outside of Terraria
public class Application
{
    [STAThread]
    static void Main(string[] args) { }
}

namespace ExperienceAndClasses
{
    class ExperienceAndClasses : Mod {
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Debug ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static bool trace = true;

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constants (and readonly) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static readonly byte[] VERSION = new byte[] { 2, 0, 0 };

        public static readonly bool IS_SERVER = (Main.netMode == 2);
        public static readonly bool IS_CLIENT = (Main.netMode == 1);
        public static readonly bool IS_SINGLEPLAYER = (Main.netMode == 0);

        public enum MESSAGE_TYPE : byte {
            BROADCAST_TRACE,
            FORCE_CLASS,
        };

        //chat colors must go here or server gives "Error on message Terraria.MessageBuffer"
        public static readonly Color COLOUR_MESSAGE_ERROR = new Color(255, 25, 25);
        public static readonly Color COLOUR_MESSAGE_TRACE = new Color(255, 0, 255);

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Treated like readonly ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static MPlayer LOCAL_MPLAYER;
        public static Mod MOD;

        public static ModHotKey HOTKEY_UI;

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Variables ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static bool inventory_state = false;

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constructor ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public ExperienceAndClasses() {
            Properties = new ModProperties() {
                Autoload = true,
            };
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Load/Unload ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public override void Load() {
            MOD = this;

            //hotkeys
            HOTKEY_UI = RegisterHotKey("Show Class Interface", "P");

        }

        public override void Unload() {
            //hotkeys
            HOTKEY_UI = null;
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ UI ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static void SetUIAutoStates() {
            inventory_state = Main.playerInventory;
            if (UI.UIClass.Instance.panel.Auto) UI.UIClass.Instance.Visibility = inventory_state;
            if (UI.UIBars.Instance.panel.Auto) UI.UIBars.Instance.Visibility = !inventory_state;
            if (UI.UIStatus.Instance.panel.Auto) UI.UIStatus.Instance.Visibility = !inventory_state;
        }

        public override void UpdateUI(GameTime gameTime) {
            //auto ui states
            if (inventory_state != Main.playerInventory) {
                SetUIAutoStates();
            }

            if (UI.UIInfo.Instance.Visibility) UI.UIInfo.Instance.UI.Update(gameTime);
            if (UI.UIClass.Instance.Visibility) UI.UIClass.Instance.UI.Update(gameTime);
            if (UI.UIBars.Instance.Visibility) UI.UIBars.Instance.UI.Update(gameTime);
            if (UI.UIStatus.Instance.Visibility) UI.UIStatus.Instance.UI.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1) {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("EAC_UIMain",
                    delegate {
                        if (UI.UIInfo.Instance.Visibility) UI.UIInfo.Instance.state.Draw(Main.spriteBatch);
                        if (UI.UIClass.Instance.Visibility) UI.UIClass.Instance.state.Draw(Main.spriteBatch);
                        if (UI.UIBars.Instance.Visibility) UI.UIBars.Instance.state.Draw(Main.spriteBatch);
                        if (UI.UIStatus.Instance.Visibility) UI.UIStatus.Instance.state.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Packets ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            byte[] bytes;

            //first 2 bytes are always type and sender
            MESSAGE_TYPE message_type = (MESSAGE_TYPE)reader.ReadByte();
            byte player_ind = (byte)reader.ReadByte();

            Player sender_player = Main.player[player_ind];
            MPlayer sender_mplayer = sender_player.GetModPlayer<MPlayer>(this);

            if (trace) {
                Commons.Trace("Recieved " + message_type + " originating from player " + player_ind);
            }
           
            switch (message_type) {
                case MESSAGE_TYPE.BROADCAST_TRACE:
                    //read
                    string message = reader.ReadString();

                    //broadcast
                    NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message), ExperienceAndClasses.COLOUR_MESSAGE_TRACE);

                    break;

                case MESSAGE_TYPE.FORCE_CLASS:
                    //read
                    bytes = reader.ReadBytes(4);

                    //set
                    sender_mplayer.ForceClass(bytes[0], bytes[1], bytes[2], bytes[3]);

                    //relay
                    if (IS_SERVER) {
                        PacketSender.SendForceClass(player_ind, bytes[0], bytes[1], bytes[2], bytes[3]);
                    }

                    break;


                /*
                case MESSAGE_TYPE.SYNC_TEST:
                    //read
                    player_ind = reader.ReadByte();
                    int1 = reader.ReadInt32();

                    //apply
                    player = Main.player[player_ind]; //sender
                    mplayer = player.GetModPlayer<MPlayer>(this);
                    mplayer.sync_test = int1;

                    //relay
                    if (IS_SERVER) {
                        ModPacket packet = MOD.GetPacket();
                        packet.Write((byte)ExperienceAndClasses.MESSAGE_TYPE.SYNC_TEST);
                        packet.Write((byte)player_ind);
                        packet.Write(int1);
                        packet.Send(-1, player_ind);
                    }
                    break;
                */

                default:
                    //unknown type
                    break;
            }
        }

    }
}
