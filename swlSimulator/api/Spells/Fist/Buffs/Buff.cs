using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells.Fist.Buffs
{
    public class Savagery : AbilityBuff
    {
        public Savagery()
        {
            WeaponType = WeaponType.Fist;
            MaxDuration = 6;
            MaxCooldown = 20;
            MaxBonusBaseDamageMultiplier = 0.15;
        }
    }
}
