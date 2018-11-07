﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;

namespace ExperienceAndClasses.Systems {
    class Attribute {
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constants (and readonly) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        //DO NOT CHANGE THE ORDER OF IDs
        public enum ATTRIBUTE_IDS : byte {
            Power,
            Vitality,
            Mind,
            Spirit,
            Agility,
            Dexterity,

            //insert here

            NUMBER_OF_IDs, //leave this last
        }

        //this may be reordered, UI uses this order
        public static ATTRIBUTE_IDS[] ATTRIBUTES_UI_ORDER = new ATTRIBUTE_IDS[] { ATTRIBUTE_IDS.Power, ATTRIBUTE_IDS.Vitality, ATTRIBUTE_IDS.Mind, ATTRIBUTE_IDS.Spirit, ATTRIBUTE_IDS.Agility, ATTRIBUTE_IDS.Dexterity };

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Treated like readonly ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public static Attribute[] ATTRIBUTE_LOOKUP { get; private set; }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Populate Lookup ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        static Attribute() {
            ATTRIBUTE_LOOKUP = new Attribute[(byte)ATTRIBUTE_IDS.NUMBER_OF_IDs];

            byte id_byte;
            string name, name_short, desc;
            bool active;

            for (ATTRIBUTE_IDS id = 0; id < ATTRIBUTE_IDS.NUMBER_OF_IDs; id++) {
                id_byte = (byte)id;

                //bonuses are defined in the instance because it would be a lot to pass to constructor

                //defaults
                name = "unknown";
                name_short = "unknown";
                desc = "unknown";
                active = true;

                switch (id) {
                    case ATTRIBUTE_IDS.Power:
                        name = "Power";
                        name_short = "PWR";
                        desc = "TODO_description";
                        break;

                    case ATTRIBUTE_IDS.Vitality:
                        name = "Vitality";
                        name_short = "VIT";
                        desc = "TODO_description";
                        break;

                    case ATTRIBUTE_IDS.Mind:
                        name = "Mind";
                        name_short = "MND";
                        desc = "TODO_description";
                        break;

                    case ATTRIBUTE_IDS.Spirit:
                        name = "Spirit";
                        name_short = "SPT";
                        desc = "TODO_description";
                        break;

                    case ATTRIBUTE_IDS.Agility:
                        name = "Agility";
                        name_short = "AGI";
                        desc = "TODO_description";
                        break;

                    case ATTRIBUTE_IDS.Dexterity:
                        name = "Dexterity";
                        name_short = "DEX";
                        desc = "TODO_description";
                        break;

                    default:
                        active = false;
                        break;
                }

                ATTRIBUTE_LOOKUP[id_byte] = new Attribute(id_byte, name, name_short, desc, active);
            }
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Instance ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public byte ID { get; private set; }
        public string Name { get; private set; }
        public string Name_Short { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public string Bonus { get; private set; }

        public Attribute(byte id, string name, string name_short, string description, bool active) {
            ID = id;
            Name = name;
            Name_Short = name_short;
            Description = description;
            Active = active;
            Bonus = "";
        }

        //per point bonuses
        private const float POWER_DAMAGE = 1f;

        public void ApplyEffect(MPlayer mplayer) {
            if (Active) {

                switch ((ATTRIBUTE_IDS)ID) {
                    case ATTRIBUTE_IDS.Power:
                        //TODO
                        break;

                    case ATTRIBUTE_IDS.Vitality:
                        //TODO
                        break;

                    case ATTRIBUTE_IDS.Mind:
                        //TODO
                        break;

                    case ATTRIBUTE_IDS.Spirit:
                        //TODO
                        break;

                    case ATTRIBUTE_IDS.Agility:
                        //TODO
                        break;

                    case ATTRIBUTE_IDS.Dexterity:
                        //TODO
                        break;
                }
            }
        }
    }

    class PowerScaling {
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constants (and readonly) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public enum POWER_SCALING_TYPES : byte {
            None,
            Melee,
            Ranged,
            Magic,
            Throwing,
            Minion,
            All,
            Rogue,

            //insert here

            NUMBER_OF_IDs, //leave this last
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Treated like readonly ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public static PowerScaling[] POWER_SCALING_LOOKUP { get; private set; }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Populate Lookup ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        static PowerScaling() {
            POWER_SCALING_LOOKUP = new PowerScaling[(byte)POWER_SCALING_TYPES.NUMBER_OF_IDs];

            byte id_byte;
            string name;
            float melee, ranged, magic, throwing, minion;

            for (POWER_SCALING_TYPES id = 0; id < POWER_SCALING_TYPES.NUMBER_OF_IDs; id++) {
                id_byte = (byte)id;

                //defaults
                name = "";
                melee = 0f;
                ranged = 0f;
                magic = 0f;
                throwing = 0f;
                minion = 0f;

                switch (id) {
                    case POWER_SCALING_TYPES.None:
                        name = "None";
                        break;

                    case POWER_SCALING_TYPES.Melee:
                        name = "Melee";
                        melee = 1f;
                        break;

                    case POWER_SCALING_TYPES.Ranged:
                        name = "Ranged";
                        ranged = 1f;
                        break;

                    case POWER_SCALING_TYPES.Magic:
                        name = "Magic";
                        magic = 1f;
                        break;

                    case POWER_SCALING_TYPES.Throwing:
                        name = "Throwing";
                        throwing = 1f;
                        break;

                    case POWER_SCALING_TYPES.Minion:
                        name = "Minion";
                        minion = 1f;
                        break;

                    case POWER_SCALING_TYPES.All:
                        name = "Melee, Ranged, Magic, Throwing, Minion";
                        melee = 1f;
                        ranged = 1f;
                        magic = 1f;
                        throwing = 1f;
                        minion = 1f;
                        break;

                    case POWER_SCALING_TYPES.Rogue:
                        name = "Melee, Throwing";
                        melee = 1f;
                        throwing = 1f;
                        break;
                }

                POWER_SCALING_LOOKUP[id_byte] = new PowerScaling(id_byte, name, melee, ranged, magic, throwing, minion);
            }
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Instance ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public byte ID { get; private set; }
        public string Name { get; private set; }
        public float Melee { get; private set; }
        public float Ranged { get; private set; }
        public float Magic { get; private set; }
        public float Throwing { get; private set; }
        public float Minion { get; private set; }

        public PowerScaling(byte id, string name, float melee, float ranged, float magic, float throwing, float minion) {
            ID = id;
            Name = name;
            Melee = melee;
            Ranged = ranged;
            Magic = magic;
            Throwing = throwing;
            Minion = minion;
        }
    }
}