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

namespace PersonalMod.Prefixes
{
    class RangedPrefix : ModPrefix
	{
		internal static List<byte> RangedPrefixes = new List<byte>();
		internal float DamageMult;
		internal float KnockbackMult;
		internal float InvAttackSpeedMult;
		internal float ShootSpeedMult;
		internal int CritChanceMod;
		public override PrefixCategory Category { get { return PrefixCategory.Ranged; } }
		public RangedPrefix() { }
		public RangedPrefix(float DamageMult = 1.0f, float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ShootSpeedMult = 1.0f, int CritChanceMod = 0)
		{
			this.DamageMult = DamageMult;
			this.KnockbackMult = KnockbackMult;
			this.InvAttackSpeedMult = InvAttackSpeedMult;
			this.ShootSpeedMult = ShootSpeedMult;
			this.CritChanceMod = CritChanceMod;
		}
		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				AddRangedPrefix(mod, RangedPrefixTypes.Lunar_Shelled       ,1.0625f,1.166f,0.875f,1.05f ,3 );
				AddRangedPrefix(mod, RangedPrefixTypes.Spacially_Bound     ,1.125f ,1.25f ,0.666f,1.125f,8 );
				AddRangedPrefix(mod, RangedPrefixTypes.Sol_Shelled         ,1.25f  ,1.333f,0.5f  ,1.2f  ,13);
				AddRangedPrefix(mod, RangedPrefixTypes.Time_Spacially_Bound,1.5f   ,1.5f  ,0.333f,1.333f,20);
			}

			return false;
		}
		public override float RollChance(Item item) { return 2f; }
		public override bool CanRoll(Item item) { return true; }
		public override void ModifyValue(ref float valueMult) { valueMult *= 1; }

		public override void SetStats(ref float DamageMult, ref float KnockbackMult, ref float InvAttackSpeedMult, ref float ItemSizeMult, ref float ShootSpeedMult, ref float ManaMult, ref int CritChanceMod)
		{
			DamageMult = this.DamageMult;
			KnockbackMult = this.KnockbackMult;
			InvAttackSpeedMult = this.InvAttackSpeedMult;
			ShootSpeedMult = this.ShootSpeedMult;
			CritChanceMod = this.CritChanceMod;
		}
		static void AddRangedPrefix(Mod mod, RangedPrefixTypes prefixType, float DamageMult = 1.0f, float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ShootSpeedMult = 1.0f, int CritChanceMod = 0)
		{
			mod.AddPrefix(prefixType.ToString(), new MagicPrefix(DamageMult, KnockbackMult, InvAttackSpeedMult, ShootSpeedMult, CritChanceMod));
			RangedPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
		}
	}
	public enum RangedPrefixTypes : byte
	{
		None,
		Lunar_Shelled,
		Spacially_Bound,
		Sol_Shelled,
		Time_Spacially_Bound
	}
}

