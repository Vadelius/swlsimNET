using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells
{
    public interface IBuff
    {
        string Name { get; }
        double MaxDuration { get; set; }
        double Duration { get; set; }
        double MaxCooldown { get; }
        double Cooldown { get; set; }
        bool Active { get; }

        WeaponType WeaponType { get; }
        bool SpecificWeaponTypeBonus { get; }
        
        double MaxBonusCritChance { get; }
        double MaxBonusCritMultiplier { get; }
        double MaxBonusDamageMultiplier { get; }
        double MaxBonusBaseDamageMultiplier { get; }

        double BonusCritChance { get; }
        double BonusCritMultiplier { get; }
        double BonusDamageMultiplier { get; }
        double BonusBaseDamageMultiplier { get; }

        int GimmickGainPerSec { get; }
        int EnergyGainPerSec { get; }

        void Activate();
        bool CanActivate();
        void Deactivate();
    }

    public class Buff : IBuff
    {
        public string Name => GetType().Name;
        public double MaxDuration { get; set; }
        public double Duration { get; set; } = -1;
        public double MaxCooldown { get; set; }
        public double Cooldown { get; set; }
        public virtual bool Active => Duration > 0;

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

        public virtual void Activate()
        {
            Cooldown = MaxCooldown;

            // Make it infinite if max duration is 0
            Duration = MaxDuration == 0 ? double.MaxValue : MaxDuration;

            BonusCritChance = MaxBonusCritChance;
            BonusCritMultiplier = MaxBonusCritMultiplier;
            BonusDamageMultiplier = MaxBonusDamageMultiplier;
            BonusBaseDamageMultiplier = MaxBonusBaseDamageMultiplier;
        }

        public virtual bool CanActivate()
        {
            return Cooldown <= 0 && Duration <= 0;
        }

        public virtual void Deactivate()
        {
            BonusCritChance = 0;
            BonusCritMultiplier = 0;
            BonusDamageMultiplier = 0;
            BonusBaseDamageMultiplier = 0;
        }
    }

    public class Debuff : Buff
    {
        public override void Activate()
        {
            base.Activate();
        }

        public override bool CanActivate()
        {
            return base.CanActivate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }

    public class AbilityBuff : Buff
    {
        public override bool Active => Duration >= 0;

        public override void Activate()
        {
            base.Activate();
        }

        public override bool CanActivate()
        {
            return false;
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}