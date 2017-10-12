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
    public class SpellTest
    {
        [TestMethod]
        public void TestIterationGcd()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash";
            setting.Iterations = 2;
            setting.FightLength = 10;
            setting.TargetType = TargetType.Champion;

            var engine = new Engine(setting);
            var iterations = engine.StartIterations();
            var fight1 = iterations.FirstOrDefault();
            var fight2 = iterations.LastOrDefault();

            var iterationCount = iterations.Count;

            var endTime1 = fight1.RoundResults.Last().TimeSec;            
            var rounds1 = fight1.RoundResults.Count;
            var endTime2 = fight2.RoundResults.Last().TimeSec;
            var rounds2 = fight2.RoundResults.Count;

            var spellCount = fight1.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(Smash))).Count();
            spellCount += fight2.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(Smash))).Count();

            Assert.AreEqual(iterationCount, 2);
            Assert.AreEqual(endTime1, 10.0m);          
            Assert.AreEqual(rounds1, 11);
            Assert.AreEqual(endTime2, 10.0m);
            Assert.AreEqual(rounds2, 11);
            Assert.IsTrue(spellCount == 22);
        }

        [TestMethod]
        public void TestCastSpell()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Elemental,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Elemental.TestCastSpell"
            };

            var spell = new CastSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var castCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is CastSpell)).Count();

            // 0.0, start cast
            // 2.5, finish cast
            // 2.5, start next cast
            // 5.0, finish cast
            // 5.0, start next cast
            // 7.5, finish cast
            // 7.5, start next cast
            // 10.0, finish cast

            Assert.AreEqual(rounds, 4);
            Assert.AreEqual(endTime, 10.0m);
            Assert.IsTrue(castCount == 4);
        }

        [TestMethod]
        public void TestChannelSpell()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Blood,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Blood.ChannelSpell"
            };          

            var spell = new ChannelSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var channelTickCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is ChannelSpell)).Count();

            // 0.0, start channel
            // 0.5, channel tick
            // 1.0, channel tick
            // 1.5, channel tick
            // 2.0, channel tick
            // 2.5, channel tick
            // 2.5, finish channel
            // 2.5, start next channel
            // 3.0, channel tick
            // 3.5, channel tick
            // 4.0, channel tick
            // 4.5, channel tick
            // 5.0, channel tick
            // 5.0, finish channel
            // 5.0, start next channel
            // 5.5, channel tick
            // 6.0, channel tick
            // 6.5, channel tick
            // 7.0, channel tick
            // 7.5, channel tick
            // 7.5, finish channel
            // 7.5, start next channel
            // 8.0, channel tick
            // 8.5, channel tick
            // 9.0, channel tick
            // 9.5, channel tick
            // 10.0, channel tick
            // 10.0, finish channel

            Assert.AreEqual(rounds, 20);
            Assert.AreEqual(endTime, 10.0m);
            Assert.IsTrue(channelTickCount == 20);
        }

        [TestMethod]
        public void TestDotSpell()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Blood,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Blood.DotSpell"
            };

            var spell = new DotSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var dotTickCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is DotSpell)).Count();

            // TODO: Dot ticks should NOT add any kind of bonus attacks?

            // 0.0, start dot
            // 0.5, dot tick
            // 1.0, dot tick
            // 1.5, dot tick
            // 2.0, dot tick
            // 2.5, dot tick
            // 2.5, finish dot
            // 2.5, start next dot
            // 3.0, dot tick
            // 3.5, dot tick
            // 4.0, dot tick
            // 4.5, dot tick
            // 5.0, dot tick
            // 5.0, finish dot
            // 5.0, start next dot
            // 5.5, dot tick
            // 6.0, dot tick
            // 6.5, dot tick
            // 7.0, dot tick
            // 7.5, dot tick
            // 7.5, finish dot
            // 7.5, start next dot
            // 8.0, dot tick
            // 8.5, dot tick
            // 9.0, dot tick
            // 9.5, dot tick
            // 10.0, dot tick
            // 10.0, finish dot

            Assert.AreEqual(rounds, 20);
            Assert.AreEqual(endTime, 10.0m);
            Assert.IsTrue(dotTickCount == 20);
        }

        [TestMethod]
        public void TestGadgetSpell()
        {
            var setting = new Settings
            {
                PrimaryWeapon = WeaponType.Elemental,
                SecondaryWeapon = WeaponType.Fist,
                Gadget = Gadget.Test,
                FightLength = 10,
                TargetType = TargetType.Champion,
                Apl = "Elemental.TestCastSpell"
            };

            var spell = new CastSpell();
            var gadgetSpell = new GadgetSpell();
            var player = new Player(setting);
            player.Spells.Add(spell);
            player.Item.Spells.Add(gadgetSpell);
            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var endTime = fight.RoundResults.Last().TimeSec;
            var rounds = fight.RoundResults.Count;

            var gadgetCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is GadgetSpell)).Count();
            var castCount = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is CastSpell)).Count();

            // 0.0, gadget 
            // 0.0, start cast
            // 2.5, finish cast
            // 2.5, gadget
            // 2.5, start next cast
            // 5.0, finish cast
            // 5.0, gadget
            // 5.0, start next cast
            // 7.5, finish cast
            // 7.5, gadget
            // 7.5, start next cast
            // 10.0, finish cast
            // 10.0, gadget

            Assert.AreEqual(rounds, 5);
            Assert.AreEqual(endTime, 10.0m);
            Assert.IsTrue(gadgetCount == 5);
            Assert.IsTrue(castCount == 4);
        }

        private Settings TestSettingsHammerFist()
        {
            return new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
            };
        }

        private sealed class CastSpell : Spell
        {
            public CastSpell()
            {
                WeaponType = WeaponType.Elemental;
                SpellType = SpellType.Cast;
                CastTime = 2.5m;
                BaseDamage = 1;
            }
        }

        private sealed class ChannelSpell : Spell
        {
            public ChannelSpell()
            {
                WeaponType = WeaponType.Blood;
                SpellType = SpellType.Channel;
                CastTime = 2.5m;
                ChannelTicks = 5;
                BaseDamage = 1;
            }
        }

        private sealed class DotSpell : Spell
        {
            public DotSpell()
            {
                WeaponType = WeaponType.Blood;
                SpellType = SpellType.Dot;
                CastTime = 0;
                DotDuration = 2.5m;
                DotTicks = 5;
                BaseDamage = 1;

                // TODO: Add ability debuff
            }
        }

        private sealed class GadgetSpell : Spell
        {
            public GadgetSpell()
            {
                WeaponType = WeaponType.None;
                AbilityType = AbilityType.Gadget;
                SpellType = SpellType.Instant;
                MaxCooldown = 2.5m;
            }
        }
    }
}
