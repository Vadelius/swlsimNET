using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Shotgun.Buffs
{
    public class Buff
    {
        public class IfritanDespoiler : AbilityBuff
        {
            public IfritanDespoiler()
            {
                MaxDuration = 4;
                BonusCritMultiplier = 0.14;
                WeaponType = WeaponType.Shotgun;
                SpecificWeaponTypeBonus = true;
            }
        }
    }
}