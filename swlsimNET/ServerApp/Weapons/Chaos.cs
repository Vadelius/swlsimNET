using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using System;

namespace swlsimNET.ServerApp.Weapons
{
    public class Chaos : Weapon
    {
        public Chaos(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 8;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var roll = Rnd.Next(1, 11);
            var highroller = Rnd.Next(1, 1001);

            if (roll >= 1 && roll <= 3)
            {
                ParadoxGenerator(player);
            }

            roll = Rnd.Next(1, 9);
            {
                if (roll == 8)
                {
                    ParadoxGenerator(player);
                }
            }

            ParadoxGenerator(player);

            if (GimmickResource != 8) return;

            if (player.Settings.PrimaryWeaponProc == WeaponProc.WarpedVisage && GimmickResource == 8)
            {
                player.AddBonusAttack(rr, new Doppleganger(player));
            }

            if (GimmickResource == 8 || player.Settings.PrimaryWeaponProc == WeaponProc.SovTechParadoxGenerator && highroller <= 55)
            {
                ChaoticEffects(player, rr);
            }

        }

        private void ParadoxGenerator(IPlayer player)
        {
            if (GimmickResource >= 8) return;

            var roll = Rnd.Next(1, 4);

            switch (roll)
            {
                case 1:
                    GimmickResource = +2;
                    if (player.Settings.PrimaryWeaponProc == WeaponProc.OtherworldlyArtifact)
                    {
                        GimmickResource++;
                    }
                    break;
                case 2:
                    GimmickResource = +3;
                    if (player.Settings.PrimaryWeaponProc == WeaponProc.OtherworldlyArtifact)
                    {
                        GimmickResource++;
                    }
                    break;
                case 3:
                    GimmickResource = +4;
                    if (player.Settings.PrimaryWeaponProc == WeaponProc.OtherworldlyArtifact)
                    {
                        GimmickResource++;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChaoticEffects(IPlayer player, RoundResult rr)
        {
            var roll = Rnd.Next(1, 11);

            switch (roll)
            {
                case 1:
                    player.AddBonusAttack(rr, new Singularity(player));
                    break;
                case 2:
                    player.AddBonusAttack(rr, new Doppleganger(player));
                    break;
                case 3:
                    player.AddBonusAttack(rr, new Enigma(player));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #region ChaosProcs

        public class Singularity : Spell
        {
            public Singularity(IPlayer player)
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Gimmick;
                BaseDamage = player.CombatPower * 2.67;
            }
        }

        public class Doppleganger : Spell
        {
            public Doppleganger(IPlayer player)
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Gimmick;
                BaseDamage = player.CombatPower * 4.33;
                // On avarage lul. Doppelgangers spawn in pairs, and have: 1/3 chance to deal 2.6CP damage & 1/3 chance to deal 5 * 0.52CP damage 
                // & 1/3 chance to deal 1.3CP damage for an average of 4.33CP damage
            }
        }

        public class Enigma : Spell
        {
            public Enigma(IPlayer player)
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Gimmick;
                // Irrelevant for now TODO: Maybe model purgable critpower steal, but otherwise irrelevant to DPS.
            }
        }

        #endregion
    }
}
