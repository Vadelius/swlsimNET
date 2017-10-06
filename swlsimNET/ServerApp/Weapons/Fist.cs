using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public class Fist : Weapon
    {
        private bool _bladedGauntlets = false;
        private int _bladedStartBonus = 1;
        private bool _bloodDrinkers = false;
        private bool _treshingClaws = false;

        public bool AllowFrenziedWrathAbilities { get; private set; }

        public override void PreAttack(IPlayer player, RoundResult rr)
        {
            if (_bladedGauntlets && _bladedStartBonus == 1)
            {
                GimmickResource = +15;
                _bladedStartBonus = 2;
            }
            if (_bladedGauntlets)
            {
                GimmickResource = +2;
            }
            if (_treshingClaws && AllowFrenziedWrathAbilities)
            {
                player.AddBonusAttack(rr, new TreshingClaws(player));
            }
        }

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
            if (_bloodDrinkers && spell.SpellType == SpellType.Dot)
            {
                GimmickResource = +3;
            }
        }
        public class TreshingClaws : Spell
        {
            public TreshingClaws(IPlayer player)
            {
                WeaponType = WeaponType.Fist;
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.69;
            }
        }
    }
}
