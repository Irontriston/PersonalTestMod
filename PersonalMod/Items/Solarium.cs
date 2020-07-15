using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PersonalMod.Items
{
	public class Solarium : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solarium"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("As the essence of the stars, it's potential is near limitless.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Cyan;
			item.value = Item.sellPrice(gold: 5);
			item.maxStack = 999;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 5);
			recipe.AddIngredient(ItemID.FragmentSolar);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}
	}
}