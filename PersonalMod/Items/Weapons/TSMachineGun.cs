using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PersonalMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.Items.Weapons
{
	public class TSMachineGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Techno-Solara Arm Gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A former arm of the Techno-Solara."
				+ "\n Has a 90% chance to not consume ammo.");
		}
		public override void SetDefaults()
		{
			item.width = 62;
			item.height = 20;
			item.rare = ItemRarityID.Purple;
			item.value = Item.sellPrice(platinum: 1);

			item.useTime = 2;
			item.useAnimation = 3;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item14;

			item.noMelee = true;
			item.ranged = true;
			item.damage = 750;
			item.knockBack = 0.5f;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 100f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool ConsumeAmmo(Player player)
        {
			return Main.rand.NextFloat() > .9f;
        }
	}
}