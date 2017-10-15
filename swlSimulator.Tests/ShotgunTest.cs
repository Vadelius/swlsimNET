using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;
using swlSimulator.api.Spells.Shotgun;
using swlSimulator.api.Weapons;
using swlSimulator.Models;

namespace swlSimulator.Tests
{
    [TestClass]
    public class ShotgunTest
    {
        [TestMethod]
        public void TestShotgunGimmick()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Shotgun,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "" // "Shotgun.Reload, Shells == 0"
            };

            var spell = new ShotgunSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var loadCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is Reload)).Count();
            var spellCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is ShotgunSpell)).Count();

            // 0.0, cast                    cast: 1
            // 1.0, cast                    cast: 2
            // 2.0, cast                    cast: 3
            // 3.0, cast                    cast: 4
            // 4.0, cast                    cast: 5
            // 5.0, cast                    cast: 6
            // 6.0 - 10.0, nothing OUT OF SHELLS

            Assert.AreEqual(rounds, 6);
            Assert.AreEqual(endTime, 5.0m);
            Assert.IsTrue(loadCount == 0);
            Assert.IsTrue(spellCount == 6);
        }

        [TestMethod]
        public void TestShotgunGimmickReload()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Shotgun,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Shotgun.Reload, Shells == 0"
            };

            var spell = new ShotgunSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var loadCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is Reload)).Count();
            var spellCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is ShotgunSpell)).Count();

            // 0.0, cast                    cast: 1
            // 1.0, cast                    cast: 2
            // 2.0, cast                    cast: 3
            // 3.0, cast                    cast: 4
            // 4.0, cast                    cast: 5
            // 5.0, cast                    cast: 6
            // 6.0, reload                  reload: 1
            // 7.0, cast                    cast: 7
            // 8.0, cast                    cast: 8
            // 9.0, cast                    cast: 9
            // 10.0, cast                   cast: 10

            Assert.AreEqual(rounds, 11);
            Assert.AreEqual(endTime, 10.0m);
            Assert.IsTrue(loadCount == 1);
            Assert.IsTrue(spellCount == 10);
        }

        private sealed class ShotgunSpell : Spell
        {
            public ShotgunSpell()
            {
                WeaponType = WeaponType.Shotgun;
                SpellType = SpellType.Cast;
                PrimaryGimmickCost = 1;
                CastTime = 0;
                BaseDamage = 1;
            }
        }
    }
}
