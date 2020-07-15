using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Items.Weapons
{
	public class SolarSword : ModItem
	{
		int SS;
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Solar Sword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A sword forged from the essence of the stars, it flings swordlike projectiles towards its foes.");
		}

		public override void SetDefaults() 
		{
			item.melee = true;
			item.melee = true;
			item.useTurn = true;
			item.autoReuse = true;

			item.damage = 1250;
			item.crit = 10;
			item.width = 40;
			item.height = 40;
			item.useTime = 15;
			item.useAnimation = 18;

			item.shoot = mod.ProjectileType("SolarFlare");
			item.shootSpeed = 25f;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5f;

			item.value = 10000;
			item.value = Item.sellPrice(gold: 75);
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
			item.noUseGraphic = true;
			item.noMelee = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}