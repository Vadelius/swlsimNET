using System.Collections.Generic;
using swlSimulator.api.Combat;
using swlSimulator.api.Spells;
using swlSimulator.api.Weapons;
using swlSimulator.api.Models;
using swlSimulator.Models;

namespace swlSimulator.api.Models
{
    public interface IPlayer
    {
        Settings Settings { get; }
        double CombatPower { get; }
        double GlanceReduction { get; } 
        double CriticalChance { get; }
        double CritPower { get; }
        double BasicSignetBoost { get; }
        double PowerSignetBoost { get; }
        double EliteSignetBoost { get; }
        decimal Interval { get; }
        decimal CurrentTimeSec { get; }

        List<ISpell> Spells { get; }
        List<IBuff> Buffs { get; }
        List<IBuff> AbilityBuffs { get; }

        IBuff GetBuffFromName(string name);
        IBuff GetAbilityBuffFromName(string name);
        bool HasPassive(string name);
        Passive GetPassive(string name);

        Weapon PrimaryWeapon { get; }
        Weapon SecondaryWeapon { get; }

        Weapon GetWeaponFromSpell(ISpell spell);
        Weapon GetOtherWeaponFromSpell(ISpell spell);
        Weapon GetWeaponFromType(WeaponType wtype);

        decimal GetWeaponResourceFromType(WeaponType wtype);

        void AddBonusAttack(RoundResult rr, ISpell spell);
    }
}