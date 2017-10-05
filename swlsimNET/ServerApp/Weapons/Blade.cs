using System;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public class Blade : Weapon
    {
        private int SpiritBladeCharges;
        private bool SpiritBladeActive => SpiritBladeCharges > 0;

        public Blade(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 5;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            ChiGenerator();
            ChiConsumer();
            SpiritBladeConsumer(player, rr);
            SpiritBladeExtender();
        }

        private void ChiGenerator()
        {
            // 50% chance
            if (Rnd.NextDouble() >= 0.5)
            {
                if(GimmickResource < 5) GimmickResource++;
            }
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

            player.AddBonusAttack(rr, new SpiritBlade(player));
            SpiritBladeCharges--;
        }

        private void SpiritBladeExtender()
        {
            if (!SpiritBladeActive) return;

            switch (GimmickResource)
            {
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
    }
}
