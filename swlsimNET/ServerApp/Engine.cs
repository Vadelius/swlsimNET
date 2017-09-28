using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp
{
    public class Engine
    {
        private const int Interval = 100;

        public List<FightResult> StartIterations(string aplStr, ObservableCollection<Passive> passives, IProgress<int> progress)
        {
            var iterationResults = new List<FightResult>();

            // Run iterations
            for (var i = 1; i <= 100; i++) //TODO: Fix
            {
                var player = NewPlayer(aplStr, passives);
                var fightResult = StartFight(player);
                fightResult.Iteration = i;            

                iterationResults.Add(fightResult);

                progress?.Report(i);
            }

            return iterationResults;
        }

        public FightResult StartFight(Player player)
        {
            var fightResult = new FightResult();

            // Run a complete fight
            // Check if we can do something 10 times a second (to account for lag)
            for (var ms = 0; ms < 240 * 1000; ms += Interval) //TODO: Fix.
            {
                var roundResult = player.NewRound(ms, Interval);

                // Only save roundes with any actions
                if (roundResult.Attacks.Any())
                {
                    fightResult.RoundResults.Add(roundResult);
                    fightResult.TotalDamage += roundResult.TotalDamage;
                    fightResult.TotalHits += roundResult.TotalHits;
                    fightResult.TotalCrits += roundResult.TotalCrits;
                }
            }

            return fightResult;
        }

        private Player NewPlayer(string aplStr, ObservableCollection<Passive> passives)
        {
            //TODO: FIX
            //var mainWeapon = GetWeaponFromType(S.Default.PrimaryWeapon, S.Default.MainAffix);
            //var offWeapon = GetWeaponFromType(S.Default.SecondaryWeapon, S.Default.OffAffix);
            var mainWeapon = GetWeaponFromType(WeaponType.Hammer, WeaponAffix.Destruction);
            var offWeapon = GetWeaponFromType(WeaponType.Fist, WeaponAffix.Destruction);
            var selectedPassives = GetSelectedPassives(passives);
            var player = new Player(mainWeapon, offWeapon, selectedPassives);

            //var apl = new AplReader(player, aplStr);
            //var aplList = apl.GetApl();

            //player.Spells = aplList;

            return player;
        }

        private Weapon GetWeaponFromType(WeaponType wtype, WeaponAffix waffix)
        {
            switch (wtype)
            {
                case WeaponType.Blade:
                    return new Blade(wtype, waffix);
                case WeaponType.Blood:
                    return new Blood(wtype, waffix);
                case WeaponType.Chaos:
                    return new Chaos(wtype, waffix);
                case WeaponType.Elemental:
                    return new Elemental(wtype, waffix);
                case WeaponType.Fist:
                    return new Fist(wtype, waffix);
                case WeaponType.Hammer:
                    return new Hammer(wtype, waffix);
                case WeaponType.Pistol:
                    return new Pistol(wtype, waffix);
                case WeaponType.AssaultRifle:
                    return new AssaultRifle(wtype, waffix);
                case WeaponType.Shotgun:
                    return new Shotgun(wtype, waffix);
            }

            return null;
        }

        private List<Passive> GetSelectedPassives(ObservableCollection<Passive> passives)
        {
            var selectedPassives = new List<Passive>();

            //TODO: Fix.

            //var passive1 = passives.FirstOrDefault(p => p.Name == S.Default.Passive1?.Split('.').Last());
            //if (passive1 != null) selectedPassives.Add(passive1);

            //var passive2 = passives.FirstOrDefault(p => p.Name == S.Default.Passive2?.Split('.').Last());
            //if (passive2 != null) selectedPassives.Add(passive2);

            //var passive3 = passives.FirstOrDefault(p => p.Name == S.Default.Passive3?.Split('.').Last());
            //if (passive3 != null) selectedPassives.Add(passive3);

            //var passive4 = passives.FirstOrDefault(p => p.Name == S.Default.Passive4?.Split('.').Last());
            //if (passive4 != null) selectedPassives.Add(passive4);

            //var passive5 = passives.FirstOrDefault(p => p.Name == S.Default.Passive5?.Split('.').Last());
            //if (passive5 != null) selectedPassives.Add(passive5);

            return selectedPassives;
        }
    }
}