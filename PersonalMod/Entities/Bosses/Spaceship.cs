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

namespace PersonalMod.Entities.Bosses
{
    [AutoloadBossHead]
    class Spaceship : ModNPC
    {
        private int ai;
        private int attacktimer;

        private int frame = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alien Ship");
            Main.npcFrameCount[npc.type] = 5;
        }

        public override void SetDefaults()
        {
            npc.boss = true; //makes this.. a boss
            npc.aiStyle = -1; //custom AI
            npc.npcSlots = 4f; // How many npcs the boss counts as

            npc.width = 194;
            npc.height = 102;
            npc.lifeMax = 500000; //Fairly Obvious
            npc.damage = 200; //FO
            npc.defense = 200; //FO
            npc.value = Item.buyPrice(1,0,0,0);
            npc.knockBackResist = 0f; //Knockback multiplier, so 0f = no knockback

            npc.lavaImmune = true; //FO
            npc.noTileCollide = true; //FO
            npc.noGravity = true; // FO
            music = MusicID.Boss3; //What music you want playing during the fight
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.2f);
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 Target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;

            npc.rotation = 0f;
            npc.netAlways = true;
            if(npc.life >= npc.lifeMax)
            {
                npc.life = npc.lifeMax;
            }

            if(npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.velocity.Y += -0.125f;
                if(npc.timeLeft > 100)
                {
                    npc.timeLeft = 100;
                    return;
                }
            }

            //ai comes in
            ai++;
            npc.ai[0] = ai * 1f;

            //animation


            //Movement
            npc.TargetClosest(faceTarget: false);
            Vector2 Distance = player.Center - npc.Center + new Vector2(0f, -100f);
            if (Distance.Length() > 20f)
            {
                Vector2 desiredVelocity = Vector2.Normalize(Distance - npc.velocity) * 8f;
                Vector2 velocity = npc.velocity;
                npc.SimpleFlyMovement(desiredVelocity, 0.5f);
                npc.velocity = Vector2.Lerp(npc.velocity, velocity, 0.5f);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;

            if (npc.frameCounter < 10)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 30)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 40)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else if (npc.frameCounter < 50)
            {
                npc.frame.Y = 4 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
    }
}
