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
    class TSShotgun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Techno-Solara Arm Gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A former arm of the Techno-Solara."
				+ "\nHas a 33.3% chance to not consume ammo.");
		}
		public override void SetDefaults()
		{
			item.width = 60;
			item.height = 20;
			item.rare = ItemRarityID.Purple;
			item.value = Item.sellPrice(platinum: 1);

			item.useTime = 60;
			item.useAnimation = 60;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item14;

			item.noMelee = true;
			item.ranged = true;
			item.damage = 1125;
			item.knockBack = 1.25f;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ProjectileID.PurificationPowder; //Vanilla guns oddly use this.
			item.shootSpeed = 16f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Solarium"), 30);
			recipe.AddIngredient(ItemID.Shotgun, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() > .333f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = Main.rand.Next(6, 12); // 6 to 12 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				float positionChange = Main.rand.NextFloat(-5,5);
				Vector2 newPosition = new Vector2 (position.X + positionChange,position.Y + positionChange);	//Alters beginning position
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8)); //10 degree spread.
				float scale = 1f - (Main.rand.NextFloat(-2, 4) * .1f);										//Randomizes the speed of projectiles.
				perturbedSpeed *= scale;
				Projectile.NewProjectile(newPosition.X, newPosition.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}
	}
}
