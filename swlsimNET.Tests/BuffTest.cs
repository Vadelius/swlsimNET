using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;
using swlSimulator.api.Spells.Hammer;
using swlSimulator.api.Weapons;
using swlSimulator.Models;

namespace swlsimNET.Tests
{
    [TestClass]
    public class BuffTest
    {
        [TestMethod]
        public void GlobalBuffs()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash";

            setting.Exposed = true;
            setting.OpeningShot = true;
            setting.Savagery = true;

            var player = new Player(setting);

            var exposed = player.Buffs.Any(b => b.GetType() == typeof(Exposed));
            var openingShot = player.Buffs.Any(b => b.GetType() == typeof(OpeningShot));
            var savagery = player.Buffs.Any(b => b.GetType() == typeof(Savagery));

            Assert.IsTrue(exposed && openingShot && savagery);
        }

        [TestMethod]
        public void TestAplBuffActive()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Fist.Savagery, Buff.UnstoppableForce.Active\r\n" +
                      "Hammer.UnstoppableForce, Rage > 100\r\n" +
                      "Hammer.Smash"
            };

            var player = new Player(setting);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var savagery = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(swlSimulator.api.Spells.Fist.Savagery)));
            var unstoppable = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(UnstoppableForce)));

            Assert.IsTrue(!savagery.Any());
            Assert.IsTrue(!unstoppable.Any());
        }

        [TestMethod]
        public void TestAplBuffActive2()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 5,
                TargetType = TargetType.Champion,
                Apl = "Hammer.UnstoppableForce\r\n" +
                      "Fist.Savagery, !Buff.UnstoppableForce.Active\r\n" +                      
                      "Hammer.Smash"
            };

            var player = new Player(setting);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var savagery = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(swlSimulator.api.Spells.Fist.Savagery)));
            var unstoppable = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(UnstoppableForce)));

            Assert.IsTrue(!savagery.Any());
            Assert.IsTrue(unstoppable.Count() == 1);
        }

        [TestMethod]
        public void TestAplBuffDuration()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Fist.Savagery, Buff.UnstoppableForce.Duration > 9\r\n" +
                      "Hammer.UnstoppableForce\r\n" +
                      "Hammer.Smash"
            };

            var player = new Player(setting);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var savagery = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(swlSimulator.api.Spells.Fist.Savagery)));
            var unstoppable = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(UnstoppableForce)));

            Assert.IsTrue(!savagery.Any());
            Assert.IsTrue(unstoppable.Count() == 1);
        }

        [TestMethod]
        public void TestAplBuffDuration2()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Fist.Savagery, Buff.UnstoppableForce.Duration > 4\r\n" +
                      "Hammer.UnstoppableForce\r\n" +
                      "Hammer.Smash"
            };

            var player = new Player(setting);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var savagery = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(swlSimulator.api.Spells.Fist.Savagery)));
            var unstoppable = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(UnstoppableForce)));

            Assert.IsTrue(savagery.Count() == 1);
            Assert.IsTrue(unstoppable.Count() == 1);
        }

        private Settings TestSettingsHammerFist()
        {
            return new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
            };
        }
    }
}
