﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;

namespace ExperienceAndClasses {
    class EACWorld : ModWorld {
        public static string password = "";

        public override void PostUpdate() {
            Systems.NPCRewards.ServerProcessXPBuffer();
        }

        public override TagCompound Save() {
            return new TagCompound {
                ["eac_password"] = Systems.Password.world_password
            };
        }

        public override void Load(TagCompound tag) {
            Systems.Password.world_password = Utilities.Commons.TagTryGet<string>(tag, "eac_password", "");
        }

    }
}