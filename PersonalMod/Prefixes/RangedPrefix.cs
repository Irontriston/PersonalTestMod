using System.Collections.Generic;
using Terraria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace DictionaryOfPrefixes.Prefixes
{
    public class RangedPrefix : ModPrefix
    {
        internal static List<byte> RangedPrefixes = new List<byte>();
        internal int critBonus = 0;
        internal float damageMult = 1f;
        internal float knockbackMult = 1f;
        internal float shootSpeedMult = 1f;
        internal float useTimeMult = 1f;

        public override PrefixCategory Category { get { return PrefixCategory.Ranged; } }


        public RangedPrefix() { }

        public RangedPrefix(float damageMult = 1f, float knockbackMult = 1f, float useTimeMult = 1f, int critBonus = 0, float shootSpeedMult = 1f)
        {
            this.damageMult = damageMult;
            this.knockbackMult = knockbackMult;
            this.useTimeMult = useTimeMult;
            this.critBonus = critBonus;
            this.shootSpeedMult = shootSpeedMult;
        }

        public override bool Autoload(ref string name)
        {
            if (base.Autoload(ref name))
            {
                AddRangedPrefix(mod, RangedPrefixType.Accurate, 1, 1, 1, 2, 1.15f);
                AddRangedPrefix(mod, RangedPrefixType.Careless, 1, 1, 0.85f, 0, 0.7f);
                AddRangedPrefix(mod, RangedPrefixType.Jittery, 0.75f, 1, 0.9f, 0, 1.1f);
                AddRangedPrefix(mod, RangedPrefixType.Offhanded, 0.85f, 1, 1f, 5, 1.1f);
                AddRangedPrefix(mod, RangedPrefixType.Pumped, 1.18f, 1, 1.1f, 5, 1f);
                AddRangedPrefix(mod, RangedPrefixType.Roguish, 1.0f, 1, 0.85f, 5, 1f);
                AddRangedPrefix(mod, RangedPrefixType.Exhausted, 1.0f, 0.95f, 1.2f, 0, 0.95f);
            }
            return false;
        }

        public override void ModifyValue(ref float valueMult) { valueMult *= 1; }

        public override bool CanRoll(Item item) { return true; }

        public override float RollChance(Item item) { return 1f; }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = this.damageMult;
            knockbackMult = this.knockbackMult;
            useTimeMult = this.useTimeMult;
            critBonus = this.critBonus;
            shootSpeedMult = this.shootSpeedMult;
        }

        static void AddRangedPrefix(Mod mod, RangedPrefixType prefixType, float damageMult = 1f, float knockbackMult = 1f, float useTimeMult = 1f, int critBonus = 0, float shootSpeedMult = 1f)
        {
            mod.AddPrefix(prefixType.ToString(), new RangedPrefix(damageMult, knockbackMult, useTimeMult, critBonus, shootSpeedMult));
            RangedPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
        }
    }

    public enum RangedPrefixType : byte
    {
        None,
        Accurate,
        Careless,
        Jittery,
        Offhanded,
        Pumped,
        Roguish,
        Exhausted
    }
}
