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
using static System.Drawing.Graphics;
using static Terraria.ModLoader.ModContent;
using Steamworks;
using System.Drawing.Design;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace PersonalMod.Projectiles
{
    class ZenithProj : ModProjectile
    {
        private float Path_Alter;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenith Projectile");
        }
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
            projectile.localNPCHitCooldown = 10;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.manualDirectionChange = true;
        }
        public override bool? Colliding(Rectangle myRect, Rectangle targetRect)
        {
            Player player = Main.player[projectile.owner];
            Vector2 PlayerCenter = player.MountedCenter;
            Vector2 CursorPos = Main.MouseWorld;
            float Distance = Math.Abs((float)Math.Sqrt(((CursorPos.X - PlayerCenter.X) * (CursorPos.X - PlayerCenter.X)) + ((CursorPos.Y - PlayerCenter.Y) * (CursorPos.Y - PlayerCenter.Y))));
            Rectangle _lanceHitboxBounds = new Rectangle(0, 0, (int)Distance, (int)(Distance / 3));

            float collisionPoint = 0f;
            float scaleFactor = 40f;
            for (int i = 14; i < projectile.oldPos.Length; i += 15)
            {
                float LocalaiValue = projectile.localAI[0] - (float)i;
                if (!(LocalaiValue < 0f) && !(LocalaiValue > 60f))
                {
                    Vector2 Rot_Center = projectile.oldPos[i] + projectile.Size / 2f;
                    Vector2 End_Position = (projectile.oldRot[i] + (float)Math.PI / 2f).ToRotationVector2();
                    _lanceHitboxBounds.X = (int)Rot_Center.X - _lanceHitboxBounds.Width / 2;
                    _lanceHitboxBounds.Y = (int)Rot_Center.Y - _lanceHitboxBounds.Height / 2;
                    if (_lanceHitboxBounds.Intersects(targetRect) && Collision.CheckAABBvLineCollision(targetRect.TopLeft(), targetRect.Size(), Rot_Center - End_Position * scaleFactor, Rot_Center + End_Position * scaleFactor, 20f, ref collisionPoint))
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
            Vector2 PlayerCenter = player.Center + new Vector2(-player.width / 4, -player.height / 1.5f);
            Vector2 CursorPos = Main.MouseWorld;

            Vector2 RotSlope = CursorPos - PlayerCenter;

            if (projectile.ai[0] <= 180 && projectile.alpha >= 0)
            {
                projectile.alpha -= 255 / 10;
            }


            float Distance = (float)Math.Abs(Math.Sqrt(Math.Pow(CursorPos.X - PlayerCenter.X, 2)
                                                     + Math.Pow(CursorPos.Y - PlayerCenter.Y, 2)));

            float Pi = (float)Math.PI;
            float Degrees = projectile.ai[0];
            float Radians = (Degrees * (Pi / 180));
            float Radius = (Distance/2);


            Vector2 PathBound_Center = new Vector2((CursorPos.X + PlayerCenter.X) / 2, (CursorPos.Y + PlayerCenter.Y) / 2);
            Vector2 Position = PathBound_Center - new Vector2((int)(Math.Cos(Radians)*Radius) - projectile.width/2
                                                             ,(int)(Math.Sin(Radians)*Radius/4) - projectile.height/2);

            projectile.position = NeededProperties.Util_Properties.RotTransform(PlayerCenter,RotSlope,Position);

            projectile.rotation = Radians-(Pi/2);
            if (projectile.ai[0] >= 360)
            {
                projectile.position = PlayerCenter;
                projectile.velocity = new Vector2(0,0);
                projectile.rotation = -(Pi/2) + Radians;
            }
            if (projectile.ai[0] >= 420)
            {
                projectile.Kill();
            }
            projectile.ai[0] += 8;

            //--------------------------------------------------------------------------------Dividing line
            /*//Making player variable "p" set as the projectile's owner
            Player p = Main.player[projectile.owner];

            //Factors for calculations
            double deg = (double)projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 64; //Distance away from the player

            //Position the player based on where the player is, the Sin/Cos of the angle times the
            //distance for the desired distance away from the player minus the projectile's width
            //and height divided by two so the center of the projectile is at the right place.
            projectile.position = p.Center - new Vector2((int)(Math.Cos(rad) * dist) - projectile.width / 2
                                                        ,(int)(Math.Sin(rad) * dist) - projectile.height / 2);

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            projectile.ai[1] += 1f;*/
        }
    }
}
