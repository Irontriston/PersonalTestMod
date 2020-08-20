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
		internal float DamageMult = 1f;
		internal float KnockbackMult = 1f;
		internal float InvAttackSpeedMult = 1f;
		internal float ShootSpeedMult = 1f;
		public override PrefixCategory Category { get { return PrefixCategory.Ranged; } }
		public RangedPrefix() { }
		public RangedPrefix(float DamageMult = 1.0f, float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ShootSpeedMult = 1.0f)
		{
			this.DamageMult = DamageMult;
			this.KnockbackMult = KnockbackMult;
			this.InvAttackSpeedMult = InvAttackSpeedMult;
			this.ShootSpeedMult = ShootSpeedMult;
		}
		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				AddRangedPrefix(mod, RangedPrefixTypes.LunarShelled       ,1.0625f,1.166f,0.875f,1.05f );
				AddRangedPrefix(mod, RangedPrefixTypes.SpaciallyBound     ,1.125f ,1.25f ,0.666f,1.125f);
				AddRangedPrefix(mod, RangedPrefixTypes.SolShelled         ,1.25f  ,1.333f,0.5f  ,1.2f  );
				AddRangedPrefix(mod, RangedPrefixTypes.TimeSpaciallyBound,1.5f   ,1.5f   ,0.333f,1.333f);
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
		}
		static void AddRangedPrefix(Mod mod, RangedPrefixTypes prefixType, float DamageMult = 1.0f, float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ShootSpeedMult = 1.0f)
		{
			mod.AddPrefix(prefixType.ToString(), new MagicPrefix(DamageMult, KnockbackMult, InvAttackSpeedMult, ShootSpeedMult));
			RangedPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
		}
	}
	public enum RangedPrefixTypes : byte
	{
		None,
		LunarShelled,
		SpaciallyBound,
		SolShelled,
		TimeSpaciallyBound
	}
}

