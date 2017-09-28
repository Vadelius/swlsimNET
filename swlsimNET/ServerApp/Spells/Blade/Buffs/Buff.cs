using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Weapons;

namespace swlsim.Spells.Blade.Buffs
{
    public class SupremeHarmony : AbilityBuff
    {
        public SupremeHarmony()
        {
            WeaponType = WeaponType.Blade;
            MaxDuration = 8;
            MaxBonusDamageMultiplier = 0.23; // TODO: Is it really all damage or only basedamage?
            SpecificWeaponTypeBonus = true;
            // 8 second buff for 23% blade damage.
        }
    }
}
