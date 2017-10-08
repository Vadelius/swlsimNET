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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(CastSpell))).Count();

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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(ChannelSpell))).Count();

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
                .SelectMany(r => r.Attacks.Where(a => a.Spell.GetType() == typeof(DotSpell))).Count();

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
                CastTime = 2.5m;
                BaseDamage = 1;
            }
        }

        private class ChannelSpell : Spell
        {
            public ChannelSpell()
            {
                WeaponType = WeaponType.Blood;
                AbilityType = AbilityType.None;
                SpellType = SpellType.Channel;
                CastTime = 2.5m;
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
                DotDuration = 2.5m;
                DotTicks = 5;
                BaseDamage = 1;

                // TODO: Add ability debuff
            }
        }
    }
}
