using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public class AssaultRifle : Weapon
    {
        private bool _infernalLoader = false;
        private bool _ksr43 = false;
        private decimal _startedCooking;
        private decimal _fusetimer;

        public override void PreAttack(IPlayer player, RoundResult roundResult)
        {
            _fusetimer = _fusetimer + player.CurrentTimeSec;
            if (_ksr43 && _fusetimer >= 3)
            {
                GimmickResource = 0;
            }
            if (!_ksr43 && _fusetimer >= 5)
            {
                GimmickResource = 0;
            }
        }

        private void GrenadeCook()
        {
            if (_ksr43)
            {
                GimmickResource++;
                _startedCooking = 0;
                _fusetimer = 0;
            }
            if (!_ksr43 && _startedCooking > 5)
            {
                GimmickResource++;
                _startedCooking = 0;
                _fusetimer = 0;
            }
        }

        public override double GetBonusBaseDamage(IPlayer player, ISpell spell, double gimmickBeforeCast)
        {
            double bonusBaseDamage = 0;
            // Infernal Loader Weapon
            if (_infernalLoader)
            {
                bonusBaseDamage += 0.075; // TODO: 7.5% AR damage
            }

            return bonusBaseDamage;
        }
        private readonly List<string> _grenadeGenerators = new List<string>
        {
            "FullAuto", "UnveilEssence", "BurstFire"
        };

        public AssaultRifle(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 1;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var spellName = spell.Name;
            if (spellName != null && !_grenadeGenerators.Contains(spellName, StringComparer.CurrentCultureIgnoreCase)) return;

            if (Rnd.Next(1, 101) <= 65)
            {
                _startedCooking = player.CurrentTimeSec;
                GrenadeCook();
            }
        } 
    }
}