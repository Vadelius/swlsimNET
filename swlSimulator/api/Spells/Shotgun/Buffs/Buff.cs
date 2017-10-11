using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells.Shotgun.Buffs
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
