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
	public class SolarFlare : ModProjectile
    {
        private static Rectangle _lanceHitboxBounds = new Rectangle(0, 0, 300, 300);
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Flare");
		}
        public override void SetDefaults()
        {
            projectile.width = 19;
            projectile.height = 45;
            projectile.aiStyle = 182;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.manualDirectionChange = true;
        }
        public override bool? Colliding(Rectangle myRect, Rectangle targetRect)
        {
            float collisionPoint = 0f;
            float scaleFactor = 40f;
            for (int i = 14; i < projectile.oldPos.Length; i += 15)
            {
                float aiValue = projectile.localAI[0] - (float)i;
                if (!(aiValue < 0f) && !(aiValue > 60f))
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
            Vector2 mountedCenter = player.MountedCenter;
            float lerpValue = NeededProperties.Util_Properties.GetLerpValue(900f, 0f, projectile.velocity.Length() * 2f, clamped: true);
            float num = MathHelper.Lerp(0.7f, 2f, lerpValue);
            projectile.localAI[0] += num;
            if (projectile.localAI[0] >= 120f)
            {
                projectile.Kill();
                return;
            }

            float lerpValue2 = NeededProperties.Util_Properties.GetLerpValue(0f, 1f, projectile.localAI[0] / 60f, clamped: true);
            float aiValue = projectile.localAI[0] / 60f;
            float AIValue = projectile.ai[0];
            float RotateValue = projectile.velocity.ToRotation();
            float Pi = (float)Math.PI;
            float num6 = (projectile.velocity.X > 0f) ? 1 : (-1);
            float num7 = Pi + num6 * lerpValue2 * ((float)Math.PI * 2f);
            float num8 = projectile.velocity.Length() + NeededProperties.Util_Properties.GetLerpValue(0.5f, 1f, lerpValue2, clamped: true) * 40f;
           
            float MinimalLimit = 60f;
            if (num8 < MinimalLimit)
            {
                num8 = MinimalLimit;
            }

            Vector2 value = mountedCenter + projectile.velocity;
            Vector2 spinningpoint = new Vector2(1f, 0f).RotatedBy(num7) * new Vector2(num8, AIValue * MathHelper.Lerp(2f, 1f, lerpValue));
            Vector2 value2 = value + spinningpoint.RotatedBy(RotateValue);
            Vector2 value3 = (1f - NeededProperties.Util_Properties.GetLerpValue(0f, 0.5f, lerpValue2, clamped: true)) * new Vector2((float)((projectile.velocity.X > 0f) ? 1 : (-1)) * (0f - num8) * 0.1f, (0f - projectile.ai[0]) * 0.3f);
            
            float num10 = num7 + RotateValue;
            projectile.rotation = num10 + (float)Math.PI / 2f;
            projectile.Center = value2 + value3;
            projectile.spriteDirection = (projectile.direction = ((projectile.velocity.X > 0f) ? 1 : (-1)));
            if (AIValue < 0f)
            {
                projectile.rotation = Pi + num6 * lerpValue2 * ((float)Math.PI * -2f) + RotateValue;
                projectile.rotation += (float)Math.PI / 2f;
                projectile.spriteDirection = (projectile.direction = ((!(projectile.velocity.X > 0f)) ? 1 : (-1)));
            }
            if (aiValue < 1f)
            {
                NeededProperties.ZenithHelper.ZenithProfileType zenithProfile = NeededProperties.ZenithHelper.GetZenithProfileType((int)projectile.ai[1]);
                Vector2 value4 = (projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
                Vector2 center = projectile.Center;
                int num11 = 1 + (int)(projectile.velocity.Length() / 100f);
                num11 = (int)((float)num11 * NeededProperties.Util_Properties.GetLerpValue(0f, 0.5f, lerpValue2, clamped: true) * NeededProperties.Util_Properties.GetLerpValue(1f, 0.5f, lerpValue2, clamped: true));
                if (num11 < 1)
                {
                    num11 = 1;
                }
                for (int i = 0; i < num11; i++)
                {
                    zenithProfile.dustMethod(center + value4 * zenithProfile.trailWidth * MathHelper.Lerp(0.5f, 1f, Main.rand.NextFloat()), projectile.rotation - (float)Math.PI / 2f + (float)Math.PI / 2f * (float)projectile.spriteDirection, player.velocity);
                }
                Vector3 vector = zenithProfile.trailColor.ToVector3();
                Vector3 value5 = Vector3.Lerp(Vector3.One, vector, 0.7f);
                Lighting.AddLight(projectile.Center, vector * 0.5f * projectile.Opacity);
                Lighting.AddLight(mountedCenter, value5 * projectile.Opacity * 0.15f);
            }
            projectile.Opacity = NeededProperties.Util_Properties.GetLerpValue(0f, 5f, projectile.localAI[0], clamped: true) * NeededProperties.Util_Properties.GetLerpValue(120f, 115f, projectile.localAI[0], clamped: true);
        }
    }
}