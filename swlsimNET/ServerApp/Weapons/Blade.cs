﻿using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using System;
using System.Linq;

namespace swlsimNET.ServerApp.Weapons
{
    public class Blade : Weapon
    {
        private int SpiritBladeCharges;
        private bool SpiritBladeActive => SpiritBladeCharges > 0;
        private int _deluge;
        public Blade(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 5;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var roll = Rnd.Next(1, 3);
            var attack = rr.Attacks.FirstOrDefault();
            if (attack == null || !attack.IsHit || attack.Damage <= 0) return;

            var weapon = player.GetWeaponFromSpell(attack.Spell);
            if (weapon == null) return;

            if (player.Settings.PrimaryWeaponProc == WeaponProc.RazorsEdge && attack.IsCrit && roll == 2)
            {
                GimmickResource++;
            }

            if (_deluge >= 6)
            {
                player.AddBonusAttack(rr, new SpiritBlade(player));
                _deluge = 0;
            }
            
            ChiGenerator(player);
            ChiConsumer();
            SpiritBladeConsumer(player, rr);
            SpiritBladeExtender();
        }

        private void ChiGenerator(IPlayer player)
        {
            var roll = Rnd.Next(1, 3);
            var highroller = Rnd.Next(1, 101);

            if (player.Settings.PrimaryWeaponProc == WeaponProc.Soulblade && highroller < 50 + GimmickResource * 3)
            {
                GimmickResource++;
            }
            if (roll == 2 && GimmickResource <= 5)
            {
                GimmickResource++;
            }
        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, double gimmickBeforeCast)
        {
            double bonusBaseDamageMultiplier = 0;

            if (player.Settings.PrimaryWeaponProc == WeaponProc.Apocalypse)
            {
                bonusBaseDamageMultiplier = 0.03 * player.PrimaryWeapon.GimmickResource;
            }


            return bonusBaseDamageMultiplier;
        }
        private void ChiConsumer()
        {
            if (GimmickResource == 5 && !SpiritBladeActive)
            {
                GimmickResource = 0;
                SpiritBladeCharges = 10;
            }
        }

        private void SpiritBladeConsumer(IPlayer player, RoundResult rr)
        {
            if (!SpiritBladeActive) return;
            var highroller = Rnd.Next(1, 101);
            
            if (player.Settings.PrimaryWeaponProc == WeaponProc.BladeOfTheSeventhSon)
            {
                player.AddBonusAttack(rr, new SpiritBlade(player));
                player.AddBonusAttack(rr, new BladeOfTheSeventhSon(player));
                SpiritBladeCharges--;
            }
            if (player.HasPassive("HardenedBlade") && highroller <= 30)
            {
                {
                    if (player.HasPassive("Deluge"))
                    {
                        _deluge += 1;
                    }
                    
                    player.AddBonusAttack(rr, new SpiritBlade(player));
                    return;
                }
                
            }
            if (player.HasPassive("Deluge"))
            {
                _deluge += 1;
            }
            else player.AddBonusAttack(rr, new SpiritBlade(player));
            SpiritBladeCharges--;
        }

        

// Every 6th hit with spirit blade unleashes an AoE of 0.38CP
        private void SpiritBladeExtender()
        {
            if (!SpiritBladeActive) return;

            switch (GimmickResource)
            {
                case 0: 
                    break;
                case 1:
                    SpiritBladeCharges += 1;
                    break;
                case 2:
                    SpiritBladeCharges += 1;
                    break;
                case 3:
                    SpiritBladeCharges += 2;
                    break;
                case 4:
                    SpiritBladeCharges += 4;
                    break;
                case 5:
                    SpiritBladeCharges += 6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private class SpiritBlade : Spell
        {
            public SpiritBlade(IPlayer player)
            {
                WeaponType = WeaponType.Blade;
                SpellType = SpellType.Gimmick;
                BaseDamage = player.CombatPower * 0.97;
            }
        }
        private class BladeOfTheSeventhSon : Spell
        {
            public BladeOfTheSeventhSon(IPlayer player)
            {
                WeaponType = WeaponType.Blade;
                SpellType = SpellType.Gimmick;
                BaseDamage = player.CombatPower * 0.97 * 0.61;
            }
        }
        private class Deluge : Spell
        {
            public Deluge(IPlayer player)
            {
                WeaponType = WeaponType.Blade;
                SpellType = SpellType.Procc;
                BaseDamage = player.CombatPower * 0.38;
            }
        }
    }
}
