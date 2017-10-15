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
    public class PistolTest
    {
        [TestMethod]
        public void TestPistolInitialGimmick()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Pistol,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 3,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var spell = new PistolSpell();
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is PistolSpell)).Count();

            var pistol = player.Pistol;

            Assert.IsTrue(spells == 3);
            Assert.IsTrue(pistol.LeftChamber == Chamber.White);
            Assert.IsTrue(pistol.RightChamber == Chamber.White);
        }

        [TestMethod]
        public void TestPistolGimmick()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Pistol,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 4,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var spell = new PistolSpell();
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is PistolSpell)).Count();

            var pistol = player.Pistol;

            Assert.IsTrue(spells == 4);
            Assert.IsTrue(pistol.ChamberLockTimeStamp == 4);
        }

        private sealed class PistolSpell : Spell
        {
            public PistolSpell()
            {
                WeaponType = WeaponType.Pistol;
                SpellType = SpellType.Cast;
                CastTime = 1;
                BaseDamage = 1;
                BonusCritChance = -100; // no crits plz
            }
        }
    }
}
