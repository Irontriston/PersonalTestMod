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

namespace PersonalMod.Items.Weapons
{
	public class SolarSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Solar Sword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A sword forged from the essence of the stars, it rains down swordlike projectiles from the heavens.");
		}

		public override void SetDefaults() 
		{
			item.melee = true;
			item.useTurn = true;
			item.autoReuse = true;

			item.damage = 750;
			item.crit = 25;
			item.width = 40;
			item.height = 40;
			item.useTime = 5;
			item.useAnimation = 10;
			item.shoot = mod.ProjectileType("SolarFlare");

			item.shootSpeed = 5f;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3.33f;

			item.value = Item.sellPrice(gold: 75);
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			int spawnNumber = Main.rand.Next(2,4);
			for(int n = 0; n < spawnNumber; n++)
			{
				Vector2 PlayerPos = player.MountedCenter;
				float XRange = Main.rand.NextFloat(Main.screenWidth/4,Main.screenWidth-Main.screenWidth/4);
				float YRange = (Main.rand.NextFloat(10,15)*16);
				Vector2 SpawnRange =Main.MouseWorld + new Vector2 ( XRange,-YRange);

				Vector2 Distance = SpawnRange - Main.MouseWorld;
				float Speed_Normalizer = NeededProperties.Util_Properties.DistanceFloat(Main.MouseWorld, SpawnRange)/160;
				Vector2 Dist_Speed_Alter = Distance/40;
				Vector2 Velocity_Perturber = new Vector2 (Main.rand.NextFloat(-0.3f, 0.3f)*16, Main.rand.NextFloat(-0.3f, 0.3f)*16);
				Vector2 VelocitySpectrum = ((Main.MouseWorld - SpawnRange)/Main.rand.NextFloat(62.5f, 68.75f)) + Velocity_Perturber;

				Projectile.NewProjectile(SpawnRange, VelocitySpectrum / Speed_Normalizer - Dist_Speed_Alter, mod.ProjectileType("SolarFlare"), item.damage*5, item.knockBack, player.whoAmI);
			}
			return false;
        }

        public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 20);
			recipe.AddIngredient(ItemID.StarWrath);
			recipe.AddIngredient(ItemID.Starfury);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
	}
}