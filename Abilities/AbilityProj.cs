﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExperienceAndClasses.Abilities
{
    public class AbilityProj
    {
        public class Proj_HealHurt : ProjNoVisual
        {
            //heal/hurt a player/npc
            //projectile.damage is the magnitude (+ for heal, - for hurt)
            //projectile.ai[0] is the mode
            //projectile.ai[1] is the target index
            //uses projectile knockback

            public override void AI()
            {
                bool is_player = projectile.ai[0] != 0;
                int target = (int)projectile.ai[1];
                int amount = projectile.damage;
                int direction = 1;

                Player owner = Main.player[projectile.owner];

                if (owner.active && !owner.dead)
                {
                    if (is_player)
                    {
                        //player
                        Player player = Main.player[target];
                        if (player.active && !player.dead)
                        {
                            if (amount > 0)
                            {
                                int amount_valid = player.statLifeMax2 - player.statLife;
                                if (amount > amount_valid)
                                {
                                    amount = amount_valid;
                                }
                                player.HealEffect(amount);
                                player.statLife += amount;
                            }
                            else if (amount < 0)
                            {
                                amount *= -1;
                                if (projectile.Center.X > player.Center.X)
                                {
                                    direction = -1;
                                }
                                player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByPlayer(projectile.owner), amount, direction, true);
                            }
                        }
                    }
                    else
                    {
                        //npc
                        NPC npc = Main.npc[target];
                        if (npc.active)
                        {
                            if (amount > 0)
                            {
                                int amount_valid = npc.lifeMax - npc.life;
                                if (amount > amount_valid)
                                {
                                    amount = amount_valid;
                                }
                                npc.HealEffect(amount);
                                npc.life += amount;
                            }
                            else if (amount < 0)
                            {
                                amount *= -1;
                                if (projectile.Center.X > npc.Center.X)
                                {
                                    direction = -1;
                                }
                                owner.ApplyDamageToNPC(npc, amount, projectile.knockBack, direction, false);
                            }
                        }
                    }
                }

                //done
                projectile.Kill();
            }

        }

        //////public abstract class HomingProj : ModProjectile
        //////{
        //////    public enum MODES : byte
        //////    {
        //////        POSITION,
        //////        NPC,
        //////        PLAYER,
        //////    }

        //////    public Vector2 target;
        //////    public float velocity_adjust = 0.5f;
        //////    public float velocity_max = 5f;
        //////    public bool initialized = false;
        //////    public bool direct = false;
        //////    public int targetIndex = 0;
        //////    public MODES mode = MODES.POSITION;

        //////    public override void AI()
        //////    {
        //////        if (!initialized)
        //////        {
        //////            if (mode == MODES.POSITION)
        //////            {
        //////                target = new Vector2(projectile.ai[0], projectile.ai[1]);
        //////            }
        //////            else
        //////            {
        //////                targetIndex = (int)projectile.ai[0];
        //////            }
        //////            initialized = true;
        //////        }
        //////        else
        //////        {
        //////            if (mode == MODES.NPC)
        //////            {
        //////                NPC npc = Main.npc[targetIndex];
        //////                if (npc.active)
        //////                {
        //////                    target = npc.position;
        //////                }
        //////                else
        //////                {
        //////                    projectile.Kill();
        //////                    return;
        //////                }
        //////            }
        //////            else if (mode == MODES.PLAYER)
        //////            {
        //////                Player player = Main.player[targetIndex];
        //////                if (player.active)
        //////                {
        //////                    target = player.position;
        //////                }
        //////                else
        //////                {
        //////                    projectile.Kill();
        //////                    return;
        //////                }
        //////            }

        //////            HomeOnto(projectile, target, velocity_adjust, velocity_max, direct);
        //////        }
        //////    }
        //////}

        //////public static void HomeOnto(Projectile projectile, Vector2 target, float velocity_adjust, float velocity_max, bool direct)
        //////{
        //////    if (!direct)
        //////    {
        //////        int direction;

        //////        direction = projectile.position.X < target.X ? 1 : -1;
        //////        projectile.velocity.X += direction * velocity_adjust;
        //////        if (projectile.velocity.X > velocity_max) projectile.velocity.X = velocity_max;
        //////        if (projectile.velocity.X < -velocity_max) projectile.velocity.X = -velocity_max;

        //////        direction = projectile.position.Y < target.Y ? 1 : -1;
        //////        projectile.velocity.Y += direction * velocity_adjust;
        //////        if (projectile.velocity.Y > velocity_max) projectile.velocity.Y = velocity_max;
        //////        if (projectile.velocity.Y < -velocity_max) projectile.velocity.Y = -velocity_max;
        //////    }
        //////    else
        //////    {
        //////        Vector2 d = projectile.DirectionTo(target);
        //////        projectile.velocity.X = d.X / (1 / velocity_max);
        //////        projectile.velocity.Y = d.Y / (1 / velocity_max);
        //////    }
        //////}
    }

    public class DustMakerProj : ProjNoVisual
    {
        public enum MODE : byte
        {
            ability_cast,
            heal,
        }

        public override void AI()
        {
            //setup
            Player player = Main.player[projectile.owner];

            //creates ability on-use dust effect, shows for all clients but dust scatter is unqiue for each client
            switch ((MODE)projectile.ai[0])
            {
                case MODE.ability_cast:
                    SpreadDust(player.Center, ExperienceAndClasses.mod.DustType<Dusts.Dust_AbilityGeneric>(), 3, 5, 2, 150, AbilityMain.COLOUR_CLASS_TYPE[(int)projectile.ai[1]]);
                    break;
                case MODE.heal:
                    SpreadDust(projectile.position, DustID.AncientLight, 10, AbilityMain.Cleric_Active_Heal.range/6, 3, 150, Color.Red, true, true);
                    break;
                default:
                    break;
            }

            //done
            projectile.Kill();
        }

        private static void SpreadDust(Vector2 position, int dust_type, int loop_count, float velocity, float scale = 1, int alpha = 255, Color colour = default(Color), bool remove_gravity = false, bool remove_light = false)
        {
            //Math.Cos and Math.Sin were crashing for some reason so a better implementation was not possible
            //Should handle large values of loop_count fairly well
            //Total number of dusts is ((loop_count * 4) - 4), velocities form a circle
            if (loop_count < 3)
            {
                loop_count = 3;
            }
            float inc = 2f / (loop_count - 1);
            int dust_index;
            float velocity_adjust, velocity_x, velocity_y;
            for (float step = -1; step <= +1; step += inc)
            {
                for (int mode = 1; mode <= 4; mode++)
                {
                    //4 directions
                    switch (mode)
                    {
                        case 1:
                            velocity_x = step;
                            velocity_y = -1;
                            break;
                        case 2:
                            velocity_x = step;
                            velocity_y = 1;
                            break;
                        case 3:
                            velocity_x = 1;
                            velocity_y = step;
                            break;
                        case 4:
                            velocity_x = -1;
                            velocity_y = step;
                            break;
                        default:
                            velocity_x = 1;
                            velocity_y = 1;
                            break;
                    }

                    //adjust for distance (so it is a circle instead of a square)
                    velocity_adjust = velocity / (float)Math.Sqrt(Math.Pow(velocity_x, 2) + Math.Pow(velocity_y, 2));
                    velocity_x *= velocity_adjust;
                    velocity_y *= velocity_adjust;

                    //do
                    dust_index = Dust.NewDust(position, 0, 0, dust_type, velocity_x, velocity_y, alpha, colour, scale);
                    if (remove_gravity)
                    {
                        Main.dust[dust_index].noGravity = true;
                        Main.dust[dust_index].velocity = new Vector2(velocity_x, velocity_y); //needs to be reset when setting gravity
                    }
                    if (remove_light)
                    {
                        Main.dust[dust_index].noLight = true;
                    }


                }
            }
        }
    }

    public abstract class ProjNoVisual : ModProjectile
    {
        public override string Texture
        {
            get
            {
                return mod.Name + "/Abilities/Placeholder";
            }
        }
    }
}