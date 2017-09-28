using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    internal class Fist : Weapon
    {
        public bool AllowFrenziedWrathAbilities { get; private set; }

        public Fist(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 100;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            if (GimmickResource >= 65)
            {
                // TODO: Set this variable 
                AllowFrenziedWrathAbilities = true;
            }
        }
    }
}
