﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace ExperienceAndClasses.Systems {
    public class Resource {
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constants (and readonly) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        public enum IDs : byte {


            NUMBER_OF_IDs, //leave this second to last
            NONE, //leave this last
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Instance Fields ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        private readonly string Internal_Name;

        public readonly IDs ID;
        public readonly ushort ID_num;

        public readonly Color Colour;

        public Texture2D Texture { get; protected set; }

        public string Name { get { return Language.GetTextValue("Mods.ExperienceAndClasses.Common.Resource_" + Internal_Name + "_Name"); } }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Instance Variables ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public float Value { get; protected set; } = 0f;
        public float Capacity { get; protected set; } = 1f;

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constructor ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public Resource(IDs id, PlayerClass.IDs class_id, byte level) {
            ID = id;
            ID_num = (ushort)id;
            Internal_Name = Enum.GetName(typeof(IDs), ID_num);
            Colour = PlayerClass.LOOKUP[(byte)class_id].Colour;
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Public Instance Methods ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        /// <summary>
        /// Run once during init
        /// </summary>
        public void LoadTexture() {
            //TODO
        }


    }
}