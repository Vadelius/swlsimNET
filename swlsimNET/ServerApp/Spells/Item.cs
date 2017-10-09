using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Utilities;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells
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

    public class Item
    {
        private ElectrograviticAttractor _electrograviticAttractor;
        private MnemonicGuardianWerewolf _werewolf;
        private ValiMetabolicAccelerator _valiMetabolicAccelerator;

        public List<ISpell> Spells { get; set; } 

        private IPlayer _player;
        private readonly Random _rnd = new Random();
        private int _lastPlasma;

        public int RepeatHits { get; set; }

        public Item(IPlayer player)
        {
            _player = player;

            _electrograviticAttractor = new ElectrograviticAttractor(player);
            _werewolf = new MnemonicGuardianWerewolf(_player);
            _valiMetabolicAccelerator = new ValiMetabolicAccelerator(_player);

            // Add all item spells with cooldowns here
            Spells = new List<ISpell>
            {
                _electrograviticAttractor,
                _werewolf,
                _valiMetabolicAccelerator
            };
        }

        public void Execution(RoundResult rr)
        {
            var attack = rr.Attacks.FirstOrDefault();
            if (attack == null || !attack.IsHit || attack.Damage <= 0) return;

            var weapon = _player.GetWeaponFromSpell(attack.Spell);
            if (weapon == null) return;

            if (attack.IsCrit)
            {
                switch (_player.Settings.Neck)
                {
                    case NeckTalisman.EgonPendant:
                        _player.AddBonusAttack(rr, new EgonPendant(_player));
                        break;
                    case NeckTalisman.ChokerOfSheedBlood:
                        _player.AddBonusAttack(rr, new ChokerOfShedBlood(_player));
                        break;
                }

                switch (_player.Settings.Luck)
                {
                    case LuckTalisman.GamblersSoul:
                        _player.AddBonusAttack(rr, new GamblersSoul(_player));
                        break;
                    // Cold Silver Dice 22% on Crit +1 Energy.
                    case LuckTalisman.ColdSilver when Helper.RNG() >= 0.78:
                        weapon.Energy++;
                        _player.AddBonusAttack(rr, new ColdSilver());
                        break;
                }              
            }

            // Hit 11% (<50% BossHP) +1 Energy.
            if (_player.Settings.Neck == NeckTalisman.SeedOfAgression && Helper.RNG() >= 0.945)
            {
                weapon.Energy++;
                _player.AddBonusAttack(rr, new SeedOfAggression());
            }

            // Ashes Proc from Spells dealing X*CombatPower (NOT ON GCD)
            if (_player.Settings.Head == HeadTalisman.Ashes && RepeatHits == 3)
            {
                _player.AddBonusAttack(rr, new Ashes());
                RepeatHits = 0;
            }
            else if (RepeatHits < 3 && _player.Settings.Head == HeadTalisman.Ashes)
            {
                RepeatHits++;
            }

            //Gadgets below
            switch (_player.Settings.Gadget)
            {
                case Gadget.ValiMetabolic:
                    _player.AddBonusAttack(rr, _valiMetabolicAccelerator);
                    break;
                case Gadget.MnemonicGuardianWerewolf:
                    _player.AddBonusAttack(rr, _werewolf);
                    break;
                case Gadget.ShardOfSesshoSeki when attack.Spell.SpellType != SpellType.Dot:
                    _player.AddBonusAttack(rr, new ShardOfSesshoSeki(_player));
                    break;
                case Gadget.ElectrograviticAttractor:
                    _player.AddBonusAttack(rr, _electrograviticAttractor);
                    break;
            }

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.AnimaTouched && _rnd.Next(1, 4) == 3)
            {
                _player.AddBonusAttack(rr, new AnimaTouched(_player));
            }

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.FlameWreathed && _rnd.Next(1, 101) <= 15)
            {
                _player.AddBonusAttack(rr, new FlameWreathed(_player));
            }

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.PlasmaForged && _rnd.Next(1, 5) == 4)
            {
                switch (_lastPlasma)
                {
                    case 0:
                        _player.AddBonusAttack(rr, new PlasmaForgedOne());
                        _lastPlasma = 1;
                        break;
                    case 1:
                        _player.AddBonusAttack(rr, new PlasmaForgedTwo());
                        _lastPlasma = 2;
                        break;
                    case 2:
                        _player.AddBonusAttack(rr, new PlasmaForgedThree());
                        _lastPlasma = 0;
                        break;
                }
            }

            if (_player.Settings.PrimaryWeaponProc == WeaponProc.Shadowbound && _rnd.Next(1, 6) == 5)
            {
                _player.AddBonusAttack(rr, new Shadowbound());
            }
        }

        private sealed class ValiMetabolicAccelerator : Spell
        {
            public ValiMetabolicAccelerator(IPlayer player, string args = null)
            {
                AbilityType = AbilityType.Gadget;
                SpellType = SpellType.Instant;
                PrimaryGain = 3;
                SecondaryGain = 2;
                MaxCooldown = 30;
                Args = args;
            }
        }

        private sealed class MnemonicGuardianWerewolf : Spell
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

        private sealed class ShardOfSesshoSeki : Spell
        {
            public ShardOfSesshoSeki(IPlayer player, string args = null)
            {
                AbilityType = AbilityType.Gadget;
                SpellType = SpellType.Procc;
                BaseDamage = 0.068; // Passively does this for every single hit except dots.
                Args = args;
            }
        }

        private sealed class ElectrograviticAttractor : Spell
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

        private sealed class AnimaTouched : Spell
        {
            public AnimaTouched(IPlayer player, string args = null)
            {
                WeaponType = WeaponType;
                SpellType = SpellType.Instant;
                BaseDamage = 0.85;
                Args = args;
            }
        }

        private sealed class FlameWreathed : Spell
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

        private sealed class PlasmaForgedOne : Spell
        {
            public PlasmaForgedOne(string args = null)
            {
                WeaponType = WeaponType;
                SpellType = SpellType.Instant;
                BaseDamage = 0.565;
                Args = args;
            }
        }

        private sealed class PlasmaForgedTwo : Spell
        {
            public PlasmaForgedTwo(string args = null)
            {
                WeaponType = WeaponType;
                SpellType = SpellType.Instant;
                BaseDamage = 1.125;
                Args = args;
            }
        }

        private sealed class PlasmaForgedThree : Spell
        {
            public PlasmaForgedThree(string args = null)
            {
                WeaponType = WeaponType;
                SpellType = SpellType.Instant;
                BaseDamage = 2.81;
                Args = args;
            }
        }

        private sealed class Shadowbound : Spell
        {
            public Shadowbound(string args = null)
            {
                WeaponType = WeaponType;
                SpellType = SpellType.Instant;
                BaseDamage = 2.25;
                MaxCooldown = 15; //ICD
                Args = args;
            }
        }

        private sealed class Ashes : Spell
        {
            public Ashes(string args = null)
            {
                WeaponType = WeaponType.None;
                SpellType = SpellType.Procc;
                BaseDamage = 0.15;
            }
        }

        private sealed class SeedOfAggression : Spell
        {
            public SeedOfAggression(string args = null)
            {
                WeaponType = WeaponType.None;
                SpellType = SpellType.Procc;
            }
        }

        private sealed class ColdSilver : Spell
        {
            public ColdSilver(string args = null)
            {
                WeaponType = WeaponType.None;
                SpellType = SpellType.Procc;
            }
        }

        private sealed class GamblersSoul : Spell
        {
            public GamblersSoul(IPlayer player, string args = null)
            {
                WeaponType = WeaponType.None;
                SpellType = SpellType.Procc;
                BaseDamage = 0.10;
            }
        }

        private sealed class ChokerOfShedBlood : Spell
        {
            public ChokerOfShedBlood(IPlayer player, string args = null)
            {
                WeaponType = WeaponType.None;
                SpellType = SpellType.Procc;
                BaseDamage = 0.20;
            }
        }

        private sealed class EgonPendant : Spell
        {
            public EgonPendant(IPlayer player, string args = null)
            {
                WeaponType = WeaponType.None;
                SpellType = SpellType.Dot;
                BaseDamage = 0.06;
                DotDuration = 5;
                //TODO: "Your damage is increased by 1.75% for each enemy affected by this effect." <- For Dot Duration.
            }
        }
    }
}

