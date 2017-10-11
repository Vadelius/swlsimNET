using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Fist.Buffs
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
