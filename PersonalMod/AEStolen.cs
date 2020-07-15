using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Terraria.ID;
using PersonalMod;

namespace DictionaryOfPrefixes
{
    public class AEStolen : GlobalItem
    {

        public static GameCulture ActiveCulture
        {
            get
            {
                return LanguageManager.Instance.ActiveCulture;
            }
        }
        public int rangedDamage;
        public int minionDamage;
        public int meleeDamage;
        public int magicDamage;
        public int meleeCrit;
        public int rangedCrit;
        public int magicCrit;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            AEStolen myClone = (AEStolen)base.Clone(item, itemClone);

            myClone.rangedDamage = rangedDamage;
            myClone.minionDamage = minionDamage;
            myClone.meleeDamage = meleeDamage;
            myClone.magicDamage = magicDamage;
            myClone.meleeCrit = meleeCrit;
            myClone.rangedCrit = rangedCrit;
            myClone.magicCrit = magicCrit;
            return myClone;
        }
        public override bool NewPreReforge(Item item)
        {

            rangedDamage = 0;
            minionDamage = 0;
            meleeDamage = 0;
            magicDamage = 0;
            meleeCrit = 0;
            rangedCrit = 0;
            magicCrit = 0;
            return base.NewPreReforge(item);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ActiveCulture == GameCulture.Polish)
            {
                if (rangedDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "rangedDamage", "+" + rangedDamage + "% obrażenia dystansowe");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (minionDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "minionDamage", "+" + minionDamage + "% obrażenia przywołanej istoty");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (meleeDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "meleeDamage", "+" + meleeDamage + "% obrażenia w walce w zwarciu");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (magicDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "magicDamage", "+" + magicDamage + "% obrażenia magiczne");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (meleeCrit > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "meleeCrit", "+" + meleeCrit + "% szansa na trafienie krytyczne w walce w zwarciu");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (rangedCrit > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "rangedCrit", "+" + rangedCrit + "% szansa na dystansowe trafienie krytyczne");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (magicCrit > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "magicCrit", "+" + magicCrit + "% szansa na magiczne trafienie krytyczne");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (rangedDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "rangedDamage", rangedDamage + "% obrażenia dystansowe");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
                if (minionDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "minionDamage", minionDamage + "% obrażenia przywołanej istoty");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
                if (meleeDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "meleeDamage", meleeDamage + "% obrażenia w walce w zwarciu");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
                if (magicDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "magicDamage", magicDamage + "% obrażenia magiczne");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
            }
            else
            {
                if (rangedDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "rangedDamage", "+" + rangedDamage + "% ranged damage");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (minionDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "minionDamage", "+" + minionDamage + "% summon damage");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (meleeDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "meleeDamage", "+" + meleeDamage + "% melee damage");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (magicDamage > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "magicDamage", "+" + magicDamage + "% magic damage");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (meleeCrit > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "meleeCrit", "+" + meleeCrit + "% melee critical strike chance");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (rangedCrit > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "rangedCrit", "+" + rangedCrit + "% ranged critical strike chance");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (magicCrit > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "magicCrit", "+" + magicCrit + "% magic critical strike chance");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
                if (rangedDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "rangedDamage", rangedDamage + "% ranged damage");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
                if (minionDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "minionDamage", minionDamage + "% summon damage");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
                if (meleeDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "meleeDamage", meleeDamage + "% melee damage");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
                if (magicDamage < 0)
                {
                    TooltipLine line = new TooltipLine(mod, "magicDamage", magicDamage + "% magic damage");
                    line.isModifier = true;
                    line.isModifierBad = true;
                    tooltips.Add(line);
                }
            }
        }
    
        public override void UpdateEquip(Item item, Player player)
        {
            if (item.prefix > 0)
            {

                player.rangedDamage += rangedDamage * .01f;
                player.minionDamage += minionDamage * .01f;
                player.meleeDamage += meleeDamage * .01f;
                player.magicDamage += magicDamage * .01f;
                player.meleeCrit += meleeCrit;
                player.rangedCrit += rangedCrit;
                player.magicCrit += magicCrit;
            }
        }
        public override void NetSend(Item item, BinaryWriter writer)
        {

            writer.Write(rangedDamage);
            writer.Write(minionDamage);
            writer.Write(meleeDamage);
            writer.Write(magicDamage);
            writer.Write(meleeCrit);
            writer.Write(rangedCrit);
            writer.Write(magicCrit);

        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            rangedDamage = reader.ReadInt32();
            minionDamage = reader.ReadInt32();
            meleeDamage = reader.ReadInt32();
            magicDamage = reader.ReadInt32();
            meleeCrit = reader.ReadInt32();
            rangedCrit = reader.ReadInt32();
            magicCrit = reader.ReadInt32();

        }
    }
}