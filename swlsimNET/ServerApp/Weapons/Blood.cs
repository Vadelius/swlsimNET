﻿using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Blood;

namespace swlsimNET.ServerApp.Weapons
{
    internal class Blood : Weapon
    {
        private bool _init;

        private Passive _flay;
        private Passive _defilement;

        private int _defilementBonusToTimeMs;
        private int _flayBonusToTimeMs;

        private bool _defilementBonus;
        private bool _flayBonus;

        private int LastBloodSpellTimeStamp { get; set; }
        private int LastDecayTimeStamp { get; set; }  

        public override double GimmickResource
        {
            get => _gimickResource;
            set
            {
                // If defilement bonus is up 20% more corruption
                var x = _defilementBonus && value > 0 ? value * 1.2 : value;
                base.GimmickResource = x;
            }
        }

        public Blood(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 100;
        }

        public override void PreAttack(IPlayer player, RoundResult rr)
        {
            // Only on first activation
            if (!_init)
            {
                _init = true;

                // If a blood dot is up
                _flay = player.GetPassive(nameof(Flay));

                // Corruption gain during Desecrate is increased by 20%
                // If at max corruption we gain 16.5% blood ability damage.
                _defilement = player.GetPassive(nameof(Defilement));
            }

            // Set Defilment bonus corruption gain
            _defilementBonus = _defilementBonusToTimeMs >= player.CurrentTimeMs;

            // Set Flay bonus damage
            _flayBonus = _flayBonusToTimeMs >= player.CurrentTimeMs;

            Decay(player);
        }

        public override double GetBonusBaseDamage(IPlayer player, ISpell spell, double corruptionBeforeCast)
        {
            double bonusBaseDamage = 0;

            // Flay passive
            if (_flay != null && _flayBonus && spell.GetType() == typeof(Maleficium))
            {
                bonusBaseDamage += _flay.BaseDamage;
            }

            return bonusBaseDamage;
        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, double corruptionBeforeCast)
        {
            double bonusBaseDamageMultiplier = 0;

            if (corruptionBeforeCast >= 11 && corruptionBeforeCast <= 60)
            {
                bonusBaseDamageMultiplier += 0.156; // 15.6%
            }
            if (corruptionBeforeCast >= 61 && corruptionBeforeCast <= 90)
            {
                bonusBaseDamageMultiplier += 0.327; // 32.7%
            }
            if (corruptionBeforeCast >= 91)
            {
                bonusBaseDamageMultiplier += 0.534; // 53.4%
            }

            // Defilement passive
            if (_defilement != null && corruptionBeforeCast >= 100)
            {
                bonusBaseDamageMultiplier += 0.165; // 16.5%
            }

            return bonusBaseDamageMultiplier;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            // Set new time stamp for new cast
            LastBloodSpellTimeStamp = player.CurrentTimeMs + spell.CastTimeMs;

            if (_defilement != null && spell.GetType() == typeof(Desecrate))
            {
                _defilementBonusToTimeMs = player.CurrentTimeMs + spell.DotDurationMs;
            }

            if (_flay != null && spell.DotDurationMs > 0)
            {
                _flayBonusToTimeMs = player.CurrentTimeMs + spell.DotDurationMs;
            }
        }   

        private void Decay(IPlayer player)
        {
            var timeSinceLastBloodSpell = player.CurrentTimeMs - LastBloodSpellTimeStamp;
            var timeSinceLastDecay = player.CurrentTimeMs - LastDecayTimeStamp;

            if (GimmickResource > 0 && timeSinceLastBloodSpell > 3000 && timeSinceLastDecay >= 1000)
            {
                // Corruption = -4 for each second.
                var time = timeSinceLastBloodSpell - 3000;

                // Remove previous decay from this decay
                if (LastDecayTimeStamp != 0 && timeSinceLastBloodSpell > timeSinceLastDecay)
                {
                    time -= timeSinceLastDecay;
                }

                // Only reduce per second, so for example 1.5s = 1s
                var reduce = (time / 1000) * 4;

                if (reduce <= 0) return;

                GimmickResource -= reduce;
                if (GimmickResource < 0) GimmickResource = 0;

                LastDecayTimeStamp = player.CurrentTimeMs;
            }
        }
    }
}
