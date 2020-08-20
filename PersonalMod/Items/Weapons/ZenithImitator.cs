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
    class ZenithImitator : ModItem
	{
		public override void SetStaticDefaults()
		{
			// By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A sword aspiring to be the Zenith.");
		}
		public override void SetDefaults()
		{
		item.melee = true;
		item.melee = true;
		item.useTurn = true;
		item.autoReuse = true;

		item.damage = 190;
		item.crit = 20;
		item.width = 32;
		item.height = 32;
		item.useAnimation = 30;
		item.useTime = item.useAnimation/3;

		item.shoot = mod.ProjectileType("ZenithProj");
		item.shootSpeed = 16f;
		item.useStyle = ItemUseStyleID.SwingThrow;
		item.knockBack = 5f;

		item.value = 10000;
		item.value = Item.sellPrice(gold: 75);
		item.rare = ItemRarityID.Expert;
		item.UseSound = SoundID.Item1;
		item.noUseGraphic = true;
		item.noMelee = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 50);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
