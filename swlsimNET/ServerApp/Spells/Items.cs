using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.Models;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Utilities;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells
{
    public class Items
    {
        public enum NeckTalisman
        {
            SeedOfAgression, ChokerOfSheedBlood, EgonPendant
        }
        public enum LuckTalisman
        {
            ColdSilver, GamblersSoul
        }
        public enum HeadTalisman
        {
            Ashes
        }
        public enum Gadget
        {
            ElectrograviticAttractor, ShardOfSesshoSeki, ValiMetabolic, MnemonicGuardianWerewolf
        }

        private Player _player;
        private readonly Random _rnd = new Random();
        private int _lastPlasma;

        public Items(Player player)
        {
            _player = player;
        }

        public void Execution(RoundResult rr)
        {
            var attack = rr.Attacks.FirstOrDefault();
            if (attack == null || !attack.IsHit || attack.Damage <= 0) return;

            var weapon = _player.GetWeaponFromSpell(attack.Spell);
            if (weapon == null) return;

            if (attack.IsCrit)
            {
                if (_player.Settings.Neck == NeckTalisman.EgonPendant)
                {
                    _player.AddBonusAttack(rr, new EgonPendant(_player));
                }
                if (_player.Settings.Neck == NeckTalisman.ChokerOfSheedBlood)
                {
                    _player.AddBonusAttack(rr, new ChokerOfShedBlood(_player));
                }
                if (_player.Settings.Luck == LuckTalisman.GamblersSoul)
                {
                    _player.AddBonusAttack(rr, new GamblersSoul(_player));
                }
                // Cold Silver Dice 22% on Crit +1 Energy.
                if (_player.Settings.Luck == LuckTalisman.ColdSilver && Helper.RNG() >= 0.78)
                {
                    weapon.Energy++;
                    _player.AddBonusAttack(rr, new ColdSilver(_player));
                }
            }

            // Hit 11% (<50% BossHP) +1 Energy.
            if (_player.Settings.Neck == NeckTalisman.SeedOfAgression && Helper.RNG() >= 0.945)
            {
                weapon.Energy++;
                _player.AddBonusAttack(rr, new SeedOfAggression(_player));
            }

            // Ashes Proc from Spells dealing X*CombatPower (NOT ON GCD)
            if (_player.Settings.Head == HeadTalisman.Ashes && _player.RepeatHits == 3)
            {
                _player.AddBonusAttack(rr, new Ashes(_player));

                _player.RepeatHits = 0;
            }
            else if (_player.RepeatHits < 3 && _player.Settings.Head == HeadTalisman.Ashes)
            {
                _player.RepeatHits++;
            }

            //Gadgets below
            //TODO: Fix the cooldowns.
            //if (_player.Settings.Gadget == Gadget.ValiMetabolic)
            //{
            //    _player.AddBonusAttack(rr, new ValiMetabolicAccelerator(_player));
            //}

            //if (_player.Settings.Gadget == Gadget.MnemonicGuardianWerewolf)
            //{
            //    _player.AddBonusAttack(rr, new MnemonicGuardianWerewolf(_player));
            //}

            //if (_player.Settings.Gadget == Gadget.ShardOfSesshoSeki && _player.CurrentSpell.SpellType != SpellType.Dot)
            //{
            //    _player.AddBonusAttack(rr, new ShardOfSesshoSeki(_player));
            //}

            //if (_player.Settings.Gadget == Gadget.ElectrograviticAttractor)
            //{
            //    _player.AddBonusAttack(rr, new ElectrograviticAttractor(_player));
            //}

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.AnimaTouched)
            {
                var roll = _rnd.Next(1, 4);
                if (roll == 3)
                {
                    _player.AddBonusAttack(rr, new AnimaTouched(_player));
                }
            }

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.FlameWreathed)
            {
                var roll = _rnd.Next(1, 101);
                if (roll <= 15)
                {
                    _player.AddBonusAttack(rr, new FlameWreathed(_player));
                }
            }

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.PlasmaForged)
            {
                var roll = _rnd.Next(1, 5);
                if (roll == 4)
                {
                    switch (_lastPlasma)
                    {
                        case 0:
                            _player.AddBonusAttack(rr, new PlasmaForgedOne(_player));
                            _lastPlasma = 1;
                            break;
                        case 1:
                            _player.AddBonusAttack(rr, new PlasmaForgedTwo(_player));
                            _lastPlasma = 2;
                            break;
                        case 2:
                            _player.AddBonusAttack(rr, new PlasmaForgedThree(_player));
                            _lastPlasma = 0;
                            break;
                    }
                }
            }

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.Shadowbound)
            {
                var roll = _rnd.Next(1, 6);
                if (roll == 5)
                {
                    _player.AddBonusAttack(rr, new Shadowbound(_player));
                }
            }
        }
    }

    // Gadgets

    public sealed class ValiMetabolicAccelerator : Spell
    {
        public ValiMetabolicAccelerator(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Instant;
            PrimaryGain = 3;
            SecondaryGain = 2;
            MaxCooldown = 30; //TODO: Actually check for gadget/weapon proc cooldowns when running _player.AddBonusAttack!
            Args = args;
        }
    }

    public sealed class MnemonicGuardianWerewolf : Spell
    {
        public MnemonicGuardianWerewolf(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Instant;
            MaxCooldown = 30;
            DotDuration = 10;
            BaseDamage = 0.0225;
            Args = args;

        }
    }

    public sealed class ShardOfSesshoSeki : Spell
    {
        public ShardOfSesshoSeki(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Procc;
            BaseDamage = 0.068; // Passively does this for every single hit except dots.
            Args = args;
        }
    }

    public sealed class ElectrograviticAttractor : Spell
    {
        public ElectrograviticAttractor(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Instant;
            BaseDamage = 1.8;
            MaxCooldown = 30;
            Args = args;
        }
    }

    // Global procs that can be of all weapon types.

    public sealed class AnimaTouched : Spell
    {
        public AnimaTouched(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 0.85;
            Args = args;
        }
    }

    public sealed class FlameWreathed : Spell
    {
        public FlameWreathed(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Dot;
            DotDuration = 5;
            BaseDamage = 0.5;
            Args = args;
        }
    }

    public sealed class PlasmaForgedOne : Spell
    {
        public PlasmaForgedOne(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 0.565;
            Args = args;
        }
    }

    public sealed class PlasmaForgedTwo : Spell
    {
        public PlasmaForgedTwo(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 1.125;
            Args = args;
        }
    }

    public sealed class PlasmaForgedThree : Spell
    {
        public PlasmaForgedThree(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 2.81;
            Args = args;
        }
    }

    public sealed class Shadowbound : Spell
    {
        public Shadowbound(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 2.25;
            MaxCooldown = 15; //ICD
            Args = args;
            //TODO: 20% chance per hit to cast "Raven Blade"
        }
    }


}

