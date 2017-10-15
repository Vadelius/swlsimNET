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
    public class FistTest
    {
        [TestMethod]
        public void TestFistGimmick()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Fist,
                SecondaryWeapon = WeaponType.Pistol,
                FightLength = 5,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var spell = new FistSpell();
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is FistSpell)).Count();

            var fist = player.Fist;

            Assert.IsTrue(spells == 6);
            Assert.IsFalse(fist.AllowFrenziedWrathAbilities);
        }

        [TestMethod]
        public void TestFistGimmickFrenziedWrath()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Fist,
                SecondaryWeapon = WeaponType.Pistol,
                FightLength = 6,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var spell = new FistSpell();
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is FistSpell)).Count();

            var fist = player.Fist;

            Assert.IsTrue(spells == 7);
            Assert.IsTrue(fist.AllowFrenziedWrathAbilities);
        }

        private sealed class FistSpell : Spell
        {
            public FistSpell()
            {
                WeaponType = WeaponType.Fist;
                SpellType = SpellType.Cast;
                CastTime = 0;
                PrimaryGimmickGain = 10;
                BaseDamage = 1;
                BonusCritChance = -100; // no crits plz
            }
        }
    }
}
