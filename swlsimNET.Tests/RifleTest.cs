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
    public class RifleTest
    {
        [TestMethod]
        public void TestRifleGimmick()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Rifle,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 4,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var spell = new RifleGrenadeSpell();
            var spell2 = new RifleLoadGrenadeSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            player.Spells.Add(spell2);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var loadCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleLoadGrenadeSpell)).Count();
            var grenadeCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleGrenadeSpell)).Count();

            // 0.0, start load cast

            // 1.0, finish load cast        load: 1
            // 1.0, start cook grenade
            // 1.0, start next load cast

            // 2.0, finish load cast        load: 2
            // 2.0, start next load cast

            // 3.0, finish load cast        load: 3
            // 3.0, start next load cast

            // 4.0, finish load cast        load: 4
            // 4.0, finish cook grenade
            // 4.0, grenade cast (gcd)      grenade: 1

            Assert.AreEqual(rounds, 4);
            Assert.AreEqual(endTime, 4.0m);
            Assert.IsTrue(loadCount == 4);
            Assert.IsTrue(grenadeCount == 1);
        }

        [TestMethod]
        public void TestRifleGimmickKsr43()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Rifle,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 3,
                TargetType = TargetType.Champion,
                PrimaryWeaponProc = WeaponProc.Ksr43,
                Apl = ""
            };

            var spell = new RifleGrenadeSpell();
            var spell2 = new RifleLoadGrenadeSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            player.Spells.Add(spell2);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var loadCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleLoadGrenadeSpell)).Count();
            var grenadeCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleGrenadeSpell)).Count();

            // 0.0, start load cast
            // 1.0, finish load cast        load: 1
            // 1.0, start cook grenade
            // 1.0, finish cook grenade
            // 1.0, grenade cast (gcd)      grenade: 1

            // 2.0, start next load cast
            // 3.0, finish load cast        load: 2
            // 3.0, start cook grenade
            // 3.0, finish cook grenade
            // 3.0, grenade cast (gcd)      grenade: 2

            Assert.AreEqual(rounds, 2);
            Assert.AreEqual(endTime, 3.0m);
            Assert.IsTrue(loadCount == 2);
            Assert.IsTrue(grenadeCount == 2);
        }

        [TestMethod]
        public void TestRifleGimmickFuseTimerApl()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Rifle,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 7,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var spell = new RifleGrenadeSpell { Args = "Rifle.FuseTimer == 3" };
            var spell2 = new RifleLoadGrenadeSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            player.Spells.Add(spell2);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var loadCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleLoadGrenadeSpell)).Count();
            var grenadeCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleGrenadeSpell)).Count();

            Assert.AreEqual(rounds, 7);
            Assert.AreEqual(endTime, 7.0m);
            Assert.IsTrue(loadCount == 7);
            Assert.IsTrue(grenadeCount == 1);
        }

        [TestMethod]
        public void TestRifleGimmickFuseTimer()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Rifle,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 8,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var spell = new RifleGrenadeSpell { Args = "Rifle.FuseTimer > 3" };
            var spell2 = new RifleLoadGrenadeSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            player.Spells.Add(spell2);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var loadCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleLoadGrenadeSpell)).Count();
            var grenadeCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleGrenadeSpell)).Count();

            // NO grenade spells since fusetimer will blow

            Assert.AreEqual(rounds, 8);
            Assert.AreEqual(endTime, 8.0m);
            Assert.IsTrue(loadCount == 8);
            Assert.IsTrue(grenadeCount == 0);
            Assert.IsTrue(!player.Grenade);
        }

        [TestMethod]
        public void TestRifleGimmickFuseTimerKsr43()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Rifle,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 4,
                TargetType = TargetType.Champion,
                PrimaryWeaponProc = WeaponProc.Ksr43,
                Apl = ""
            };

            var spell = new RifleGrenadeSpell { Args = "Rifle.FuseTimer > 3" };
            var spell2 = new RifleLoadGrenadeSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            player.Spells.Add(spell2);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var loadCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleLoadGrenadeSpell)).Count();
            var grenadeCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is RifleGrenadeSpell)).Count();

            // NO grenade spells since fusetimer will blow

            Assert.AreEqual(rounds, 4);
            Assert.AreEqual(endTime, 4.0m);
            Assert.IsTrue(loadCount == 4);
            Assert.IsTrue(grenadeCount == 0);
        }

        private sealed class RifleLoadGrenadeSpell : Spell
        {
            public RifleLoadGrenadeSpell()
            {
                WeaponType = WeaponType.Rifle;
                SpellType = SpellType.Cast;
                CastTime = 1.0m;
                BaseDamage = 1;
            }
        }

        private sealed class RifleGrenadeSpell : Spell
        {
            public RifleGrenadeSpell()
            {
                PrimaryGimmickCost = 1;
                WeaponType = WeaponType.Rifle;
                SpellType = SpellType.Cast;
                CastTime = 0;
                BaseDamage = 1;
            }
        }
    }
}
