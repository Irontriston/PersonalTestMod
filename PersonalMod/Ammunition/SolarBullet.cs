using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Ammunition
{
    public class SolarBullet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 8;
            item.height = 8;
            item.value = Item.sellPrice(silver: 10);
            item.rare = ItemRarityID.Red;

            item.consumable = true;
            item.maxStack = 999;
            item.ranged = true;
            item.damage = 250;
            item.knockBack = 0.5f;

            item.shoot = mod.ProjectileType("SolarProjectile");
            item.shootSpeed = 75f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Solarium"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}