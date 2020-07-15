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
    class SolarPick_Axe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Pick-Axe");
            Tooltip.SetDefault("A Pick-axe forged with the essense of the stars.");
        }
        public override void SetDefaults()
		{
			item.damage = 300;
			item.melee = true;
			item.useTurn = true;
			item.autoReuse = true;

			item.width = 40;
			item.height = 40;
			item.useTime = 4;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;

			item.value = 10000;
			item.value = Item.sellPrice(gold: 75);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.pick = 500;
			item.axe = 100;
			item.tileBoost += 7;
		}
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 18);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
