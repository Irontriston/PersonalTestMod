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
	class MeleePrefix : ModPrefix
	{

		internal static List<byte> MeleePrefixes = new List<byte>();
		internal float DamageMult = 1f;
		internal float InvAttackSpeedMult = 1f;
		internal float KnockbackMult = 1f;
		internal float ItemSizeMult = 1f;
		internal int CritChanceMod = 0;

		// see documentation for vanilla weights and more information
		// note: a weight of 0f can still be rolled. see CanRoll to exclude prefixes.
		// note: if you use PrefixCategory.Custom, actually use ChoosePrefix instead, see ExampleInstancedGlobalItem

		// change your category this way, defaults to Custom
		public override PrefixCategory Category { get { return PrefixCategory.Melee; } }
				

		public MeleePrefix() {}
		
		public MeleePrefix(float DamageMult = 1.0f,  float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ItemSizeMult = 1.0f, int CritChanceMod = 0)
		{
			this.DamageMult = DamageMult;
			this.KnockbackMult = KnockbackMult;
			this.InvAttackSpeedMult = InvAttackSpeedMult;
			this.ItemSizeMult = ItemSizeMult;
			this.CritChanceMod = CritChanceMod;
		}
		// Allow multiple prefix autoloading this way (permutations of the same prefix)
		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				AddMeleePrefix(mod, MeleePrefixTypes.Lunarite    , 1.166f, 1.125f, 0.833f, 1.033f, 6 );
				AddMeleePrefix(mod, MeleePrefixTypes.Celestialite, 1.33f , 1.25f , 0.666f, 1.067f, 13);
				AddMeleePrefix(mod, MeleePrefixTypes.Solaric     , 1.66f , 1.433f, 0.5f  , 1.1f  , 25);
				AddMeleePrefix(mod, MeleePrefixTypes.Galactic    , 2.0f  , 1.625f, 0.333f, 1.167f , 43);
			}

			return false;
		}
		public override float RollChance(Item item) => 2f;
		public override bool CanRoll(Item item)
		{
			if (item.noMelee == true) return false;
			else return true;
		}
		public override void ModifyValue(ref float valueMult) { valueMult *= 1; }

		public override void SetStats(ref float DamageMult, ref float KnockbackMult, ref float InvAttackSpeedMult, ref float ItemSizeMult, ref float ShootSpeedMult, ref float ManaMult, ref int CritChanceMod)
		{
			DamageMult = this.DamageMult;
			KnockbackMult = this.KnockbackMult;
			InvAttackSpeedMult = this.InvAttackSpeedMult;
			ItemSizeMult = this.ItemSizeMult;
			CritChanceMod = this.CritChanceMod;
		}
		static void AddMeleePrefix(Mod mod, MeleePrefixTypes prefixType, float DamageMult = 1.0f, float KnockbackMult = 1.0f, float InvAttackSpeedMult = 1.0f, float ItemSizeMult = 1.0f, int CritChanceMod = 0)
		{
			mod.AddPrefix(prefixType.ToString(), new MeleePrefix(DamageMult, KnockbackMult, InvAttackSpeedMult, ItemSizeMult, CritChanceMod));
			MeleePrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
		}
	}
	public enum MeleePrefixTypes : byte
	{
		None,
		Lunarite,
		Celestialite,
		Solaric,
		Galactic,
	}

}
