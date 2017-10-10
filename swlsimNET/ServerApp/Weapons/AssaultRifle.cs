using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace swlsimNET.ServerApp.Weapons
{
    public class AssaultRifle : Weapon
    {

        private decimal _startedCooking;
        private decimal _fusetimer;

        public override void PreAttack(IPlayer player, RoundResult roundResult)
        {
            var ksr43 = player.Settings.PrimaryWeaponProc == WeaponProc.Ksr43;

            _fusetimer = _fusetimer + player.CurrentTimeSec;
            if (ksr43 && _fusetimer >= 3)
            {
                GimmickResource = 0;
            }
            if (!ksr43 && _fusetimer >= 5)
            {
                GimmickResource = 0;
            }


        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var spellName = spell.Name;
            var roll = Rnd.Next(1, 101);
            var ksr43 = player.Settings.PrimaryWeaponProc == WeaponProc.Ksr43;

            if (spellName != null && _grenadeGenerators.Contains(spellName, StringComparer.CurrentCultureIgnoreCase))
            {
                if (roll > 65)
                {
                    GimmickResource++;
                }
            }


            if (GimmickResource >= 1)
            {
                if (ksr43)
                {
                    _startedCooking = 0;
                }
                if (!ksr43)
                {
                    _startedCooking = 0;
                }
            }
        }


        public override double GetBonusBaseDamage(IPlayer player, ISpell spell, double gimmickBeforeCast)
        {
            double bonusBaseDamage = 0;
            // Infernal Loader Weapon
            if (player.Settings.PrimaryWeaponProc == WeaponProc.InfernalLoader)
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


    }
}