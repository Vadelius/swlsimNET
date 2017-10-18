using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;
using swlSimulator.api.Spells.Hammer;
using swlSimulator.api.Weapons;
using swlSimulator.Models;

namespace swlSimulator.Tests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void AplReaderTest()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash, Rage < 50";

            var player = new Player(setting);
            var spell = player.Spells.Find(s => s.GetType() == typeof(Smash));

            var spellArgs = spell.Args == "Rage < 50";

            Assert.IsTrue(spell != null && spellArgs);
        }

        [TestMethod]
        public void AplTest()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash, Rage > 50";
            setting.FightLength = 10;

            var player = new Player(setting);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is Smash)).Count();

            Assert.IsTrue(spells == 0);
        }

        [TestMethod]
        public void WeaponTypes()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash";
            var player = new Player(setting);

            Assert.IsInstanceOfType(player.PrimaryWeapon, typeof(Hammer));
            Assert.IsInstanceOfType(player.SecondaryWeapon, typeof(Fist));
        }

        private Settings TestSettingsHammerFist()
        {
            return new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                TargetType = TargetType.Champion
            };
        }
    }
}
