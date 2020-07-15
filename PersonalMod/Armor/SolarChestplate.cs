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
    [AutoloadEquip(EquipType.Body)]
    class SolarChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Chestplate");
            Tooltip.SetDefault("A chestplate made from the essence of the stars."
                + "\nIncreases maximum health by 300 and regen by 25");
        }
        public override void SetDefaults()
        {
            item.width = 35;
            item.height = 33;
            item.lifeRegen = 25;
            item.defense = 200;
            item.value = 10000;
            item.rare = ItemRarityID.Cyan;
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 300;
            Lighting.AddLight(player.position, 1.25f, 0.52f, 0f);
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
