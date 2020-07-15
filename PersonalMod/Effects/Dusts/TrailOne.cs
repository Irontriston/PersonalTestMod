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
    class TrailOne : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 0.875f;
            dust.alpha = 50;
        }
        public override bool Update(Dust dust)
        {
            dust.rotation = dust.velocity.X;
            dust.scale *= 0.9f;
            if (dust.scale < 0.25f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
