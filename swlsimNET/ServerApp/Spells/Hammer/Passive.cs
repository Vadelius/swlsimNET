using System;
using System.Collections.Generic;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Hammer
{
    public class Shatter : Passive
    {
        public Shatter()
        {
            WeaponType = WeaponType.Hammer;
            SpellTypes.Add(typeof(RazorShards));
            SpellTypes.Add(typeof(RazorShardsRage));
            BaseDamageModifier = 0.15;
            // Razor Shards: 6% BaseDamage stacks 5 times on hit
            // TODO: Make it possible to apply onhit bonuses
        }
    }

    public class Outrage : Passive
    {
        public Outrage()
        {
            WeaponType = WeaponType.Hammer;
            SpellTypes.Add(typeof(Seethe));
            PrimaryGimmickGain = 38;
            // Using Seethe generating 38 Rage
        }
    }

    public class Obliterate : Passive
    {
        public Obliterate()
        {
            WeaponType = WeaponType.Hammer;
            SpellTypes.Add(typeof(Demolish));
            SpellTypes.Add(typeof(DemolishRage));
            BaseDamageCritModifier = 0.35;
            // +35% Demolish BaseDamage on crit
        }
    }

    public class Annihilate : Passive
    {
        public Annihilate()
        {
            WeaponType = WeaponType.Hammer;
            SpellTypes.Add(typeof(EruptionRage));
            BaseDamage = 7.12; // Damage increased to 12.72 * CombatPower Instead of 5.6 Base..
            PrimaryGimmickCost = 50; // Total of 50
            PrimaryGimmickGain = -7;
        }
    }

    public class LetLoose : Passive
    {
        public LetLoose()
        {
            WeaponType = WeaponType.Hammer;
            ModelledInWeapon = true;
            // if using "Rampage" causes you to become Enraged increase base damage,
            // Demolish: 30%, Eruption with Annihilate Passive: 20%
        }
    }

    public class Beatdown : Spell
    {
        public Beatdown(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            SpellType = SpellType.Passive;
            // Blindside: Successfully interrupt 6s 75% generate 1 energy for the weapon used.
        }
    }

    public class UnbridledWrath : Passive
    {
        // Enrage bonus effects increased:
        // Razor Shards DoT: 15%
        // Demolish: 8%
        // Pulverise max health: 50%
        // Burning Wrath ground AoE: 14%
        // Eruption damage with Annihilate Passive: 5.5%

        public UnbridledWrath()
        {
            WeaponType = WeaponType.Hammer;

            SpecificSpellTypes.Add(new Passive
            {
                SpellTypes = new List<Type> { typeof(RazorShardsRage) },
                BaseDamageModifier = 0.15
            });

            SpecificSpellTypes.Add(new Passive
            {
                SpellTypes = new List<Type> { typeof(DemolishRage) },
                BaseDamageModifier = 0.08
            });
        }

        public override void Init(IPlayer player)
        {
            if(player.HasPassive(nameof(Annihilate)))
            {
                SpecificSpellTypes.Add(new Passive
                {
                    SpellTypes = new List<Type> { typeof(EruptionRage) },
                    BaseDamageModifier = 0.055
                });                
            }

            base.Init(player);
        }
    }

    public class FastAndFurious : Passive
    {
        public FastAndFurious()
        {
            WeaponType = WeaponType.Hammer;
            BaseDamageModifier = 0.09;
            ModelledInWeapon = true;
            // 25% Movement speed, 9% Hammer damage 3.5s after becoming Enraged
        }
    }

    public class Berserker : Passive
    {
        public Berserker()
        {
            WeaponType = WeaponType.Hammer;
            SpecificWeaponTypeBonus = true;
            PrimaryGimmickGainOnCrit = 4;
            // Crit hits with Hammer grants 4 extra Rage
        }
    }

    public class BluntForceTrauma : Spell
    {
        public BluntForceTrauma(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            SpellType = SpellType.Passive;
            // Attack 4 times with Hammer 10% Crit BaseDamage 3s
        }
    }

    public class AngerManagement : Spell
    {
        public AngerManagement(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Whenever you use a Hammer consumer, your next Hammer consumer generates 2 extra rage for every second, up to a maximum of 5
        }
    }
}