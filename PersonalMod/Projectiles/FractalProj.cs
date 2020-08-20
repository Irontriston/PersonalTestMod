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

namespace PersonalMod.Projectiles
{
	class FractalProj : ModProjectile
	{
		private bool noEnchantmentVisuals;
		private static Rectangle _lanceHitboxBounds = new Rectangle(0, 0, 300, 300);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zenith Projectile");
		}
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.alpha = 0;
			projectile.extraUpdates = 1;
			projectile.usesLocalNPCImmunity = true;
			projectile.manualDirectionChange = true;
			projectile.localNPCHitCooldown = 10;
			projectile.penetrate = -1;
			noEnchantmentVisuals = true;
		}
		public override bool? Colliding(Rectangle myRect, Rectangle targetRect)
		{

			float collisionPoint = 0f;
			float scaleFactor = 40f;
			for (int i = 14; i < projectile.oldPos.Length; i += 15)
			{
				float num2 = projectile.localAI[0] - (float)i;
				if (!(num2 < 0f) && !(num2 > 60f))
				{
					Vector2 value2 = projectile.oldPos[i] + projectile.Size / 2f;
					Vector2 value3 = (projectile.oldRot[i] + (float)Math.PI / 2f).ToRotationVector2();
					_lanceHitboxBounds.X = (int)value2.X - _lanceHitboxBounds.Width / 2;
					_lanceHitboxBounds.Y = (int)value2.Y - _lanceHitboxBounds.Height / 2;
					if (_lanceHitboxBounds.Intersects(targetRect) && Collision.CheckAABBvLineCollision(targetRect.TopLeft(), targetRect.Size(), value2 - value3 * scaleFactor, value2 + value3 * scaleFactor, 20f, ref collisionPoint))
					{
						return true;
					}
				}
			}
			Vector2 value4 = (projectile.rotation + (float)Math.PI / 2f).ToRotationVector2();
			_lanceHitboxBounds.X = (int)projectile.position.X - _lanceHitboxBounds.Width / 2;
			_lanceHitboxBounds.Y = (int)projectile.position.Y - _lanceHitboxBounds.Height / 2;
			if (_lanceHitboxBounds.Intersects(targetRect) && Collision.CheckAABBvLineCollision(targetRect.TopLeft(), targetRect.Size(), projectile.Center - value4 * scaleFactor, projectile.Center + value4 * scaleFactor, 20f, ref collisionPoint))
			{
				return true;
			}
			return false;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			Vector2 PlayerCenter = player.MountedCenter;
			Vector2 CursorPos = Main.MouseWorld;

			Vector2 Distance = CursorPos - PlayerCenter;
			float DistLenth = (float)Math.Abs(Math.Sqrt(((CursorPos.X - PlayerCenter.X) * (CursorPos.X - PlayerCenter.X)) + ((CursorPos.Y - PlayerCenter.Y) * (CursorPos.Y - PlayerCenter.Y))));
			float DistAlter = 0.33f;

			float lerpValue = NeededProperties.Util_Properties.GetLerpValue(500f, 0f, projectile.velocity.Length() * 2f, clamped: true);
			float AI_Increment = MathHelper.Lerp(0.7f, 2f, lerpValue);
			projectile.localAI[0] += AI_Increment;
			if (projectile.localAI[0] >= 120f)
			{
				projectile.Kill();
				return;
			}

			float LocalaiValue = projectile.localAI[0] / 60f;
			float lerpValue2 = NeededProperties.Util_Properties.GetLerpValue(0f, 1f, LocalaiValue, clamped: true);
			float BaseSpeed = projectile.ai[0];
			float RotValue = projectile.velocity.ToRotation();
			float Pi = (float)Math.PI;
			float Orbital_Return_Percentage = (projectile.velocity.X > 0f) ? 1.0f : (-1.0f); //if proj. vel. > 0, then  vertical : horizontal  return coverage
			float Orbit_Manager = Pi + Orbital_Return_Percentage * lerpValue2 * (Pi*2f);
			float ReturnPos = projectile.velocity.Length() + NeededProperties.Util_Properties.GetLerpValue(0.5f, 1.0f, lerpValue2, clamped: true) * 40f;
			float Min = 60f;
			if (ReturnPos < Min)
			{
				ReturnPos = Min;
			}
			Vector2 Center_Velocity = PlayerCenter + projectile.velocity;
			Vector2 spinningpoint = new Vector2(1f, 0.5f).RotatedBy(Orbit_Manager) * new Vector2(ReturnPos, BaseSpeed * MathHelper.Lerp(2f, 1f, lerpValue));
			Vector2 Rot_Center = Center_Velocity + spinningpoint.RotatedBy(RotValue);
			Vector2 End_Position = (1f - NeededProperties.Util_Properties.GetLerpValue(0f, 0.5f, lerpValue2, clamped: true)) * new Vector2((float)((projectile.velocity.X > 0f) ? 1f : (-1f)) * (0f - ReturnPos) * 0.1f, (0f - projectile.ai[0]) * 0.3f);
			float num10 = Orbit_Manager + RotValue;
			projectile.rotation = num10 + Pi / 2f;

			projectile.Center = Rot_Center + End_Position;
			projectile.spriteDirection = (projectile.direction = ((projectile.velocity.X > 0f) ? 1 : (-1)));
			projectile.localAI[1] += AI_Increment;
			if (BaseSpeed < 0f)
			{
				projectile.rotation = Pi + Orbital_Return_Percentage * lerpValue2 * (Pi * -2f) + RotValue;
				projectile.rotation += Pi / 2f;
				projectile.spriteDirection = (projectile.direction = ((!(projectile.velocity.X > 0f)) ? 1 : (-1)));
			}
			if (LocalaiValue < 1f)
			{
				NeededProperties.ZenithHelper.ZenithProfileType zenithProfile = NeededProperties.ZenithHelper.GetZenithProfile((int)projectile.ai[1]);
				Vector2 value4 = (projectile.rotation - Pi / 2f).ToRotationVector2();
				Vector2 center = projectile.Center;
				int AI_Increment11 = 1 + (int)(projectile.velocity.Length() / 100f);
				AI_Increment11 = (int)((float)AI_Increment11 * NeededProperties.Util_Properties.GetLerpValue(0f, 0.5f, lerpValue2, clamped: true) * NeededProperties.Util_Properties.GetLerpValue(1f, 0.5f, lerpValue2, clamped: true));
				if (AI_Increment11 < 1)
				{
					AI_Increment11 = 1;
				}
				for (int i = 0; i < AI_Increment11; i++)
				{
					zenithProfile.dustMethod(center + value4 * zenithProfile.trailWidth * MathHelper.Lerp(0.5f, 1f, Main.rand.NextFloat()), projectile.rotation - Pi / 2f + Pi / 2f * (float)projectile.spriteDirection, player.velocity);
				}
				Vector3 vector = zenithProfile.trailColor.ToVector3();
				Vector3 value5 = Vector3.Lerp(Vector3.One, vector, 0.7f);
				Lighting.AddLight(projectile.Center, vector * 0.5f * projectile.Opacity);
				Lighting.AddLight(PlayerCenter, value5 * projectile.Opacity * 0.15f);
			}

			projectile.Opacity = NeededProperties.Util_Properties.GetLerpValue(0f, 5f, projectile.localAI[0], clamped: true) * NeededProperties.Util_Properties.GetLerpValue(120f, 115f, projectile.localAI[0], clamped: true);


			if (!projectile.active)
			{
				return;
			}
			projectile.numUpdates = projectile.extraUpdates;
			while (projectile.numUpdates >= 0)
			{
				if (projectile.position.X <= Main.leftWorld || projectile.position.X + (float)projectile.width >= Main.rightWorld || projectile.position.Y <= Main.topWorld || projectile.position.Y + (float)projectile.height >= Main.bottomWorld)
				{
					projectile.active = false;
					return;
				}
				if (!noEnchantmentVisuals)
				{
					UpdateEnchantmentVisuals();
				}
			}
		}
		private void UpdateEnchantmentVisuals()
		{
			if (projectile.npcProj)
			{
				return;
			}
			if (Main.player[projectile.owner].frostBurn && (projectile.melee || projectile.ranged) && projectile.friendly && !projectile.hostile && !projectile.noEnchantments && Main.rand.Next(2 * (1 + projectile.extraUpdates)) == 0)
			{
				int Dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
				Main.dust[Dust1].noGravity = true;
				Main.dust[Dust1].velocity *= 0.7f;
				Main.dust[Dust1].velocity.Y -= 0.5f;
			}
			if (projectile.melee && Main.player[projectile.owner].magmaStone && !projectile.noEnchantments && Main.rand.Next(3) != 0)
			{
				int Dust2 = Dust.NewDust(new Vector2(projectile.position.X - 4f, projectile.position.Y - 4f), projectile.width + 8, projectile.height + 8, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[Dust2].scale = 1.5f;
				}
				Main.dust[Dust2].noGravity = true;
				Main.dust[Dust2].velocity.X *= 2f;
				Main.dust[Dust2].velocity.Y *= 2f;
			}
			if (!projectile.melee || Main.player[projectile.owner].meleeEnchant <= 0 || projectile.noEnchantments)
			{
				return;
			}
			if (Main.player[projectile.owner].meleeEnchant == 1 && Main.rand.Next(3) == 0)
			{
				int Dust3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 171, 0f, 0f, 100);
				Main.dust[Dust3].noGravity = true;
				Main.dust[Dust3].fadeIn = 1.5f;
				Main.dust[Dust3].velocity *= 0.25f;
			}
			if (Main.player[projectile.owner].meleeEnchant == 1)
			{
				if (Main.rand.Next(3) == 0)
				{
					int Dust4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 171, 0f, 0f, 100);
					Main.dust[Dust4].noGravity = true;
					Main.dust[Dust4].fadeIn = 1.5f;
					Main.dust[Dust4].velocity *= 0.25f;
				}
			}
			else if (Main.player[projectile.owner].meleeEnchant == 2)
			{
				if (Main.rand.Next(2) == 0)
				{
					int Dust5 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 2.5f);
					Main.dust[Dust5].noGravity = true;
					Main.dust[Dust5].velocity *= 0.7f;
					Main.dust[Dust5].velocity.Y -= 0.5f;
				}
			}
			else if (Main.player[projectile.owner].meleeEnchant == 3)
			{
				if (Main.rand.Next(2) == 0)
				{
					int Dust6 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 2.5f);
					Main.dust[Dust6].noGravity = true;
					Main.dust[Dust6].velocity *= 0.7f;
					Main.dust[Dust6].velocity.Y -= 0.5f;
				}
			}
			else if (Main.player[projectile.owner].meleeEnchant == 4)
			{
				int Dust7 = 0;
				if (Main.rand.Next(2) == 0)
				{
					Dust7 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 1.1f);
					Main.dust[Dust7].noGravity = true;
					Main.dust[Dust7].velocity.X /= 2f;
					Main.dust[Dust7].velocity.Y /= 2f;
				}
			}
			else if (Main.player[projectile.owner].meleeEnchant == 5)
			{
				if (Main.rand.Next(2) == 0)
				{
					int Dust8 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 169, 0f, 0f, 100);
					Main.dust[Dust8].velocity.X += projectile.direction;
					Main.dust[Dust8].velocity.Y += 0.2f;
					Main.dust[Dust8].noGravity = true;
				}
			}
			else if (Main.player[projectile.owner].meleeEnchant == 6)
			{
				if (Main.rand.Next(2) == 0)
				{
					int Dust9 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100);
					Main.dust[Dust9].velocity.X += projectile.direction;
					Main.dust[Dust9].velocity.Y += 0.2f;
					Main.dust[Dust9].noGravity = true;
				}
			}
			else if (Main.player[projectile.owner].meleeEnchant == 7)
			{
				Vector2 velocity = projectile.velocity;
				if (velocity.Length() > 4f)
				{
					velocity *= 4f / velocity.Length();
				}
				if (Main.rand.Next(20) == 0)
				{
					int RanValue1 = Main.rand.Next(139, 143);
					int Dust11 = Dust.NewDust(projectile.position, projectile.width, projectile.height, RanValue1, velocity.X, velocity.Y, 0, default(Color), 1.2f);
					Main.dust[Dust11].velocity.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
					Main.dust[Dust11].velocity.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
					Main.dust[Dust11].velocity.X += (float)Main.rand.Next(-50, 51) * 0.05f;
					Main.dust[Dust11].velocity.Y += (float)Main.rand.Next(-50, 51) * 0.05f;
					Main.dust[Dust11].scale *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
				}
				if (Main.rand.Next(40) == 0)
				{
					int RanValue2 = Main.rand.Next(276, 283);
					int Dust13 = Gore.NewGore(projectile.position, velocity, RanValue2);
					Main.gore[Dust13].velocity.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
					Main.gore[Dust13].velocity.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
					Main.gore[Dust13].scale *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
					Main.gore[Dust13].velocity.X += (float)Main.rand.Next(-50, 51) * 0.05f;
					Main.gore[Dust13].velocity.Y += (float)Main.rand.Next(-50, 51) * 0.05f;
				}
			}
			else if (Main.player[projectile.owner].meleeEnchant == 8 && Main.rand.Next(4) == 0)
			{
				int Dust14 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 46, 0f, 0f, 100);
				Main.dust[Dust14].noGravity = true;
				Main.dust[Dust14].fadeIn = 1.5f;
				Main.dust[Dust14].velocity *= 0.25f;
			}
		}
	}
}
