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
    class ModZenith : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("New Zenith");// By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A sword that believes itself to be the new Zenith.");
		}
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 56;
			item.height = 56;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.melee = true;
			item.melee = true;
			item.shoot = mod.ProjectileType("FractalProj");
			item.useAnimation = 30;
			item.useTime = item.useAnimation / 3;
			item.shootSpeed = 16f;
			item.damage = 190;
			item.knockBack = 6.5f;
			item.value = Item.sellPrice(0, 50);
			item.crit = 10;
			item.rare = ItemRarityID.Red;
			item.noUseGraphic = true;
			item.noMelee = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperShortsword);
			recipe.AddIngredient(ItemID.EnchantedSword);
			recipe.AddIngredient(ItemID.Seedler);
			recipe.AddIngredient(ItemID.BeeKeeper);
			recipe.AddIngredient(ItemID.Starfury);
			recipe.AddIngredient(ItemID.TerraBlade);
			recipe.AddIngredient(ItemID.TheHorsemansBlade);
			recipe.AddIngredient(ItemID.InfluxWaver);
			recipe.AddIngredient(ItemID.StarWrath);
			recipe.AddIngredient(ItemID.Meowmere);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
