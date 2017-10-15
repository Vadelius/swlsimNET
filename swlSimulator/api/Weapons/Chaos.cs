using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;

namespace swlSimulator.api.Weapons
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

            if (spell?.Name == "ChaosUnluckySpell") // Unit test
            {
                return;
            }

            // 30% chance of generating paradoxes
            if (roll > 3 && spell?.Name != "ChaosLuckySpell") // Unit test
            {
                return;
            }

            //// TODO: Whaaaaat is this?
            //roll = Rnd.Next(1, 9);
            //{
            //    if (roll == 8)
            //    {
            //        ParadoxGenerator(player);
            //    }
            //}

            ParadoxGenerator(player, spell);

            if (player.Settings.PrimaryWeaponProc == WeaponProc.WarpedVisage && GimmickResource == 8)
            {
                player.AddBonusAttack(rr, new Doppleganger());
            }

            if (GimmickResource == 8 || player.Settings.PrimaryWeaponProc == WeaponProc.SovTechParadoxGenerator && highroller <= 55)
            {
                ChaoticEffects(player, rr);
            }
        }

        private void ParadoxGenerator(IPlayer player, ISpell spell)
        {
            var roll = Rnd.Next(2, 5);

            if (spell?.Name == "ChaosLuckySpell") // Unit test
            {
                roll = 2;
            }

            GimmickResource += roll;

            if (player.Settings.PrimaryWeaponProc == WeaponProc.OtherworldlyArtifact)
            {
                GimmickResource++;
            }
        }

        private void ChaoticEffects(IPlayer player, RoundResult rr)
        {
            // TODO: 30% chance here also???
            var roll = Rnd.Next(1, 11);

            switch (roll)
            {
                case 1:
                    player.AddBonusAttack(rr, new Singularity());
                    break;
                case 2:
                    player.AddBonusAttack(rr, new Doppleganger());
                    break;
                case 3:
                    player.AddBonusAttack(rr, new Enigma());
                    break;
            }
        }

        #region ChaosProcs

        public class Singularity : Spell
        {
            public Singularity()
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Gimmick;
                BaseDamage = 2.67;
                // TODO: No cost??
            }
        }

        public class Doppleganger : Spell
        {
            public Doppleganger()
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Gimmick;
                BaseDamage = 4.33;
                // TODO: No cost??
                // On avarage lul. Doppelgangers spawn in pairs, and have: 1/3 chance to deal 2.6CP damage & 1/3 chance to deal 5 * 0.52CP damage 
                // & 1/3 chance to deal 1.3CP damage for an average of 4.33CP damage
            }
        }

        public class Enigma : Spell
        {
            public Enigma()
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Gimmick;
                // TODO: No cost??
                // Irrelevant for now TODO: Maybe model purgable critpower steal, but otherwise irrelevant to DPS.
            }
        }

        #endregion
    }
}
