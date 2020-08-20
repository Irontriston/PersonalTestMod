using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Items.Tools
{
	class SolarWarhammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar War-Hammer");
			Tooltip.SetDefault("A hammer forged with the essense of stars to annihilate its wielder's foes.");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
			item.melee = true;
			item.useTurn = true;
			item.autoReuse = true;

			item.width = 40;
			item.height = 40;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;

			item.value = 10000;
			item.value = Item.sellPrice(platinum: 1);
			item.rare = ItemRarityID.Cyan;
			item.hammer = 400;
			item.UseSound = SoundID.Item1;
			item.tileBoost += 8;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 21);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
