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
	public class SolarFlare : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Flare");
		}
        public override void SetDefaults()
        {
            projectile.width = 19;
            projectile.height = 45;
            projectile.aiStyle = 0;
            projectile.timeLeft = 300;
            projectile.penetrate = 5;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }
        public override void AI()
		{
			Lighting.AddLight(projectile.position, 2.0f, 0.5f, 0f);
            projectile.rotation = (float)projectile.velocity.ToRotation() + (float)(MathHelper.PiOver2);
            float SpeedAlter = Main.rand.NextFloat(0.875f, 1.125f);
            Vector2 DustSpeed = new Vector2(projectile.velocity.X / Main.rand.NextFloat(3.75f,6.25f), projectile.velocity.Y / Main.rand.NextFloat(3.75f, 6.25f));

            for (int n = 0; n < 1; n++)
            {
                int t1 = Dust.NewDust(projectile.Center, 1, 1, DustType<TrailOne>(), DustSpeed.X, DustSpeed.Y, 50, default, 1f);
                Main.dust[t1].velocity *= SpeedAlter;
                Main.dust[t1].rotation = projectile.rotation;
                Main.dust[t1].noGravity = true;
            }

        }
    }
}