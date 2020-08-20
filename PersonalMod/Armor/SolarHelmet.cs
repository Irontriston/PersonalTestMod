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
    [AutoloadEquip(EquipType.Head)]
    class SolarHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Helmet");
            Tooltip.SetDefault("A headpiece made with the essence of the stars."
                + "\nIncreases damage dealt by 75% and mana regen by 20."
                + "\nGives the wearer a 50% chance to not consume ammo.");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 16;
            item.defense = 163;
            item.value = 10000;
            item.rare = ItemRarityID.Cyan;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SolarChestplate>() && legs.type == ItemType<SolarGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.manaRegen += 40;
            player.statLifeMax2 *= 4;
            player.statManaMax2 *= 3;
            item.lifeRegen += 25;
        }

        public override void UpdateEquip(Player player)
        {
            Lighting.AddLight(player.position, 1.0f, 0.4f, 0f);
            player.allDamageMult += 0.75f;
            player.manaRegen += 20;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > 0.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Solarium"), 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
