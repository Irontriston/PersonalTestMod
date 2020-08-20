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

namespace PersonalMod.VanillaCopycats
{
    class LuminArrowCons : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Arrow Copycat");
		}
		public override void SetDefaults()
        {
			item.shootSpeed = 0.5f;
			item.shoot = mod.ProjectileType("LuminArrow");
			item.damage = 50;
			item.width = 16;
			item.height = 40;
			item.maxStack = 999;
			item.consumable = true;
			item.ammo = AmmoID.Arrow;
			item.knockBack = 3.5f;
			item.ranged = true;
			item.rare = ItemRarityID.Cyan;
			item.value = Item.sellPrice(0, 0, 50, 0);
		}
		public override void AddRecipes()
		{

			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 111);
			recipe.AddRecipe();
		}
	}
	class LuminArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Arrow Copycat");
		}
		public override void SetDefaults()
		{
			projectile.arrow = true;
			projectile.width = 12;
			projectile.height = 32;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.MaxUpdates = 3;
			projectile.timeLeft = projectile.MaxUpdates * 45;
			projectile.ignoreWater = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.alpha = 255;
			projectile.penetrate = 5;
		}
			
		public override void AI()
		{

			if (projectile.localAI[0] == 0f && projectile.localAI[1] == 0f)
			{
				projectile.localAI[0] = projectile.Center.X;
				projectile.localAI[1] = projectile.Center.Y;
				projectile.ai[0] = projectile.velocity.X;
				projectile.ai[1] = projectile.velocity.Y;
			}
			projectile.alpha -= 25;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}

		}
		public override void Kill(int timeLeft)
		{
			projectile.timeLeft = 0;
			int num192 = Main.rand.Next(5,10);
			for (int num193 = 0; num193 < num192; num193++)
			{
				int num194 = Dust.NewDust(projectile.Center, 0, 0, 220, 0f, 0f, 100, default(Color), 0.5f);
				Dust dust15 = Main.dust[num194];
				Dust dust226 = dust15;
				dust226.velocity *= 1.6f;
				Main.dust[num194].velocity.Y -= 1f;
				Main.dust[num194].position = Vector2.Lerp(Main.dust[num194].position, projectile.Center, 0.5f);
				Main.dust[num194].noGravity = true;
			}
		}
    }
}
