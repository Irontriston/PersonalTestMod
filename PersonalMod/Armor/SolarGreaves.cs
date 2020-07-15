using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    class SolarGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Greaves");
            Tooltip.SetDefault("Greaves made from the essence of the stars."
                + "\nIncreases movement by 150%.");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 14;
            item.defense = 175;
            item.value = 10000;
            item.rare = ItemRarityID.Cyan;
            Lighting.AddLight(item.position, 1.0f, 0.4f, 0f);
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 2.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Solarium"), 35);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}