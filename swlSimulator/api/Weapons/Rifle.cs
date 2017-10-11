using System;
using System.Collections.Generic;
using System.Linq;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;

namespace swlSimulator.api.Weapons
{
    public class Rifle : Weapon
    {
        private decimal _cookingReadyTimeSec = decimal.MaxValue;
        private decimal _fuseTimeSec;
        private bool _ksr43;
        private bool _infernalLoader;

        private bool _init;

        private readonly List<string> _grenadeGenerators = new List<string>
        {
            "FullAuto", "UnveilEssence", "BurstFire"
        };

        public Rifle(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 1;
        }

        public override void PreAttack(IPlayer player, RoundResult rr)
        {
            // Only on first activation
            if (!_init)
            {
                _init = true;

                // TODO: Not on secondary weapon proc?
                _ksr43 = player.Settings.PrimaryWeaponProc == WeaponProc.Ksr43;
                _infernalLoader = player.Settings.PrimaryWeaponProc == WeaponProc.InfernalLoader;
            }

            if (GimmickResource >= 1)
            {
                _fuseTimeSec += rr.Interval;

                if (_ksr43 && _fuseTimeSec >= 3)
                {
                    GimmickResource = 0;
                }
                else if (!_ksr43 && _fuseTimeSec >= 5)
                {
                    GimmickResource = 0;
                }
            }
            else if (_cookingReadyTimeSec <= player.CurrentTimeSec)
            {
                // We can use grenade
                GimmickResource = 1;
                _cookingReadyTimeSec = decimal.MaxValue;
                _fuseTimeSec = 0;
            }     
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            if (GimmickResource < 1 && Rnd.Next(1, 101) > 65 
                && _grenadeGenerators.Contains(spell.Name, StringComparer.CurrentCultureIgnoreCase) 
                || GimmickResource < 1 && spell.Name == "RifleLoadGrenadeSpell") // Unit test
            {
                // Start cooking, if KSR43 it can be used directly
                if (_ksr43)
                {
                    _cookingReadyTimeSec = decimal.MaxValue;
                    GimmickResource = 1;
                    _fuseTimeSec = 0;
                }
                else
                {
                    _cookingReadyTimeSec = player.CurrentTimeSec + 5;
                }
            }
        }

        public override double GetBonusBaseDamage(IPlayer player, ISpell spell, decimal gimmickBeforeCast)
        {
            double bonusBaseDamage = 0;

            if (_infernalLoader)
            {
                bonusBaseDamage += 0.075; // TODO: 7.5% AR damage
            }

            return bonusBaseDamage;
        } 
    }
}