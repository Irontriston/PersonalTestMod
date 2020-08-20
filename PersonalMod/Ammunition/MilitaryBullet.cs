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

namespace PersonalMod.Ammunition
{
    class MilitaryBullet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 7;
            item.maxStack = 999;

            item.damage = 200;
            item.knockBack = 0.25f;
            item.shootSpeed = 16f;

            item.consumable = true;
            item.ranged = true;

            item.shoot = mod.ProjectileType("MilitaryBullet");
            item.ammo = AmmoID.Bullet;
            item.value = Item.sellPrice(silver: 20);
            item.rare = ItemRarityID.Red;
        }
    }
}
