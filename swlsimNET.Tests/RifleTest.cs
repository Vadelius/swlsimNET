using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlsimNET.Models;
using swlsimNET.ServerApp;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.Tests
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
                FightLength = 6,
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

            // private const int Fusetimer = 5;
            // private const int FusetimerKsr43 = 3;
            // private const int Cookingtimer = 5;

            // 0.0, start load cast
            // 1.0, finish load cast        load: 1
            // 1.0, start cook grenade

            // 1.0, start next load cast
            // 2.0, finish load cast        load: 2
            // 2.0, start next load cast
            // 3.0, finish load cast        load: 3
            // 3.0, start next load cast
            // 4.0, finish load cast        load: 4
            // 4.0, start next load cast
            // 5.0, finish load cast        load: 5
            // 5.0, start next load cast
            // 6.0, finish load cast        load: 6

            // 6.0, finish cook grenade
            // 6.0, grenade cast (gcd)      grenade: 1

            // Rounds where something is executed, 1 and 3 (2 total)

            Assert.AreEqual(rounds, 6);
            Assert.AreEqual(endTime, 6.0m);
            Assert.IsTrue(loadCount == 6);
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
                FightLength = 11,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var spell = new RifleGrenadeSpell { Args = "Rifle.FuseTimer == 5" };
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

            Assert.AreEqual(rounds, 11);
            Assert.AreEqual(endTime, 11.0m);
            Assert.IsTrue(loadCount == 11);
            Assert.IsTrue(grenadeCount == 1);
        }

        [TestMethod]
        public void TestRifleGimmickFuseTimer()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Rifle,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 12,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var spell = new RifleGrenadeSpell { Args = "Rifle.FuseTimer > 5" };
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

            Assert.AreEqual(rounds, 12);
            Assert.AreEqual(endTime, 12.0m);
            Assert.IsTrue(loadCount == 12);
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
