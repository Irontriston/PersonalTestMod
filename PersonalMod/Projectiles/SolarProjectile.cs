using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PersonalMod.Effects.Dusts;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Projectiles
{
    public class SolarProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
			ProjectileID.Sets.Homing[projectile.type] = true;
            DisplayName.SetDefault("Solar Bullet");
        }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 12;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.timeLeft = 320;

            projectile.penetrate = 6;
            projectile.extraUpdates = 1;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.arrow = false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 5; i++)
            {
                int Bounce = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("TrailOne"), 0.5f, 0.5f, 100, default(Color), 1f);
                Main.dust[Bounce].noGravity = true;
                Main.dust[Bounce].velocity *= 5f;
                
            }
            if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                projectile.velocity.X = oldVelocity.X * -0.75f;
            }
            if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.75f;
            }
            projectile.penetrate -= 1;
            return false;
        }
        public override void AI()
        {
            if (projectile.owner == Main.myPlayer && projectile.penetrate <= 1 && projectile.width < 100 )
            {
                projectile.timeLeft = 20;
            }

            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 23)
            {
                projectile.alpha = 255;
                projectile.velocity *= 0;
                projectile.position = projectile.Center;
                projectile.width = 125;
                projectile.height = 125;
                projectile.Center = projectile.position;

                projectile.penetrate = -1;
                projectile.damage = 1000;

            }


            Lighting.AddLight(projectile.position, 1.5f, 0.375f, 0f);
            if(projectile.owner == Main.myPlayer && projectile.timeLeft >= 23)
            {
                float SpeedAlter = Main.rand.NextFloat(0.875f, 1.125f);

               for (int num163 = 0; num163 < 1; num163++)
               {
                  float x2 = projectile.position.X - projectile.velocity.X / 10f * (float)num163;
                  float y2 = projectile.position.Y - projectile.velocity.Y / 10f * (float)num163;
                  int Trail = Dust.NewDust(new Vector2(x2, y2), projectile.width, projectile.height, DustType<TrailOne>(), projectile.velocity.X / 10, projectile.velocity.Y / 10, 50, default, 1f);
                  Main.dust[Trail].velocity *= SpeedAlter;
                  Main.dust[Trail].rotation = projectile.rotation;
                  Main.dust[Trail].noGravity = true;
               }
            }


            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }

            Vector2 move = Vector2.Zero;
            float distance = 500f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }

            if (target)
            {
                AdjustMagnitude(ref move);
                projectile.velocity = (13.625f * projectile.velocity + move) / 13.75f;
                AdjustMagnitude(ref projectile.velocity);
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 62.5f)
            {
                vector *= 62.5f / magnitude;
            }
        }
        public override void Kill(int timeLeft)
        {
            float dustSpeed = Main.rand.NextFloat(0.1f, 5f);
            projectile.position = projectile.Center;
            projectile.width = 8;
            projectile.height = 12;
            projectile.Center = projectile.position;

            for (int num854 = 0; num854 < 5; num854++)
            {
                int num855 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustType<TrailOne>(), 0f, 0f, 100, default(Color), 1.5f);
                Dust dust = Main.dust[num855];
                dust.velocity *= 1.4f;
            }
        }
    }
}