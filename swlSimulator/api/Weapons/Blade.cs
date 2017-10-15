using System;
using System.Linq;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;

namespace swlSimulator.api.Weapons
{
    public class Blade : Weapon
    {
        private int _spiritBladeCharges;
        private bool SpiritBladeActive => _spiritBladeCharges > 0;
        private int _deluge;

        public Blade(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 5;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var attack = rr.Attacks.FirstOrDefault(a => a.Spell == spell);
            if (attack == null || !attack.IsHit || attack.Damage <= 0) return;

            var roll = Rnd.Next(1, 3);

            if (player.Settings.PrimaryWeaponProc == WeaponProc.RazorsEdge && attack.IsCrit && roll == 2)
            {
                GimmickResource++;
            }

            if (_deluge >= 6)
            {
                player.AddBonusAttack(rr, new SpiritBlade());
                _deluge = 0;
            }
            
            ChiGenerator(player);
            ChiConsumer();
            SpiritBladeConsumer(player, rr);
            SpiritBladeExtender();
        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, decimal gimmickBeforeCast)
        {
            double bonusBaseDamageMultiplier = 0;

            if (player.Settings.PrimaryWeaponProc == WeaponProc.Apocalypse)
            {
                bonusBaseDamageMultiplier = 0.03 * (double)player.PrimaryWeapon.GimmickResource;
            }

            return bonusBaseDamageMultiplier;
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

        private void ChiConsumer()
        {
            if (GimmickResource == 5 && !SpiritBladeActive)
            {
                GimmickResource = 0;
                _spiritBladeCharges = 10;
            }
        }

        private void SpiritBladeConsumer(IPlayer player, RoundResult rr)
        {
            if (!SpiritBladeActive)
            {
                return;
            }

            var highroller = Rnd.Next(1, 101);
            
            if (player.Settings.PrimaryWeaponProc == WeaponProc.BladeOfTheSeventhSon)
            {
                player.AddBonusAttack(rr, new SpiritBlade());
                player.AddBonusAttack(rr, new BladeOfTheSeventhSon());
                _spiritBladeCharges--;
            }

            if (player.HasPassive("HardenedBlade") && highroller <= 30)
            {
                {
                    if (player.HasPassive("Deluge"))
                    {
                        _deluge += 1;
                    }
                    
                    player.AddBonusAttack(rr, new SpiritBlade());
                    return;
                }
                
            }

            if (player.HasPassive("Deluge"))
            {
                _deluge += 1;
            }

            else
            {
                player.AddBonusAttack(rr, new SpiritBlade());
            }
            _spiritBladeCharges--;
        }     

        // Every 6th hit with spirit blade unleashes an AoE of 0.38CP
        private void SpiritBladeExtender()
        {
            if (!SpiritBladeActive)
            {
                return;
            }

            switch (GimmickResource)
            {
                case 0: 
                    break;
                case 1:
                    _spiritBladeCharges += 1;
                    break;
                case 2:
                    _spiritBladeCharges += 1;
                    break;
                case 3:
                    _spiritBladeCharges += 2;
                    break;
                case 4:
                    _spiritBladeCharges += 4;
                    break;
                case 5:
                    _spiritBladeCharges += 6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private sealed class SpiritBlade : Spell
        {
            public SpiritBlade()
            {
                WeaponType = WeaponType.Blade;
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.97;
            }
        }

        private sealed class BladeOfTheSeventhSon : Spell
        {
            public BladeOfTheSeventhSon()
            {
                WeaponType = WeaponType.Blade;
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.97 * 0.61; // TODO: Is this correct?
            }
        }
        private sealed class Deluge : Spell
        {
            public Deluge(IPlayer player)
            {
                WeaponType = WeaponType.Blade;
                SpellType = SpellType.Procc;
                BaseDamage = 0.38;
            }
        }
    }
}
