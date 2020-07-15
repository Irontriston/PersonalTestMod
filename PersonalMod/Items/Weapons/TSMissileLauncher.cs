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
				+ "\nHas a 75% chance to not consume ammo.");
		}
		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 21;
			item.rare = ItemRarityID.Purple;
			item.value = Item.sellPrice(platinum: 1);

			item.useTime = 15;
			item.useAnimation = 16;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item11;

			item.noMelee = true;
			item.ranged = true;
			item.damage = 500;
			item.knockBack = 0.5f;
            item.useAmmo = 771;          /*AmmoIDRocket*/
			item.shoot = 134;            //RocketI, since vanilla Rocket Launcher uses this.
			item.shootSpeed = 20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 40);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
