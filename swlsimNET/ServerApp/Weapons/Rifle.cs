using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace swlsimNET.ServerApp.Weapons
{
    public class Rifle : Weapon
    {
        private const int Fusetimer = 5;
        private const int FusetimerKsr43 = 3;
        private const int Cookingtimer = 5;

        private decimal _cookingReadyTimeSec = decimal.MaxValue;
        private decimal _fuseTimeSec;

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

        public decimal FuseTimer => _fuseTimeSec;
        //public decimal Cookingtimer => ;

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
                    _fuseTimeSec += rr.Interval; 
                }               

                if (_ksr43 && _fuseTimeSec > FusetimerKsr43)
                {
                    GimmickResource = 0;
                }
                else if (!_ksr43 && _fuseTimeSec > Fusetimer)
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

            _rr = rr;
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
                else if (_cookingReadyTimeSec > player.CurrentTimeSec + Cookingtimer)
                {
                    _cookingReadyTimeSec = player.CurrentTimeSec + Cookingtimer;
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