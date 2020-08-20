using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Effects.Dusts
{
    class BulletTrail : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 1.0f;
            dust.alpha = 50;
        }
        public override bool Update(Dust dust)
        {
            dust.alpha += 5;
            dust.scale *= 1.025f;
            if(dust.alpha >= 200 && dust.alpha <= 255)
            {
                dust.alpha += 1;
                dust.scale *= 1.01f;
            }
            if(dust.alpha >= 255)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
