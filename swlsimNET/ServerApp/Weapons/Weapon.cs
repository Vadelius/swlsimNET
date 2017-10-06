using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public enum WeaponType
    {
        None, AssaultRifle, Blade, Blood, Chaos, Elemental, Fist, Hammer, Pistol, Shotgun
    }

    public enum WeaponAffix
    {
        None, Efficiency, Destruction, Energy, Havoc
    }
    public enum WeaponProc
    {
        None, AnimaTouched, FlameWreathed, PlasmaForged, Shadowbound
    }

    public class Weapon
    {
        #region Fields

        protected const int MaxEnergy = 15;
        protected int _energy = 15;
        protected double _gimickResource;
        protected int _maxGimickResource;

        private List<ISpell> _eliteSpells = new List<ISpell>();
        protected readonly Random Rnd = new Random();

        #endregion

        #region Constructor

        public Weapon(WeaponType wtype, WeaponAffix waffix)
        {
            WeaponType = wtype;
            WeaponAffix = waffix;
        }

        #endregion

        #region Properties

        public WeaponType WeaponType { get; }
        public WeaponAffix WeaponAffix { get; }

        public virtual double GimmickResource
        {
            get => _gimickResource;
            set
            {
                _gimickResource = value;
                if (_gimickResource > _maxGimickResource)
                    _gimickResource = _maxGimickResource;
                if (_gimickResource < 0)
                    _gimickResource = 0;
            }
        }

        public int Energy
        {
            get => _energy;
            set
            {
                _energy = value;
                if (_energy > MaxEnergy)
                    _energy = MaxEnergy;
                if (_energy < 0)
                    _energy = 0;
            }
        }

        private double LastEnergyOnCritTimeStamp { get; set; }

        #endregion

        #region Methods

        public virtual void PreAttack(IPlayer player, RoundResult rr)
        {
            // This happens BEFORE the attack is made and BEFORE damage calculations
            // All the time every round
        }

        public virtual double GetBonusBaseDamage(IPlayer player, ISpell spell, double gimmickResourceBeforeCast)
        {
            // This happens BEFORE the attack is made and BEFORE damage calculations
            // Only when a spell is being being cast (finished) or channel tick
            return 0;
        }

        public virtual double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, double gimmickResourceBeforeCast)
        {
            // This happens BEFORE the attack is made and BEFORE damage calculations
            // Only when a spell is being being cast (finished) or channel tick
            return 0;
        }

        public virtual void OnHit(IPlayer player, ISpell spell, double gimmickResourceBeforeCast)
        {
            // This happens BEFORE the attack is made and AFTER damage calculations
            // Only when spell hit
        }

        public virtual void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            // This happens AFTER the attack is made and AFTER damage calculations
            // Only when spell hit
        }

        public void EnergyOnCrit(IPlayer player)
        {
            var timeSinceLastEnergyOnCrit = player.CurrentTimeSec - LastEnergyOnCritTimeStamp;

            if (timeSinceLastEnergyOnCrit >= 1 || LastEnergyOnCritTimeStamp == 0)
            {
                Energy++;
                LastEnergyOnCritTimeStamp = player.CurrentTimeSec;
            }
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            return Rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        #region Weapon affixes

        public void WeaponAffixes(IPlayer player, ISpell spell, RoundResult rr)
        {
            switch (WeaponAffix)
            {
                case WeaponAffix.Efficiency:
                    EfficiencyAffix(player);
                    break;
                case WeaponAffix.Destruction:
                    DestructionAffix(player, rr);
                    break;
                case WeaponAffix.Energy:
                    EnergyAffix(player, spell);
                    break;
                case WeaponAffix.Havoc:
                    HavocAffix(player);
                    break;
            }
        }

        private void EfficiencyAffix(IPlayer player)
        {
            if (!_eliteSpells.Any())
            {
                _eliteSpells = player.Spells.Where(s => s.AbilityType == AbilityType.Elite).ToList();
            }

            // 50% chance
            if (Rnd.Next(1, 3) <= 1) return;

            foreach (var eliteSpell in _eliteSpells)
            {
                if (eliteSpell.Cooldown > 0)
                {
                    eliteSpell.Cooldown -= eliteSpell.Cooldown * 0.05;
                }
            }
        }

        private void DestructionAffix(IPlayer player, RoundResult rr)
        {
            // Get percentage of fight done
            var percentageChange = (player.CurrentTimeSec - player.Settings.FightLength) / player.Settings.FightLength * -100;

            // TODO: < 35% Target HP
            if (percentageChange < 35)
            {
                player.AddBonusAttack(rr, new Destruction());
            }
        }

        private void EnergyAffix(IPlayer player, ISpell spell)
        {
            var roll = Rnd.Next(1, 101);
            if (roll <= 33 && spell?.AbilityType == AbilityType.Power)
            {
                player.PrimaryWeapon.Energy++;
            }
        }

        private void HavocAffix(IPlayer player)
        {
            // TODO: Stacks infinite?? Must be veeeery wrong
            //player.CritPower += 0.075;
        }

        private class Destruction : Spell
        {
            public Destruction()
            {
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.45;
            }
        }

        #endregion

        #endregion 
    }
}