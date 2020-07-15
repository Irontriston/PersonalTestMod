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
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.arrow = false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 5; i++)
            {
                int Bounce  = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("TrailOne"), 0f, 0f, 100, default(Color), 2f);
                Main.dust[Bounce].noGravity = true;
                Main.dust[Bounce].velocity *= 5f;
                
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

            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 20)
            {
                projectile.alpha = 255;
                projectile.velocity = new Vector2 (0, 0);
                projectile.position = projectile.Center;
                projectile.width = 125;
                projectile.height = 125;
                projectile.Center = projectile.position;

                projectile.penetrate = -1;
                projectile.damage = 1000;

                for (int i = 0; i < 5; i++)
                {
                    int Expl = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), (int)projectile.width, (int)projectile.height, mod.DustType("TrailOne"), 0f, 0f, 50, default(Color), 1.25f);
                    Main.dust[Expl].noGravity = true;
                    Main.dust[Expl].velocity *= 0f;

                }
            }


            Lighting.AddLight(projectile.position, 1.5f, 0.375f, 0f);
            for (int num163 = 0; num163 < 2; num163++)
            {
                float x2 = projectile.position.X - projectile.velocity.X / 10f * (float)num163;
                float y2 = projectile.position.Y - projectile.velocity.Y / 10f * (float)num163;
                int Trail = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustType<TrailOne>(), 0f, 0f, 0, default, 1f);
                Main.dust[Trail].velocity *= 0f;
                Main.dust[Trail].noGravity = true;
            }



            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }

            Vector2 move = Vector2.Zero;
            float distance = 1000f;
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
                projectile.velocity = (13.5f * projectile.velocity + move) / 13.75f;
                AdjustMagnitude(ref projectile.velocity);
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 37.5f)
            {
                vector *= 37.5f / magnitude;
            }
        }
    }
}