using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Weapons;

namespace swlsim.Spells.Hammer.Buffs
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
