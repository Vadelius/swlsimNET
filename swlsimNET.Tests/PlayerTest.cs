using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlsimNET.Models;
using swlsimNET.ServerApp;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Hammer;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.Tests
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
        public void WeaponTypes()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash";
            var player = new Player(setting);

            Assert.IsInstanceOfType(player.PrimaryWeapon, typeof(Hammer));
            Assert.IsInstanceOfType(player.SecondaryWeapon, typeof(Fist));
        }

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
            Assert.AreEqual(endTime1, 9.9);          
            Assert.AreEqual(rounds1, 10);
            Assert.AreEqual(endTime2, 9.9);
            Assert.AreEqual(rounds2, 10);
            Assert.IsTrue(spellCount == 20);
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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(CastSpell))).Count();

            // TODO: Is this correct assumptions? or can we finish a cast and start a new one the same ms
            // 0ms, start cast
            // 2500ms, finish cast
            // 2600ms, start next cast
            // 5100ms, finish cast
            // 5200, start next cast
            // 7700ms, finish cast

            Assert.AreEqual(rounds, 4);
            Assert.AreEqual(endTime, 9.9);
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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(ChannelSpell))).Count();

            // TODO: Is this correct assumptions? or can we finish a channel and start a new one the same ms
            // 0ms, start channel
            // 500ms, channel tick
            // 1000ms, channel tick
            // 1500ms, channel tick
            // 2000ms, channel tick
            // 2500ms, channel tick
            // 2500ms, finish channel
            // 2600ms, start next channel
            // 3100ms, channel tick
            // 3600ms, channel tick
            // 4100ms, channel tick
            // 4600ms, channel tick
            // 5100ms, channel tick
            // 5100ms, finish channel
            // 5200, start next channel
            // 5700ms, channel tick
            // 6200ms, channel tick
            // 6700ms, channel tick
            // 7200ms, channel tick
            // 7700ms, channel tick
            // 7700ms, finish channel

            Assert.AreEqual(rounds, 20);
            Assert.AreEqual(endTime, 9.9);
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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(DotSpell))).Count();

            // TODO: Dot ticks should NOT add any kind of bonus attacks?

            // TODO: Is this correct assumptions? or can we finish a channel and start a new one the same ms
            // 0ms, start dot
            // 500ms, dot tick
            // 1000ms, dot tick
            // 1500ms, dot tick
            // 2000ms, dot tick
            // 2500ms, dot tick
            // 2500ms, finish dot
            // 2600ms, start next dot
            // 3100ms, dot tick
            // 3600ms, dot tick
            // 4100ms, dot tick
            // 4600ms, dot tick
            // 5100ms, dot tick
            // 5100ms, finish dot
            // 5200, start next dot
            // 5700ms, dot tick
            // 6200ms, dot tick
            // 6700ms, dot tick
            // 7200ms, dot tick
            // 7700ms, dot tick
            // 7700ms, finish dot

            Assert.AreEqual(rounds, 20);
            Assert.AreEqual(endTime, 9.9);
            Assert.IsTrue(dotTickCount == 20);
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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(ServerApp.Spells.Fist.Savagery)));
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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(ServerApp.Spells.Fist.Savagery)));
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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(ServerApp.Spells.Fist.Savagery)));
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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(ServerApp.Spells.Fist.Savagery)));
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

        private class CastSpell : Spell
        {
            public CastSpell()
            {
                WeaponType = WeaponType.Elemental;
                AbilityType = AbilityType.None;
                SpellType = SpellType.Cast;
                CastTime = 2.5;
                BaseDamage = 1;
            }
        }

        private class ChannelSpell : Spell
        {
            public ChannelSpell()
            {
                WeaponType = WeaponType.Elemental;
                AbilityType = AbilityType.None;
                SpellType = SpellType.Channel;
                CastTime = 2.5;
                ChannelTicks = 5;
                BaseDamage = 1;
            }
        }

        private class DotSpell : Spell
        {
            public DotSpell()
            {
                WeaponType = WeaponType.Blood;
                AbilityType = AbilityType.None;
                SpellType = SpellType.Dot;
                CastTime = 0;
                DotDuration = 2.5;
                DotTicks = 5;
                BaseDamage = 1;

                // TODO: Add ability debuff
            }
        }
    }
}
