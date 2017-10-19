using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;
using swlSimulator.api.Weapons;
using swlSimulator.Models;

namespace swlSimulator.Tests
{
    [TestClass]
    public class ElementalTest
    {
        [TestMethod]
        public void TestElementalGimmick()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Elemental,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 5,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var spell = new ElementalSpell();
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is ElementalSpell)).Count();

            // Dmg calculated from HEAT BEFORE CAST!

            // heatBeforeCast >= 25 && heatBeforeCast <= 50 
            //      GimmickBonusDamage = 1.087; // 8.7%

            // heatBeforeCast >= 50 && heatBeforeCast <= 75 
            //      GimmickBonusDamage = 1.174; // 17.4%

            // heatBeforeCast >= 75 && heatBeforeCast <= 100 
            //      GimmickBonusDamage = 1.348 // 34.8%

            var round1 = fight.RoundResults.First(r => r.TimeSec == 1);
            var round2 = fight.RoundResults.First(r => r.TimeSec == 2);
            var round3 = fight.RoundResults.First(r => r.TimeSec == 3);
            var round4 = fight.RoundResults.First(r => r.TimeSec == 4);
            var round5 = fight.RoundResults.First(r => r.TimeSec == 5);

            Assert.IsTrue(Math.Abs(round1.TotalDamage - 10) < 0.001);   // 0
            Assert.IsTrue(Math.Abs(round2.TotalDamage - 10.87) < 0.01); // 30
            Assert.IsTrue(Math.Abs(round3.TotalDamage - 11.74) < 0.01); // 60
            Assert.IsTrue(Math.Abs(round4.TotalDamage - 13.48) < 0.01); // 90
            Assert.IsTrue(Math.Abs(round5.TotalDamage - 13.48) < 0.01); // 100
            Assert.IsTrue(spells == 5);
        }

        [TestMethod]
        public void TestElementalGimmickFigurineCold()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Elemental,
                PrimaryWeaponProc = WeaponProc.FrozenFigurine,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 5,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var spell = new ElementalFrozenSpell();
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is ElementalFrozenSpell)).Count();

            // Dmg calculated from HEAT BEFORE CAST!

            // heatBeforeCast >= 25 && heatBeforeCast <= 50 
            //      GimmickBonusDamage = 1.797; // 79.7%

            // heatBeforeCast >= 50 && heatBeforeCast <= 75 
            //      GimmickBonusDamage = 1.884; // 88.4%

            // heatBeforeCast >= 75 && heatBeforeCast <= 100 
            //      GimmickBonusDamage = 2.058 // 205.8%

            var round1 = fight.RoundResults.First(r => r.TimeSec == 1);
            var round2 = fight.RoundResults.First(r => r.TimeSec == 2);
            var round3 = fight.RoundResults.First(r => r.TimeSec == 3);
            var round4 = fight.RoundResults.First(r => r.TimeSec == 4);
            var round5 = fight.RoundResults.First(r => r.TimeSec == 5);

            Assert.IsTrue(Math.Abs(round1.TotalDamage - 10) < 0.001);   // 0
            Assert.IsTrue(Math.Abs(round2.TotalDamage - 17.97) < 0.01); // 30
            Assert.IsTrue(Math.Abs(round3.TotalDamage - 18.84) < 0.01); // 60
            Assert.IsTrue(Math.Abs(round4.TotalDamage - 20.58) < 0.01); // 90
            Assert.IsTrue(Math.Abs(round5.TotalDamage - 20.58) < 0.01); // 100
            Assert.IsTrue(spells == 5);
        }

        // TODO: Add Decay test

        private sealed class ElementalSpell : Spell
        {
            public ElementalSpell()
            {
                PrimaryGimmickGain = 30;
                WeaponType = WeaponType.Elemental;
                SpellType = SpellType.Cast;
                CastTime = 1;
                BaseDamage = 1;
                BonusCritChance = -100; // no crits plz
            }
        }

        private sealed class ElementalFrozenSpell : Spell
        {
            public ElementalFrozenSpell()
            {
                PrimaryGimmickGain = 30;
                WeaponType = WeaponType.Elemental;
                SpellType = SpellType.Cast;
                CastTime = 1;
                BaseDamage = 1;
                BonusCritChance = -100; // no crits plz
                ElementalType = ElementalType.Cold;
            }
        }
    }
}
