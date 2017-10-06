using System.Collections.Generic;
using swlsimNET.Models;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Models
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
        double Interval { get; }
        double CurrentTimeSec { get; }

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

        double GetWeaponResourceFromType(WeaponType wtype);

        void AddBonusAttack(RoundResult rr, ISpell spell);
    }
}