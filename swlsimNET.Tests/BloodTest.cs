using System;
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
    public class BloodTest
    {
        [TestMethod]
        public void TestBloodGimmick()
        {
            var setting = new Settings
            {
                CombatPower = 10,
                PrimaryWeapon = WeaponType.Blood,
                SecondaryWeapon = WeaponType.Fist,
                FightLength = 5,
                TargetType = TargetType.Champion,
                Apl = ""
            };

            var player = new Player(setting);
            var bSpell = new BloodSpell();
            player.Spells.Add(bSpell);

            var engine = new Engine(setting);
            var fight = engine.StartFight(player);

            var bSpells = fight.RoundResults
                .SelectMany(r => r.Attacks.Where(a => a.Spell is BloodSpell)).Count();

            var round1 = fight.RoundResults.First(r => r.TimeSec == 1);
            var round2 = fight.RoundResults.First(r => r.TimeSec == 2);
            var round3 = fight.RoundResults.First(r => r.TimeSec == 3);
            var round4 = fight.RoundResults.First(r => r.TimeSec == 4);
            var round5 = fight.RoundResults.First(r => r.TimeSec == 5);

            Assert.IsTrue(Math.Abs(round1.TotalDamage - 10) < 0.001);   // 0
            Assert.IsTrue(Math.Abs(round2.TotalDamage - 11.56) < 0.01); // 30
            Assert.IsTrue(Math.Abs(round3.TotalDamage - 11.56) < 0.01); // 60
            Assert.IsTrue(Math.Abs(round4.TotalDamage - 13.27) < 0.01); // 90
            Assert.IsTrue(Math.Abs(round5.TotalDamage - 15.34) < 0.01); // 100

            Assert.IsTrue(bSpells == 5);
        }

        // TODO: Add Decay test

        private sealed class BloodSpell : Spell
        {
            public BloodSpell()
            {
                PrimaryGimmickGain = 30;
                WeaponType = WeaponType.Blood;
                SpellType = SpellType.Cast;
                CastTime = 1;
                BaseDamage = 1;
                BonusCritChance = -100; // no crits plz
            }
        }
    }
}
