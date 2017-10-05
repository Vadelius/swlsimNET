using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlsimNET;
using swlsimNET.Models;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Blood;
using swlsimNET.ServerApp.Weapons;

namespace UnitTests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void SettingsAssumptions()
        {
            var settings = new swlsimNET.Models.Settings
            {
                BasicSignet = 75,
                PowerSignet = 24,
                EliteSignet = 30,
                Iterations = 1,
                FightLength = 60,
                CombatPower = 1500,
                CriticalChance = 30,
                CritPower = 110,
                HeadSignetIsCdr = true,
                OpeningShot = true,
                Exposed = true,
                PrimaryWeapon = WeaponType.Blood,
                SecondaryWeapon = WeaponType.Fist,
                PrimaryWeaponAffix = WeaponAffix.Havoc,
                SecondaryWeaponAffix = WeaponAffix.Havoc

            };

            const bool expected = true;
            Assert.AreEqual(expected, settings.Exposed);

            {

                var selectedPassives = GetSelectedPassives();
                // mainWeapon = Player.GetWeaponFromType(WeaponType.Blood);
                //var offWeapon = Player.GetWeaponFromType(WeaponType.Fist);
                //var player = new Player(mainWeapon, offWeapon, selectedPassives, settings);
                //var apl = new AplReader(player, settings.Apl);
                //var aplList = apl.GetApl();

                //player.Spells = aplList;

                Assert.IsNotNull(settings.PrimaryWeapon);
                Assert.IsNotNull(settings.SecondaryWeapon);
                Assert.AreNotEqual(1499, settings.CombatPower);
            }

            List<Passive> GetSelectedPassives()
            {
                var selectedPassives = new List<Passive>();

                var passive = settings.AllPassives.Find(p => p.Name == settings.Passive1);
                if (passive != null) selectedPassives.Add(passive);

                passive = settings.AllPassives.Find(p => p.Name == settings.Passive2);
                if (passive != null) selectedPassives.Add(passive);

                passive = settings.AllPassives.Find(p => p.Name == settings.Passive3);
                if (passive != null) selectedPassives.Add(passive);

                passive = settings.AllPassives.Find(p => p.Name == settings.Passive4);
                if (passive != null) selectedPassives.Add(passive);

                passive = settings.AllPassives.Find(p => p.Name == settings.Passive5);
                if (passive != null) selectedPassives.Add(passive);

                return selectedPassives;
            }

        }
    }
}
