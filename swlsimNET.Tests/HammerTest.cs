using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;
using swlSimulator.api.Weapons;
using swlSimulator.Models;

namespace swlsimNET.Tests
{
    [TestClass]
    public class HammerTest
    {
        [TestMethod]
        public void TestHammerGimmick()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 3,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var hSpell = new HammerSpell();
            var hSpellRage = new HammerSpellRage();

            player.Spells.Add(hSpell);
            player.Spells.Add(hSpellRage);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var spell = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is HammerSpell)).Count();
            var spellRage = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is HammerSpellRage)).Count();

            Assert.AreEqual(rounds, 4);
            Assert.AreEqual(endTime, 3.0m);
            Assert.IsTrue(spell == 2);
            Assert.IsTrue(spellRage == 2);
        }

        [TestMethod]
        public void TestHammerEnrage()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = "Hammer.Smash, Buff.Enraged"
            };

            var player = new Player(setting);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var attacks = fight.RoundResults.Any();
            Assert.IsFalse(attacks);
        }

        [TestMethod]
        public void TestHammerEnrage2()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var hSpell = new HammerSpell();
            player.Spells.Add(hSpell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var attacks = fight.RoundResults.Any();
            Assert.IsTrue(attacks);
            Assert.IsTrue(player.Buff.Enraged);
        }

        private sealed class HammerSpell : Spell
        {
            public HammerSpell()
            {
                PrimaryGimmickGain = 50;
                WeaponType = WeaponType.Hammer;
                SpellType = SpellType.Cast;
                BaseDamage = 1;
            }
        }

        private sealed class HammerSpellRage: Spell
        {
            public HammerSpellRage()
            {
                PrimaryGimmickCost = 50;
                WeaponType = WeaponType.Hammer;
                SpellType = SpellType.Cast;
                BaseDamage = 1;
            }
        }
    }
}
