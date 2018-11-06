﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace ExperienceAndClasses.Systems {
    class Class {
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constants (and readonly) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        //DO NOT CHANGE THE ORDER OF IDs
        public enum CLASS_IDS : byte {
            New, //newly created chracters will momentarily have this class before being set to novice
            None, //no class selected (intentionally)
            Novice,
            Warrior,
            Ranger,
            Mage,
            Rogue,
            Summoner,
            Cleric,
            Hybrid,
            Knight,
            Berserker,
            Guardian,
            Trickshot,
            Engineer,
            Sniper,
            Mystic,
            Sage,
            Assassin,
            Ninja,
            Hivemind,
            SoulBinder,
            Saint,
            HybridPrime,

            //insert here

            NUMBER_OF_IDs, //leave this last
        }

        //which classes to show in ui and where
        public static byte[,] class_locations = new byte[5,7];

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Treated like readonly ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public static Class[] CLASS_LOOKUP { get; private set; }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Populate Lookup ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        static Class() {
            CLASS_LOOKUP = new Class[(byte)CLASS_IDS.NUMBER_OF_IDs];

            string name, desc;
            byte tier, id_byte;
            Texture2D texture;
            CLASS_IDS id_prereq;
            PowerScaling.POWER_SCALING_TYPES power_scaling;

            for (CLASS_IDS id = 0; id < CLASS_IDS.NUMBER_OF_IDs; id++) {
                id_byte = (byte)id;

                //defaults
                name = "Unknown" + id;
                desc = "";
                tier = 0;
                id_prereq = CLASS_IDS.New;
                texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Novice");
                power_scaling = PowerScaling.POWER_SCALING_TYPES.None;

                //specifics
                switch (id) {
                    case CLASS_IDS.None:
                        name = "None";
                        break;

                    case CLASS_IDS.Novice:
                        name = "Novice";
                        desc = "TODO_description";
                        tier = 1;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Novice");
                        class_locations[0, 3] = id_byte;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.All;
                        break;

                    case CLASS_IDS.Warrior:
                        name = "Warrior";
                        desc = "TODO_description";
                        tier = 2;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Warrior");
                        class_locations[1, 0] = id_byte;
                        id_prereq = CLASS_IDS.Novice;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Melee;
                        break;

                    case CLASS_IDS.Ranger:
                        name = "Ranger";
                        desc = "TODO_description";
                        tier = 2;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Ranger");
                        class_locations[1, 1] = id_byte;
                        id_prereq = CLASS_IDS.Novice;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Ranged;
                        break;

                    case CLASS_IDS.Mage:
                        name = "Mage";
                        desc = "TODO_description";
                        tier = 2;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Mage");
                        class_locations[1, 2] = id_byte;
                        id_prereq = CLASS_IDS.Novice;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Magic;
                        break;

                    case CLASS_IDS.Rogue:
                        name = "Rogue";
                        desc = "TODO_description";
                        tier = 2;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Rogue");
                        class_locations[1, 3] = id_byte;
                        id_prereq = CLASS_IDS.Novice;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Rogue;
                        break;

                    case CLASS_IDS.Summoner:
                        name = "Summoner";
                        desc = "TODO_description";
                        tier = 2;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Summoner");
                        class_locations[1, 4] = id_byte;
                        id_prereq = CLASS_IDS.Novice;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Minion;
                        break;

                    case CLASS_IDS.Cleric:
                        name = "Cleric";
                        desc = "TODO_description";
                        tier = 2;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Cleric");
                        class_locations[1, 5] = id_byte;
                        id_prereq = CLASS_IDS.Novice;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.All;
                        break;

                    case CLASS_IDS.Hybrid:
                        name = "Hybrid";
                        desc = "TODO_description";
                        tier = 2;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Hybrid");
                        class_locations[1, 6] = id_byte;
                        id_prereq = CLASS_IDS.Novice;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.All;
                        break;

                    case CLASS_IDS.Knight:
                        name = "Knight";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Knight");
                        class_locations[2, 0] = id_byte;
                        id_prereq = CLASS_IDS.Warrior;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Melee;
                        break;

                    case CLASS_IDS.Berserker:
                        name = "Berserker";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Berserker");
                        class_locations[3, 0] = id_byte;
                        id_prereq = CLASS_IDS.Warrior;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Melee;
                        break;

                    case CLASS_IDS.Guardian:
                        name = "Guardian";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Guardian");
                        class_locations[4, 0] = id_byte;
                        id_prereq = CLASS_IDS.Warrior;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Melee;
                        break;

                    case CLASS_IDS.Sniper:
                        name = "Sniper";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Sniper");
                        class_locations[2, 1] = id_byte;
                        id_prereq = CLASS_IDS.Ranger;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Ranged;
                        break;

                    case CLASS_IDS.Trickshot:
                        name = "Trickshot";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Trickshot");
                        class_locations[3, 1] = id_byte;
                        id_prereq = CLASS_IDS.Ranger;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Ranged;
                        break;

                    case CLASS_IDS.Engineer:
                        name = "Engineer";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Engineer");
                        class_locations[4, 1] = id_byte;
                        id_prereq = CLASS_IDS.Ranger;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Ranged;
                        break;

                    case CLASS_IDS.Mystic:
                        name = "Mystic";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Mystic");
                        class_locations[2, 2] = id_byte;
                        id_prereq = CLASS_IDS.Mage;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Magic;
                        break;

                    case CLASS_IDS.Sage:
                        name = "Sage";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Sage");
                        class_locations[3, 2] = id_byte;
                        id_prereq = CLASS_IDS.Mage;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Magic;
                        break;

                    case CLASS_IDS.Assassin:
                        name = "Assassin";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Assassin");
                        class_locations[2, 3] = id_byte;
                        id_prereq = CLASS_IDS.Rogue;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Melee;
                        break;

                    case CLASS_IDS.Ninja:
                        name = "Ninja";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Ninja");
                        class_locations[3, 3] = id_byte;
                        id_prereq = CLASS_IDS.Rogue;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Throwing;
                        break;

                    case CLASS_IDS.SoulBinder:
                        name = "Soul Binder";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_SoulBinder");
                        class_locations[2, 4] = id_byte;
                        id_prereq = CLASS_IDS.Summoner;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Minion;
                        break;

                    case CLASS_IDS.Hivemind:
                        name = "Hivemind";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Hivemind");
                        class_locations[3, 4] = id_byte;
                        id_prereq = CLASS_IDS.Summoner;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.Minion;
                        break;

                    case CLASS_IDS.Saint:
                        name = "Saint";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_Saint");
                        class_locations[2, 5] = id_byte;
                        id_prereq = CLASS_IDS.Cleric;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.All;
                        break;

                    case CLASS_IDS.HybridPrime:
                        name = "Hybrid Prime";
                        desc = "TODO_description";
                        tier = 3;
                        texture = ModLoader.GetTexture("ExperienceAndClasses/Textures/Tokens/ClassToken_HybridPrime");
                        class_locations[2, 6] = id_byte;
                        id_prereq = CLASS_IDS.Hybrid;
                        power_scaling = PowerScaling.POWER_SCALING_TYPES.All;
                        break;
                }

                //add
                CLASS_LOOKUP[id_byte] = new Class(id_byte, name, desc, tier, texture, (byte)id_prereq, PowerScaling.POWER_SCALING_LOOKUP[(byte)power_scaling]);
            }
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Instance ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public byte ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public byte Tier { get; private set; }
        public Texture2D Texture { get; private set; }
        public byte ID_Prereq { get; private set; }
        public PowerScaling Power_Scaling { get; private set; }

        public Class(byte id, string name, string description, byte tier, Texture2D texture, byte id_prereq, PowerScaling power_scaling) {
            ID = id;
            Name = name;
            Description = description;
            Tier = tier;
            Texture = texture;
            ID_Prereq = id_prereq;
            Power_Scaling = power_scaling;
        }

        public string GetPrereqString() {
            string text = "REQUIREMENT:\n" + CLASS_LOOKUP[ID_Prereq].Name;
            switch (Tier) {
                case 2:
                    text += " Lv." + Shared.LEVEL_REQUIRED_TIER_2;
                    break;

                case 3:
                    text += " Lv." + Shared.LEVEL_REQUIRED_TIER_3;
                    break;
            }
            return text;
        }

        public string GetDamagetring() {
            return "DAMAGE TYPE:\n" + Power_Scaling.Name;
        }

        public string GetAttributeString() {
            return "ATTRIBUTES:\nTODO_attribute ★★★☆☆";
        }
    }
}
