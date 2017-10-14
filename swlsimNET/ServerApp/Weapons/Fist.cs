using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public class Fist : Weapon
    {
        private int _bladedStartBonus = 1;

        public bool AllowFrenziedWrathAbilities { get; private set; }

        public Fist(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 100;
        }

        public override void PreAttack(IPlayer player, RoundResult rr)
        {
            if (player.Settings.PrimaryWeaponProc == WeaponProc.BladedGauntlets && _bladedStartBonus == 1)
            {
                GimmickResource = +15; // TODO: Grem, =+ ?
                _bladedStartBonus = 2;
            }

            if (player.Settings.PrimaryWeaponProc == WeaponProc.BladedGauntlets)
            {
                GimmickResource = +2; // TODO: Grem, =+ ?
            }

            if (player.Settings.PrimaryWeaponProc == WeaponProc.TreshingClaws && AllowFrenziedWrathAbilities)
            {
                player.AddBonusAttack(rr, new TreshingClaws());
            }
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            if (GimmickResource >= 65)
            {
                // TODO: Set this variable 
                AllowFrenziedWrathAbilities = true;
            }

            if (player.Settings.PrimaryWeaponProc == WeaponProc.BloodDrinkers && spell.SpellType == SpellType.Dot)
            {
                GimmickResource = +3; // TODO: Grem, =+ ?
            }
        }

        private sealed class TreshingClaws : Spell
        {
            public TreshingClaws()
            {
                WeaponType = WeaponType.Fist;
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.69;
            }
        }
    }
}
