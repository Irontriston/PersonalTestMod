using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PersonalMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Items.Weapons
{
    class TSMissileLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Techno-Solara Missile Launcher"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A former launcher of the Techno-Solara."
				+ "\nLaunches a series of rockets.");
		}
		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 21;
			item.rare = ItemRarityID.Purple;
			item.value = Item.sellPrice(platinum: 1);

			item.useTime = 5;
			item.useAnimation = 45;
			item.reuseDelay = 5;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item11;

			item.noMelee = true;
			item.ranged = true;
			item.damage = 750;
			item.knockBack = 0.5f;
            item.useAmmo = 771;          /*AmmoIDRocket*/
			item.shoot = 134;            //RocketI, since vanilla Rocket Launcher uses this.
			item.shootSpeed = 20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 40);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool ConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 1);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float scale = 1f - (Main.rand.NextFloat(-1, 9f) * .1f);
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
			perturbedSpeed *= scale;
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
}
