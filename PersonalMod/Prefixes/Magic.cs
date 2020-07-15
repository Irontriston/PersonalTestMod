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
	class MagicPrefix : ModPrefix
	{
		internal static List<byte> MagicPrefixes = new List<byte>();
		internal float DamageMult;
		internal float KnockbackMult;
		internal float ShootSpeedMult;
		internal float ManaMult;
		internal int CritChanceMod;
		public override PrefixCategory Category { get {return PrefixCategory.Magic; } }

		public MagicPrefix() { }
		public MagicPrefix(float DamageMult = 1.0f, float KnockbackMult = 1.0f, float ShootSpeedMult = 1.0f, float ManaMult = 1.0f, int CritChanceMod = 0)
		{
			this.DamageMult = DamageMult;
			this.KnockbackMult = KnockbackMult;
			this.ShootSpeedMult = ShootSpeedMult;
			this.ManaMult = ManaMult;
			this.CritChanceMod = CritChanceMod;
		}
		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				MagePrefixes(mod, MagicPrefixTypes.Lunarbound    , 1.125f , 1.0625f , 1.06f  , 0.875f , 3  );
				MagePrefixes(mod, MagicPrefixTypes.Spacial       , 1.2f   , 1.125f  , 1.13f  , 0.625f , 8  );
				MagePrefixes(mod, MagicPrefixTypes.Photobolstered, 1.325f , 1.2f    , 1.2f   , 0.25f  , 13 );
				MagePrefixes(mod, MagicPrefixTypes.Quantum       , 1.625f , 1.333f  , 1.333f , 0.025f , 20 );
			}

			return false;
		}
		public override float RollChance(Item item) { return 3f; }
		public override bool CanRoll(Item item)
		{
			if ( item.magic == true ) return true;
			else return false;
		}
		public override void ModifyValue(ref float valueMult) { valueMult *= 1; }

		public override void SetStats(ref float DamageMult, ref float KnockbackMult, ref float InvAttackSpeedMult, ref float ItemSizeMult, ref float ShootSpeedMult, ref float ManaMult, ref int CritChanceMod)
		{
			DamageMult = this.DamageMult;
			KnockbackMult = this.KnockbackMult;
			ShootSpeedMult = this.ShootSpeedMult;
			ManaMult = this.ManaMult;
			CritChanceMod = this.CritChanceMod;
		}
		static void MagePrefixes(Mod mod, MagicPrefixTypes prefixType, float DamageMult = 1.0f, float KnockbackMult = 1.0f, float ShootSpeedMult = 1.0f, float ManaMult = 1.0f, int CritChanceMod = 0)
		{
			mod.AddPrefix(prefixType.ToString(), new MagicPrefix(DamageMult, KnockbackMult, ShootSpeedMult, ManaMult, CritChanceMod));
			MagicPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
		}
	}
	public enum MagicPrefixTypes : byte
	{
		None,
		Lunarbound,
		Spacial,
		Photobolstered,
		Quantum
	}
}
