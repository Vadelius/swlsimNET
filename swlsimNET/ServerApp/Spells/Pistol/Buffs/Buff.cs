using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Pistol.Buffs
{
    public class Flourish : AbilityBuff
    {
        public Flourish()
        {
            MaxDuration = 6;
            MaxBonusBaseDamageMultiplier = 0.15;
            WeaponType = WeaponType.Pistol;
            SpecificWeaponTypeBonus = true;
        }
    }
}