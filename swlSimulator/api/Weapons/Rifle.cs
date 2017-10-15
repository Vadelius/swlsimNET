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
        // TODO: Check values, 6s total cooktimer and fully cooked after 3s??
        // TODO: All greande spells have a 4s cooldown
        private const decimal FUSE_TIMER = 3;
        private const decimal COOKINGTIMER = 3;

        private decimal _cookingReadyTimeSec = decimal.MaxValue;

        private bool _ksr43;
        private bool _infernalLoader;
        private bool _init;

        private RoundResult _rr;

        private readonly List<string> _grenadeGenerators = new List<string>
        {
            "FullAuto", "UnveilEssence", "BurstFire"
        };

        public Rifle(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 1;
        }

        public decimal FuseTimer { get; private set; }
        //public decimal CookingTimer => COOKINGTIMER - FuseTimer; // TODO: Fix cooking timer for APL and remove fusetimer

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
                if (_rr == null || rr.TimeSec != _rr.TimeSec)
                {
                    FuseTimer += rr.Interval; 
                }               

                if (FuseTimer > FUSE_TIMER)
                {
                    GimmickResource = 0;
                }
            }
            else if (_cookingReadyTimeSec <= player.CurrentTimeSec)
            {
                // We can use grenade
                GimmickResource = 1;
                _cookingReadyTimeSec = decimal.MaxValue;
                FuseTimer = 0;
            }

            _rr = rr;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            // TODO: Check values some spells have 37.5% chance and some 65%
            if (GimmickResource < 1 && Rnd.Next(1, 101) > 65
                && _grenadeGenerators.Contains(spell.Name, StringComparer.CurrentCultureIgnoreCase) 
                || GimmickResource < 1 && spell.Name == "RifleLoadGrenadeSpell") // Unit test
            {
                // Start cooking, if KSR43 it can be used directly
                if (_ksr43)
                {
                    _cookingReadyTimeSec = decimal.MaxValue;
                    GimmickResource = 1;
                    FuseTimer = 0;
                }
                else if (_cookingReadyTimeSec > player.CurrentTimeSec + COOKINGTIMER)
                {
                    _cookingReadyTimeSec = player.CurrentTimeSec + COOKINGTIMER;
                }
            }
        }

        public override double GetBonusBaseDamage(IPlayer player, ISpell spell, decimal gimmickBeforeCast)
        {
            double bonusBaseDamage = 0;

            if (_infernalLoader)
            {
                bonusBaseDamage += 0.075; // +7.5% AR damage
            }

            return bonusBaseDamage;
        } 
    }
}