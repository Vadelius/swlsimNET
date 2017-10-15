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
    public class ChaosTest
    {
        [TestMethod]
        public void TestChaosGimmick()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Chaos,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var spell = new ChaosUnluckySpell();
            var player = new Player(setting);
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var spellCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is ChaosUnluckySpell)).Count();

            Assert.AreEqual(rounds, 10);
            Assert.AreEqual(endTime, 10.0m);
            Assert.IsTrue(spellCount == 10);
            Assert.IsTrue(player.Paradox == 0);
        }

        [TestMethod]
        public void TestChaosGimmickParadox()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Chaos,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 4,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var spell = new ChaosLuckySpell();
            var player = new Player(setting);
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var spellCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is ChaosLuckySpell)).Count();

            Assert.AreEqual(rounds, 4);
            Assert.AreEqual(endTime, 4.0m);
            Assert.IsTrue(spellCount == 4);
            Assert.IsTrue(player.Paradox == 8);
        }

        private sealed class ChaosUnluckySpell : Spell
        {
            public ChaosUnluckySpell()
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Cast;
                CastTime = 1;
                BaseDamage = 1;
            }
        }

        private sealed class ChaosLuckySpell : Spell
        {
            public ChaosLuckySpell()
            {
                WeaponType = WeaponType.Chaos;
                SpellType = SpellType.Cast;
                CastTime = 1;
                BaseDamage = 8;
            }
        }
    }
}
