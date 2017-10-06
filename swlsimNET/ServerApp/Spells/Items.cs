using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Spells
{
    public class Items : Spell
    {
        public override SpellType SpellType { get; set; } = SpellType.Passive;
        public double BaseDamageModifier { get; set; }
        public double BaseDamageCritModifier { get; set; }
        public bool BonusSpellOnlyOnCrit { get; set; }


        public virtual void Init(IPlayer player)
        {
        }

        private bool _valiMetabollic = false;
        private bool _mnemonicGuardianWerewolf = false;
        private bool _shardOfSesshoSeki = false;
        private bool _electrograviticAttractor = false;

        private bool _animaTouched = false;
        private bool _flameWreathed = false;
        private bool _plasmaForged = false;
        private bool _shadowbound = false;
        private int _lastPlasma;

        private readonly Random _rnd = new Random();

        public void PreAttack(IPlayer player, RoundResult rr) //TODO: Override? Not void.
        {

            if (_valiMetabollic)
            {
                player.AddBonusAttack(rr, new ValiMetabolicAccelerator(player));
            }
            if (_mnemonicGuardianWerewolf)
            {
                player.AddBonusAttack(rr, new MnemonicGuardianWerewolf(player));
            }
            if (_shardOfSesshoSeki)
            {
                player.AddBonusAttack(rr, new ShardOfSesshoSeki(player));
            } // And current spell isnt a DOT.
            if (_electrograviticAttractor)
            {
                player.AddBonusAttack(rr, new ElectrograviticAttractor(player));
            }

            if (_animaTouched)
            {
                var roll = _rnd.Next(1, 4);
                if (roll == 3)
                {
                    player.AddBonusAttack(rr, new AnimaTouched(player));
                }
            }
            if (_flameWreathed)
            {
                var roll = _rnd.Next(1, 101);
                if (roll <= 15)
                {
                    player.AddBonusAttack(rr, new FlameWreathed(player));
                }
            }

            //Plasma-Forged TODO: Whenever you hit you have a 25 % chance to deal an additional(0.565 * Combat Power) physical damage to the target.
            //The amount of damage dealt increases to(1.125 * Combat Power) physical damage the second time this effect triggers on the same target.
            //The third time this effect triggers on the same target, the damage dealt is increased to
            //(2.81 * Combat Power) physical damage and the count of the number of times this effect has triggered is reset.

            if (_plasmaForged)
            {
                var roll = _rnd.Next(1, 5);
                if (roll == 4)
                {
                    switch (_lastPlasma)
                    {
                        case 0:
                            player.AddBonusAttack(rr, new PlasmaForgedOne(player));
                            _lastPlasma = 1;
                            break;
                        case 1:
                            player.AddBonusAttack(rr, new PlasmaForgedTwo(player));
                            _lastPlasma = 2;
                            break;
                        case 2:
                            player.AddBonusAttack(rr, new PlasmaForgedThree(player));
                            _lastPlasma = 0;
                            break;
                    }
                }
            }

            if (_shadowbound)
            {
                var roll = _rnd.Next(1, 6);
                if (roll == 5)
                {
                    player.AddBonusAttack(rr, new Shadowbound(player));
                }
            }
        }
    }

    public sealed class ValiMetabolicAccelerator : Items
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

    public sealed class MnemonicGuardianWerewolf : Items
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

    public sealed class ShardOfSesshoSeki : Items
    {
        public ShardOfSesshoSeki(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Procc;
            BaseDamage = 0.068; // Passively does this for every single hit except dots.
            Args = args;
        }
    }

    public sealed class ElectrograviticAttractor : Items
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

    public sealed class AnimaTouched : Items
    {
        public AnimaTouched(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 0.85;
            Args = args;
        }
    }

    public sealed class FlameWreathed : Items
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

    public sealed class PlasmaForgedOne : Items
    {
        public PlasmaForgedOne(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 0.5;
            Args = args;
        }
    }

    public sealed class PlasmaForgedTwo : Items
    {
        public PlasmaForgedTwo(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 0.5;
            Args = args;
        }
    }

    public sealed class PlasmaForgedThree : Items
    {
        public PlasmaForgedThree(IPlayer player, string args = null)
        {
            WeaponType = WeaponType;
            SpellType = SpellType.Instant;
            BaseDamage = 0.5;
            Args = args;
        }
    }

    public sealed class Shadowbound : Items
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
