using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PersonalMod.Effects.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Projectiles
{
    class MilitaryBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Military Bullet");
        }
        public override void SetDefaults()
        {
            projectile.width = 7;
            projectile.height = 5;
            projectile.aiStyle = 0;
            projectile.timeLeft = 600;
            projectile.penetrate = 1;
            projectile.damage = 125;

            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = false;
            projectile.arrow = false;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI/2);
            projectile.ai[0] += 1f;
            for(int trail = 0; trail < 5; trail++)
            {
                int airtrail = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustType<BulletTrail>(), 0f, 0f, 0, default(Color), 1f);
                Dust dust = Main.dust[airtrail];
            }
        }
    }
}
