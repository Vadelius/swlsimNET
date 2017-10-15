using System.Collections.Generic;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells
{
    public class Buff : IBuff
    {
        public string Name => GetType().Name;
        public decimal MaxDuration { get; set; }
        public decimal Duration { get; set; } = -1;
        public decimal MaxCooldown { get; set; }
        public decimal Cooldown { get; set; }
        public virtual bool Active => Duration > 0;
        public List<decimal> ActivationRounds { get; } = new List<decimal>();
        public List<decimal> DeactivationRounds { get; } = new List<decimal>();

        public double MaxBonusCritChance { get; set; }
        public double MaxBonusCritMultiplier { get; set; }
        public double MaxBonusDamageMultiplier { get; set; }
        public double MaxBonusBaseDamageMultiplier { get; set; }

        public WeaponType WeaponType { get; set; }
        public bool SpecificWeaponTypeBonus { get; set; }

        public double BonusDamageMultiplier { get; set; }
        public double BonusBaseDamageMultiplier { get; set; }
        public double BonusCritChance { get; set; }
        public double BonusCritMultiplier { get; set; }

        public int GimmickGainPerSec { get; set; }
        public int EnergyGainPerSec { get; set; }

        public virtual void Activate(decimal round)
        {
            Cooldown = MaxCooldown;

            // Make it infinite if max duration is 0
            Duration = MaxDuration == 0 ? decimal.MaxValue : MaxDuration;
            ActivationRounds.Add(round);

            BonusCritChance = MaxBonusCritChance;
            BonusCritMultiplier = MaxBonusCritMultiplier;
            BonusDamageMultiplier = MaxBonusDamageMultiplier;
            BonusBaseDamageMultiplier = MaxBonusBaseDamageMultiplier;
        }

        public virtual bool CanActivate()
        {
            return Cooldown <= 0 && Duration <= 0;
        }

        public virtual void Deactivate(decimal round)
        {
            BonusCritChance = 0;
            BonusCritMultiplier = 0;
            BonusDamageMultiplier = 0;
            BonusBaseDamageMultiplier = 0;
            DeactivationRounds.Add(round);
        }
    }

    public class Debuff : Buff
    {
        public override void Activate(decimal round)
        {
            base.Activate(round);
        }

        public override bool CanActivate()
        {
            return base.CanActivate();
        }

        public override void Deactivate(decimal round)
        {
            base.Deactivate(round);
        }
    }

    public class AbilityBuff : Buff
    {
        public override bool Active => Duration >= 0;

        public override void Activate(decimal round)
        {
            base.Activate(round);
        }

        public override bool CanActivate()
        {
            return false;
        }

        public override void Deactivate(decimal round)
        {
            base.Deactivate(round);
        }
    }
}