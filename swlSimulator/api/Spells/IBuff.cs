using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells
{
    public interface IBuff
    {
        string Name { get; }
        decimal MaxDuration { get; set; }
        decimal Duration { get; set; }
        decimal MaxCooldown { get; }
        decimal Cooldown { get; set; }
        bool Active { get; }
        List<decimal> ActivationRounds { get; }

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

        void Activate(decimal round);
        bool CanActivate();
        void Deactivate(decimal round);
        List<decimal> DeactivationRounds { get; }
    }
}
