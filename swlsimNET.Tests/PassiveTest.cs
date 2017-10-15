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
    public class PassiveTest
    {
        [TestMethod]
        public void HasPassive()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 4,
                TargetType = TargetType.Champion,
                Apl = "",
                Passive1 = "Outrage"
            };

            var player = new Player(setting);

            Assert.IsTrue(player.HasPassive("Outrage"));
            Assert.IsFalse(player.HasPassive("Obliterate"));
        }

        [TestMethod]
        public void PassiveBonusToSpell()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = "",
            };

            var passive = new Passive1();
            var spell = new Spell1();

            var player = new Player(setting);
            player.Passives.Add(passive);
            player.Spells.Add(spell);

            player.NewRound(0, 0);

            Assert.IsTrue(player.HasPassive("Passive1"));
            Assert.IsTrue(player.Spells.First().PrimaryGimmickGain > 0);
        }

        [TestMethod]
        public void PassiveBonusToSpell2()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = "",
            };

            var passive = new Passive3();
            var spell = new Spell1();

            var player = new Player(setting);
            player.Passives.Add(passive);
            player.Spells.Add(spell);

            player.NewRound(0, 0);

            var spell1 = player.Spells.First();

            Assert.IsTrue(spell1.BaseDamage == 2);
            Assert.IsTrue(spell1.BaseDamageCrit == 3);
        }

        [TestMethod]
        public void PassiveBonusToAllWeaponSpells()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = "",
            };

            var passive = new Passive2();
            var spell = new Spell1();
            var spell2 = new Spell2();
            var spell3 = new Spell3();

            var player = new Player(setting);
            player.Passives.Add(passive);
            player.Spells.Add(spell);
            player.Spells.Add(spell2);
            player.Spells.Add(spell3);

            player.NewRound(0, 0);

            Assert.IsTrue(player.HasPassive("Passive2"));
            Assert.IsTrue(player.Spells.Where(s => s.WeaponType == WeaponType.Hammer).All(sp => sp.PrimaryGimmickGain > 0));
            Assert.IsTrue(player.Spells.First(s => s.WeaponType != WeaponType.Hammer).PrimaryGimmickGain == 0);
        }

        [TestMethod]
        public void PassiveBonusSpellHit()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = "",
            };

            var passive = new PassiveBonusSpell();
            var spell = new Spell1();

            var player = new Player(setting);
            player.Passives.Add(passive);
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is Spell1)).Count();
            var bonusSpells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is PassiveBonusSpell)).Count();

            Assert.IsTrue(player.HasPassive("PassiveBonusSpell"));
            Assert.IsTrue(spells > 0);
            Assert.IsTrue(spells == bonusSpells);
        }

        [TestMethod]
        public void PassiveBonusSpellOnlyCrit()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = "",
            };

            var passive = new PassiveBonusSpell2();
            var spell = new Spell2();

            var player = new Player(setting);
            player.Passives.Add(passive);
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is Spell2)).Count();
            var bonusSpells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is PassiveBonusSpell2)).Count();

            Assert.IsTrue(spells > 0);
            Assert.IsTrue(bonusSpells == spells);
        }

        [TestMethod]
        public void PassiveBonusSpellOnlyCrit2()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 1,
                TargetType = TargetType.Champion,
                Apl = "",
            };

            var passive = new PassiveBonusSpell2();
            var spell = new Spell2{ BonusCritChance = -100 };

            var player = new Player(setting);
            player.Passives.Add(passive);
            player.Spells.Add(spell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var spells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is Spell2)).Count();
            var bonusSpells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is PassiveBonusSpell2)).Count();

            Assert.IsTrue(spells > 0);
            Assert.IsTrue(bonusSpells == 0);
        }

        // Passive specials
        //spell.BaseDamage *= (1 + BaseDamageModifier);
        //spell.BaseDamageCrit *= (1 + BaseDamageCritModifier);

        private class Passive1 : Passive
        {
            public Passive1()
            {
                WeaponType = WeaponType.Hammer;
                SpellTypes.Add(typeof(Spell1));
                PrimaryGimmickGain = 10;
            }
        }

        private class Passive2 : Passive
        {
            public Passive2()
            {
                WeaponType = WeaponType.Hammer;
                SpecificWeaponTypeBonus = true;
                PrimaryGimmickGain = 10;
            }
        }

        private class Passive3 : Passive
        {
            public Passive3()
            {
                WeaponType = WeaponType.Hammer;
                SpellTypes.Add(typeof(Spell1));
                BaseDamageModifier = 1;
                BaseDamageCritModifier = 2;
            }
        }

        private class PassiveBonusSpell : Passive
        {
            public PassiveBonusSpell()
            {
                WeaponType = WeaponType.Hammer;
                SpellTypes.Add(typeof(Spell1));
                BaseDamage = 0.81;
                PassiveBonusSpell = this;
                // Critical Hits with Dual Shot deal an additional 0,81CP BaseDamage
            }
        }

        private class PassiveBonusSpell2 : Passive
        {
            public PassiveBonusSpell2()
            {
                WeaponType = WeaponType.Hammer;
                SpellTypes.Add(typeof(Spell2));
                BaseDamage = 0.81;
                BonusSpellOnlyOnCrit = true;
                PassiveBonusSpell = this;
                // Critical Hits with Dual Shot deal an additional 0,81CP BaseDamage
            }
        }

        private sealed class Spell1 : Spell
        {
            public Spell1()
            {
                WeaponType = WeaponType.Hammer;
                CastTime = 1.0m;
                BaseDamage = 1;
                PrimaryGimmickGain = 0;
            }
        }

        private sealed class Spell2 : Spell
        {
            public Spell2()
            {
                WeaponType = WeaponType.Hammer;
                CastTime = 1.0m;
                BaseDamage = 1;
                PrimaryGimmickGain = 0;
                BonusCritChance = 100;
            }
        }

        private sealed class Spell3 : Spell
        {
            public Spell3()
            {
                WeaponType = WeaponType.Fist;
                CastTime = 1.0m;
                BaseDamage = 1;
                PrimaryGimmickGain = 0;
            }
        }
    }
}
