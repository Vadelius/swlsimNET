using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlsimNET.Models;
using swlsimNET.ServerApp;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Blood;
using swlsimNET.ServerApp.Spells.Hammer;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.Tests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void GlobalBuffs()
        {
            var setting = TestSettingsHammerFist();
            setting.Exposed = true;
            setting.OpeningShot = true;
            setting.Savagery = true;

            var player = new Player(setting);

            var exposed = player.Buffs.Any(b => b.GetType() == typeof(Exposed));
            var openingShot = player.Buffs.Any(b => b.GetType() == typeof(OpeningShot));
            var savagery = player.Buffs.Any(b => b.GetType() == typeof(Savagery));

            Assert.IsInstanceOfType(player.PrimaryWeapon, typeof(Hammer));
            Assert.IsInstanceOfType(player.SecondaryWeapon, typeof(Fist));
            Assert.IsTrue(exposed && openingShot && savagery);
        }

        [TestMethod]
        public void TestIterationGcd()
        {
            var setting = TestSettingsHammerFist();
            setting.Iterations = 2;
            setting.FightLength = 10;
            setting.TargetType = TargetType.Champion;

            var engine = new Engine(setting);
            var iterations = engine.StartIterations();
            var fight1 = iterations.FirstOrDefault();
            var fight2 = iterations.LastOrDefault();

            var iterationCount = iterations.Count;

            var endTimeMs1 = fight1.RoundResults.Last().TimeMs;            
            var rounds1 = fight1.RoundResults.Count;
            var endTimeMs2 = fight2.RoundResults.Last().TimeMs;
            var rounds2 = fight2.RoundResults.Count;

            var spellCount = fight1.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(Smash))).Count();
            spellCount += fight2.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(Smash))).Count();

            Assert.AreEqual(iterationCount, 2);
            Assert.AreEqual(endTimeMs1, 9000);          
            Assert.AreEqual(rounds1, 10);
            Assert.AreEqual(endTimeMs2, 9000);
            Assert.AreEqual(rounds2, 10);
            Assert.IsTrue(spellCount == 20);
        }

        [TestMethod]
        public void TestIterationChannel()
        {
            var setting = TestSettingsBloodFist();
            setting.Iterations = 1;
            setting.FightLength = 5;
            setting.TargetType = TargetType.Champion;

            var engine = new Engine(setting);
            var iterations = engine.StartIterations();
            var fight = iterations.FirstOrDefault();
            var iterationCount = iterations.Count;

            var endTimeMs = fight.RoundResults.Last().TimeMs;
            var rounds = fight.RoundResults.Count;

            var channelTickCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(Maleficium))).Count();

            // TODO: Is this correct assumptions?
            Assert.AreEqual(iterationCount, 1);
            Assert.AreEqual(endTimeMs, 5000);
            Assert.AreEqual(rounds, 5);
            Assert.AreEqual(endTimeMs, 7000);
            Assert.AreEqual(rounds, 10);
            Assert.IsTrue(channelTickCount == 10);
        }

        private Settings TestSettingsHammerFist()
        {
            return new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                Apl = "Hammer.Smash"
            };
        }

        private Settings TestSettingsBloodFist()
        {
            return new Settings
            {
                PrimaryWeapon = WeaponType.Blood,
                SecondaryWeapon = WeaponType.Fist,
                Apl = "Blood.Maleficium"
            };
        }
    }
}
