using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Models;

namespace swlsimNET.ServerApp.Spells
{
    public class Passive : Spell
    {
        public override SpellType SpellType { get; set; } = SpellType.Passive;
        public List<Type> SpellTypes { get; set; } = new List<Type>();
        public List<Passive> SpecificSpellTypes { get; set; } = new List<Passive>();
        public bool SpecificWeaponTypeBonus { get; set; }
        public double BaseDamageModifier { get; set; }
        public double BaseDamageCritModifier { get; set; }
        public bool BonusSpellOnlyOnCrit { get; set; }
        public bool ModelledInWeapon { get; set; }

        public virtual void Init(IPlayer player){}

        public void LoopSpellsFromPassive(IPlayer player)
        {
            // Get spells that are modified by this passive
            foreach (var spellType in SpellTypes)
            {
                // Since this is APL there can be several instances of each spell. Modify them all.
                var spells = player.Spells.Where(s => s.GetType() == spellType);

                foreach (var spell in spells)
                {
                    ModifySpellWithPassive(spell);
                }
            }
        }

        public void ModifySpellWithPassive(ISpell spell)
        {
            // To not add passive bonus spell stats onto the spell itself
            if (PassiveBonusSpell != null)
            {
                spell.PassiveBonusSpell = PassiveBonusSpell;
                return;
            }

            // ISpell members
            spell.PrimaryGimmickCost += PrimaryGimmickCost;
            spell.SecondaryGimmickCost += SecondaryGimmickCost;
            spell.PrimaryGimmickGain += PrimaryGimmickGain;
            spell.SecondaryGimmickGain += SecondaryGimmickGain;
            spell.PrimaryGimmickGainOnCrit += PrimaryGimmickGainOnCrit;
            spell.BaseDamage += BaseDamage;
            spell.BaseDamageCrit += BaseDamageCrit;
            spell.PrimaryCost += PrimaryCost;
            spell.SecondaryCost += SecondaryCost;
            spell.PrimaryGain += PrimaryGain;
            spell.SecondaryGain += SecondaryGain;
            spell.MaxCooldown += MaxCooldown;
            spell.CastTime += CastTime;
            spell.ChannelTicks += ChannelTicks;
            spell.BonusCritChance += BonusCritChance;
            spell.BonusCritPower += BonusCritPower;
            //spell.PassiveBonusSpell = PassiveBonusSpell;

            // Passive specials
            spell.BaseDamage *= (1 + BaseDamageModifier);
            spell.BaseDamageCrit *= (1 + BaseDamageCritModifier);
        }
    }
}
