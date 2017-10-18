using System;
using System.Collections.Generic;
using System.Linq;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Utilities;
using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells
{
    public enum SpellType
    {
        Cast,
        Instant, // Ignores GCD
        Passive,
        Channel,
        Procc,
        Gimmick,
        Dot,
        Buff
    }

    public enum AbilityType
    {
        None,
        Basic,
        Special,
        Power,
        Elite,
        Gadget,
        WaistSignet
    }
    
    public enum ElementalType
    {
        None,
        Cold,
        Fire,
        Lightning
    }

    public class Spell : ISpell
    {
        private readonly List<ISpell> _spellsOfSameType = new List<ISpell>();

        private double _baseDamage;
        private double _bonusBaseDamage;
        private double _bonusBaseDamageMultiplier;

        private double _bonusCritChance;
        private double _bonusCritMultiplier;
        private double _bonusDamage;

        private decimal _primaryGimmickBeforeCast;

        private decimal TickInterval => CastTime / ChannelTicks;
        private double CritPowerMultiplier => SpellType == SpellType.Channel ? 1.25 : 1;
        public int PrimaryGimmickCost { get; set; }
        public int PrimaryGimmickReduce { get; set; } // Will not prevent cast if not enough, just reduce to minimum
        public int SecondaryGimmickCost { get; set; }
        public int SecondaryGimmickReduce { get; set; }
        public int PrimaryGimmickGain { get; set; }
        public int SecondaryGimmickGain { get; set; }
        public int PrimaryGimmickRequirement { get; set; }
        public int PrimaryGimmickGainOnCrit { get; set; }
        public string Name => GetType().Name;

        public double BaseDamage
        {
            get => _baseDamage;
            set
            {
                _baseDamage = value;
                if (BaseDamageCrit == 0)
                {
                    BaseDamageCrit = value;
                }
            }
        }

        public double DotExpirationBaseDamage { get; set; }
        public double BaseDamageCrit { get; set; }
        public WeaponType WeaponType { get; set; }
        public decimal DotDuration { get; set; }
        public virtual SpellType SpellType { get; set; }
        public AbilityType AbilityType { get; set; }
        public ElementalType ElementalType { get; set; }
        public int PrimaryCost { get; set; }
        public int SecondaryCost { get; set; }
        public int PrimaryGain { get; set; }
        public int SecondaryGain { get; set; }
        public decimal MaxCooldown { get; set; }
        public decimal Cooldown { get; set; }
        public decimal CastTime { get; set; }
        public int ChannelTicks { get; set; }
        public int DotTicks { get; set; } // TODO: implement in spellbase
        public double BonusCritChance { get; set; }
        public double BonusCritPower { get; set; }
        public string Args { get; set; }

        // Triggered bonus spell / buff
        public Passive PassiveBonusSpell { get; set; }

        public AbilityBuff AbilityBuff { get; set; }

        public virtual Attack Execute(Player player)
        {
            if (WeaponType != WeaponType.None)
            {
                var spellWeapon = player.GetWeaponFromSpell(this);
                var otherWepon = player.GetOtherWeaponFromSpell(this);

                _primaryGimmickBeforeCast = spellWeapon.GimmickResource;

                spellWeapon.Energy -= PrimaryCost;
                otherWepon.Energy -= SecondaryCost;

                spellWeapon.GimmickResource -= PrimaryGimmickCost;
                otherWepon.GimmickResource -= SecondaryGimmickCost;

                spellWeapon.GimmickResource -= PrimaryGimmickReduce;
                otherWepon.GimmickResource -= SecondaryGimmickReduce;
            }

            if (MaxCooldown > 0)
            {
                Cooldown = MaxCooldown;

                // Set Cooldown of all spells of same type
                foreach (var spell in _spellsOfSameType)
                {
                    spell.Cooldown = AbilityType == AbilityType.Elite
                        ? MaxCooldown * (1 - player.EliteSignetCooldownReduction)
                        : MaxCooldown;
                }
            }

            switch (SpellType)
            {
                case SpellType.Buff:
                    return StartCast(player);
                case SpellType.Cast:
                    return StartCast(player);
                case SpellType.Channel:
                    return StartChannel(player);
                case SpellType.Dot:
                    return StartCast(player);
                case SpellType.Gimmick:
                    return CastNoGcdFinished(player);
                case SpellType.Procc:
                    return CastNoGcdFinished(player);
                case SpellType.Instant:
                    return CastNoGcdFinished(player);
                case SpellType.Passive: // TODO: Needs a test for passive spelltype
                    return CastFinished(player);
                default:
                    return CastFinished(player);
                // TODO: Implement other types
            }
        }

        public virtual bool CanExecute(Player player)
        {
            var spell = this; // debug        

            // Is Casting or channeling
            if (player.CastTime > 0)
            {
                return false;
            }

            // GCD check
            if (spell.SpellType != SpellType.Instant && player.GCD > 0)
            {
                return false;
            }

            if (WeaponType != WeaponType.None)
            {
                var spellWeapon = player.GetWeaponFromSpell(this);
                var otherWepon = player.GetOtherWeaponFromSpell(this);

                var primary = spellWeapon.Energy >= PrimaryCost;
                var secondary = otherWepon.Energy >= SecondaryCost;

                var pgimmick = spellWeapon.GimmickResource >= PrimaryGimmickCost;
                var sgimmick = otherWepon.GimmickResource >= SecondaryGimmickCost;

                var pgimmickreq = spellWeapon.GimmickResource >= PrimaryGimmickRequirement;

                var res = primary && secondary && pgimmick && sgimmick && pgimmickreq;

                if (!res)
                {
                    return false;
                }
            }

            var args = true;

            // Evaluate args if any
            if (!string.IsNullOrEmpty(Args))
            {
                args = Helper.EvaluateArgs(Args, player);
            }

            // Find instance of same spell with highest cooldown...
            if (!_spellsOfSameType.Any())
            {
                foreach (var s in player.Spells)
                {
                    if (s.Name == Name)
                    {
                        _spellsOfSameType.Add(s);
                    }

                    if (WeaponType != WeaponType.Hammer)
                    {
                        continue;
                    }

                    // Hammer specific shit again
                    if (s.Name + "Rage" == Name || s.Name == Name + "Rage")
                    {
                        _spellsOfSameType.Add(s);
                    }
                }
            }

            return Cooldown <= 0 && args;
        }

        public Attack Continue(Player player)
        {
            if (SpellType == SpellType.Cast)
            {
                return ContinueCast(player);
            }
            if (SpellType == SpellType.Channel)
            {
                return ContinueChannel(player);
            }
            if (SpellType == SpellType.Dot)
            {
                return ContinueCast(player);
            }

            return null;
        }

        private Attack StartCast(Player player)
        {
            // Set player to casting spell
            if (SpellType == SpellType.Cast || SpellType == SpellType.Buff)
            {
                player.CurrentSpell = this;
                player.CastTime = CastTime;
            }

            // Set player GCD
            if (player.GCD < 1 && SpellType != SpellType.Instant)
            {
                player.GCD = 1;
            }

            // Instant finish directly
            if (player.CastTime == 0)
            {
                return CastFinished(player);
            }

            return null;
        }

        private Attack StartChannel(Player player)
        {
            // Set player to channeling spell
            if (SpellType == SpellType.Channel)
            {
                player.CurrentSpell = this;
                player.CastTime = CastTime;
            }

            // Set player GCD
            if (player.GCD < 1 && SpellType != SpellType.Instant)
            {
                player.GCD = 1;
            }

            return null;
        }

        private Attack ContinueCast(Player player)
        {
            if (player.CastTime == 0)
            {
                return CastFinished(player);
            }

            return null;
        }

        private Attack ContinueChannel(Player player)
        {
            if (player.CastTime == 0)
            {
                // Last tick, mark casting spell as finished
                player.CurrentSpell = null;
                return ChannelTick(player);
            }

            if (player.CastTime % TickInterval == 0 && player.CastTime < CastTime)
            {
                // Ticks
                return ChannelTick(player);
            }

            return null;
        }

        private Attack ChannelTick(Player player)
        {
            var spell = this; // debug

            var initialTick = CastTime - TickInterval == player.CastTime;
            var lastTick = CastTime == 0;

            // All bonuses applied only on first tick
            if (initialTick)
            {
                AddBuffBonuses(player);
                AddGimmickBonuses(player);
            }

            var isHit = IsHit(player);

            // Dont have to calculate crit on a miss
            var isCrit = false;
            if (isHit)
            {
                isCrit = IsCrit(player);
            }

            var damage = GetDamage(player, isHit, isCrit);

            // Get energy / gimmick gains on hit
            // TODO: Is this correct for all spells, no damage = it doesn't have to hit to get gains?
            if (isHit || BaseDamage <= 0)
            {
                OnHitGains(player);
            }
            if (isCrit)
            {
                OnCritGains(player);
            }

            if (isHit)
            {
                AddGimmickOnHitBonuses(player);
            }

            return new Attack {Spell = this, Damage = damage, IsCrit = isCrit, IsHit = isHit};
        }

        private void AddGimmickOnHitBonuses(IPlayer player)
        {
            // TODO: Check all callers of this method, must non dmg spells actually hit? Should non dmg spells get here?
            var spellWeapon = player.GetWeaponFromSpell(this);
            spellWeapon?.OnHit(player, this, _primaryGimmickBeforeCast);
        }

        private Attack CastFinished(Player player)
        {
            var spell = this; // debug

            AddBuffBonuses(player);
            AddGimmickBonuses(player);

            var isHit = IsHit(player);

            // Dont have to calculate crit on a miss
            var isCrit = false;
            if (isHit)
            {
                isCrit = IsCrit(player);
            }

            var damage = GetDamage(player, isHit, isCrit);

            // TODO: Make dot tick instead of all dmg on apply
            if (SpellType == SpellType.Dot)
            {
                double expirationdamage = 0;
                if (DotExpirationBaseDamage > 0)
                {
                    expirationdamage = GetDamage(player, isHit, isCrit, DotExpirationBaseDamage);
                }

                damage = damage * (double) DotDuration + expirationdamage;
            }

            // Get energy / gimmick gains on hit
            // TODO: Is this correct for all spells, no damage = it doesn't have to hit to get gains?
            if (isHit || BaseDamage <= 0)
            {
                OnHitGains(player);
            }
            if (isHit && damage > 0)
            {
                OnCritGains(player);
            }

            if (isHit)
            {
                AddGimmickOnHitBonuses(player);
            }

            // Mark casting spell as finished
            player.CurrentSpell = null;

            return new Attack {Spell = this, Damage = damage, IsCrit = isCrit, IsHit = isHit};
        }

        private Attack CastNoGcdFinished(IPlayer player)
        {
            var spell = this; // debug

            AddBuffBonuses(player);
            AddGimmickBonuses(player);

            var isHit = IsHit(player);

            // Dont have to calculate crit on a miss
            var isCrit = false;
            if (isHit)
            {
                isCrit = IsCrit(player);
            }

            var damage = GetDamage(player, isHit, isCrit);

            // TODO: Make dot tick instead of all dmg on apply
            if (SpellType == SpellType.Dot)
            {
                double expirationdamage = 0;
                if (DotExpirationBaseDamage > 0)
                {
                    expirationdamage = GetDamage(player, isHit, isCrit, DotExpirationBaseDamage);
                }

                damage = damage * (double) DotDuration + expirationdamage;
            }

            // Get energy / gimmick gains on hit
            // TODO: Is this correct for all spells, no damage = it doesn't have to hit to get gains?
            if (isHit || BaseDamage <= 0)
            {
                OnHitGains(player);
            }
            if (isHit && damage > 0)
            {
                OnCritGains(player);
            }

            if (isHit)
            {
                AddGimmickOnHitBonuses(player);
            }

            return new Attack {Spell = this, Damage = damage, IsCrit = isCrit, IsHit = isHit};
        }

        private void OnHitGains(IPlayer player)
        {
            if (WeaponType != WeaponType.None)
            {
                var spellWeapon = player.GetWeaponFromSpell(this);
                var otherWepon = player.GetOtherWeaponFromSpell(this);

                spellWeapon.Energy += PrimaryGain;
                otherWepon.Energy += SecondaryGain;

                spellWeapon.GimmickResource += PrimaryGimmickGain;
                otherWepon.GimmickResource += SecondaryGimmickGain;
            }
        }

        private void OnCritGains(IPlayer player)
        {
            if (WeaponType != WeaponType.None)
            {
                var spellWeapon = player.GetWeaponFromSpell(this);
                //var otherWepon = player.GetOtherWeaponFromSpell(this);

                spellWeapon.GimmickResource += PrimaryGimmickGainOnCrit;
            }
        }

        private void AddBuffBonuses(IPlayer player)
        {
            var spellWeapon = player.GetWeaponFromSpell(this);

            // Set all buff/debuff defaults
            _bonusCritChance = 0;
            _bonusDamage = 0;
            _bonusCritMultiplier = 0;
            _bonusBaseDamageMultiplier = 0;
            _bonusBaseDamage = 0;

            // Add all bonuses together additive
            foreach (var buff in player.Buffs.Where(b => b.Active))
            {
                if (buff.SpecificWeaponTypeBonus)
                {
                    if (spellWeapon?.WeaponType != buff.WeaponType)
                    {
                        continue;
                    }
                }

                _bonusCritChance += buff.BonusCritChance;
                _bonusDamage += buff.BonusDamageMultiplier;
                _bonusCritMultiplier += buff.BonusCritMultiplier;
                _bonusBaseDamageMultiplier += buff.BonusBaseDamageMultiplier;
            }
        }

        private void AddGimmickBonuses(IPlayer player)
        {
            var spellWeapon = player.GetWeaponFromSpell(this);

            if (spellWeapon == null)
            {
                return;
            }

            _bonusBaseDamageMultiplier +=
                spellWeapon.GetBonusBaseDamageMultiplier(player, this, _primaryGimmickBeforeCast);
            _bonusBaseDamage += spellWeapon.GetBonusBaseDamage(player, this, _primaryGimmickBeforeCast);
        }

        private bool IsHit(IPlayer player)
        {
            var target = (int) player.Settings.TargetType / (double) 100;
            return Helper.IsHit(target, player.GlanceReduction);
        }

        private bool IsCrit(IPlayer player)
        {
            // Non damage spells can't crit 
            if (SpellType == SpellType.Channel)
            {
                // 0.8 represents the penalty for channel spells (compensated via more critpower)
                return BaseDamage > 0 &&
                       Helper.IsCrit((player.CriticalChance + BonusCritChance + _bonusCritChance) * 0.8);
            }

            return BaseDamage > 0 && Helper.IsCrit(player.CriticalChance + BonusCritChance + _bonusCritChance);
        }

        private double GetDamage(IPlayer player, bool isHit, bool isCrit, double basedamage = 0)
        {
            double boost = 1;
            double damage = 0;

            switch (AbilityType)
            {
                case AbilityType.Basic:
                    boost = player.BasicSignetBoost;
                    break;
                case AbilityType.Power:
                    boost = player.PowerSignetBoost;
                    break;
                case AbilityType.Elite:
                    boost = player.EliteSignetBoost;
                    break;
                case AbilityType.WaistSignet:
                    boost = player.WaistSignetBoost;
                    break;
            }

            // Ignore spell basedamage and calculate from this value instead
            if (basedamage > 0)
            {
                damage = isHit
                    ? basedamage * player.CombatPower * boost
                    : 0;

                damage = isCrit
                    ? basedamage * (1 + _bonusBaseDamageMultiplier)
                      * player.CombatPower * boost *
                      ((player.CritPower + BonusCritPower + _bonusCritMultiplier) * CritPowerMultiplier)
                    : damage;

                // Add bonus damage
                damage = damage * (1 + _bonusDamage);

                return damage;
            }

            // Calculate from spell basedamage
            damage = isHit
                ? (BaseDamage + _bonusBaseDamage) * (1 + _bonusBaseDamageMultiplier) * player.CombatPower * boost
                : 0;

            damage = isCrit
                ? (Math.Max(BaseDamage, BaseDamageCrit) + _bonusBaseDamage) * (1 + _bonusBaseDamageMultiplier)
                  * player.CombatPower * boost *
                  ((player.CritPower + BonusCritPower + _bonusCritMultiplier) * CritPowerMultiplier)
                : damage;

            // Add bonus damage
            damage = damage * (1 + _bonusDamage);

            return damage;
        }
    }
}