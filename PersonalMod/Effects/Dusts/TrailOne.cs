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
            dust.scale = 1.0f;
            dust.alpha = 50;
        }
        public override bool Update(Dust dust)
		{
			if (dust.customData != null && dust.customData is NPC)
			{
				NPC nPC2 = (NPC)dust.customData;
				dust.position += nPC2.position - nPC2.oldPosition;
				if (dust.noGravity)
				{
					dust.velocity *= 1.02f;
				}
				dust.alpha -= 70;
				if (dust.alpha < 0)
				{
					dust.alpha = 0;
				}
				dust.scale *= 0.95f;
				if (dust.scale <= 0.01f)
				{
					dust.active = false;
				}
			}
			else if (dust.noGravity)
			{
				dust.velocity *= 1.025f;
				dust.scale += 0.025f;
				dust.alpha += 5;
				if (dust.alpha > 255)
				{
					dust.active = false;
				}
			}
			return false;
        }
    }
}
