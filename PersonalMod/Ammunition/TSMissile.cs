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

namespace PersonalMod.Ammunition
{
    class TSMissile : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno-Solaran Missile");
        }
        public override void SetDefaults()
        {
            item.damage = 500;
            item.ranged = true;
            item.width = 20;
            item.height = 14;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2.5f;
            item.value = Item.sellPrice(silver: 50);
            item.rare = ItemRarityID.Cyan;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("TSMissileProj");
            item.ammo = 771; //AmmoID.Rocket
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Solarium"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();
        }
    }
}
