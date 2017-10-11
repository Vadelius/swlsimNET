using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Hammer.Buffs
{
    public class UnstoppableForce : AbilityBuff
    {
        public UnstoppableForce()
        {
            MaxDuration = 8;
            MaxBonusBaseDamageMultiplier = 0.2;
            GimmickGainPerSec = 3;
            WeaponType = WeaponType.Hammer;
            SpecificWeaponTypeBonus = true;
        }
    }
}
