using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells
{
    public interface ISpell
    {
        string Name { get; }
        double BaseDamage { get; set; }
        decimal DotDuration { get; set; }
        double DotExpirationBaseDamage { get; set; }
        double BaseDamageCrit { get; set; }
        WeaponType WeaponType { get; }
        SpellType SpellType { get; }
        AbilityType AbilityType { get; }
        ElementalType ElementalType { get; }
        int PrimaryCost { get; set; }
        int SecondaryCost { get; set; }
        int PrimaryGain { get; set; }
        int SecondaryGain { get; set; }
        int PrimaryGimmickCost { get; set; }
        int PrimaryGimmickReduce { get; set; }
        int SecondaryGimmickCost { get; set; }
        int SecondaryGimmickReduce { get; set; }
        int PrimaryGimmickGain { get; set; }
        int SecondaryGimmickGain { get; set; }
        int PrimaryGimmickRequirement { get; set; }
        int PrimaryGimmickGainOnCrit { get; set; }
        decimal MaxCooldown { get; set; }
        decimal Cooldown { get; set; }
        decimal CastTime { get; set; }
        int ChannelTicks { get; set; }
        int DotTicks { get; set; }
        double BonusCritChance { get; set; }
        double BonusCritPower { get; set; }
        string Args { get; }
        Passive PassiveBonusSpell { get; set; }
        AbilityBuff AbilityBuff { get; set; }
        Attack Execute(Player player);
        bool CanExecute(Player player);
    }
}