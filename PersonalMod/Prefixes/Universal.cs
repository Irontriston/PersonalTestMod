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
	class UniversalPrefix : ModPrefix
	{
		internal static List<byte> UniversalPrefixes = new List<byte>();
		internal float DamageMult;
		internal float KnockbackMult;
		internal float InvAttackSpeedMult;
		internal float ShootSpeedMult;
		internal int CritChanceMod;
		public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }

		public UniversalPrefix() { }

		public UniversalPrefix(float DamageMult = 1.0f, float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ShootSpeedMult = 1.0f, int CritChanceMod = 0)
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
				AddUniversalPrefix(mod, UniversalPrefixTypes.Orbital         ,1.1f ,1.1f  ,0.9f  ,1.1f, 13);
				AddUniversalPrefix(mod, UniversalPrefixTypes.Extraterrestrial,1.25f,1.2f  ,0.75f ,1.2f, 25);
				AddUniversalPrefix(mod, UniversalPrefixTypes.Imperial        ,1.45f,1.333f,0.55f ,1.3f, 38);
				AddUniversalPrefix(mod, UniversalPrefixTypes.ArchGodly       ,1.75f,1.5f  ,0.333f,1.4f, 50);
			}
			return false;
		}
		public override float RollChance(Item item) => 4f;
		public override bool CanRoll(Item item) {return true;}
		public override void ModifyValue(ref float valueMult) { valueMult *= 1; }

		public override void SetStats(ref float DamageMult, ref float KnockbackMult, ref float InvAttackSpeedMult, ref float ItemSizeMult, ref float ShootSpeedMult, ref float ManaMult, ref int CritChanceMod)
		{
			DamageMult = this.DamageMult;
			KnockbackMult = this.KnockbackMult;
			InvAttackSpeedMult = this.InvAttackSpeedMult;
			ShootSpeedMult = this.ShootSpeedMult;
			CritChanceMod = this.CritChanceMod;
		}
		static void AddUniversalPrefix(Mod mod, UniversalPrefixTypes prefixType, float DamageMult = 1.0f, float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ShootSpeedMult = 1.0f , int CritChanceMod = 0)
		{
			mod.AddPrefix(prefixType.ToString(), new UniversalPrefix(DamageMult, KnockbackMult, InvAttackSpeedMult,ShootSpeedMult , CritChanceMod));
			UniversalPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
		}
	}
	public enum UniversalPrefixTypes : byte
	{
		None,
		Orbital,
		Extraterrestrial,
		Imperial,
		ArchGodly,
	}
}
