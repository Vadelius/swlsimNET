using System.Collections.Generic;
using System.Linq;
using swlSimulator.api.Spells.Blade;
using swlSimulator.api.Spells.Blood;
using swlSimulator.api.Spells.Hammer;
using swlSimulator.api.Spells.Pistol;
using swlSimulator.api.Spells.Shotgun;
using swlSimulator.Models;

namespace swlSimulator.api.Spells
{
    public static class Passives
    {
        public static List<Passive> AllPassives = new List<Passive>
        {
            // TODO: Add ALL passives here

            // Blade
            new HardenedBlade(),
            new EyeOfTheStorm(),
            new StormSurge(),
            new Deluge(),

            // Hammer
            new Outrage(),
            new Obliterate(),
            new Berserker(),
            new Annihilate(),
            new UnbridledWrath(),
            new LetLoose(),
            new FastAndFurious(),

            // Pistol
            new FatalShot(),
            new DeadlyDance(),
            new Jackpot(),
            new FixedGame(),
            new HeavyCaliberRounds(),
            new FullyLoaded(),
            new WinStreak(),
            new FlechetteRounds(),
            new BeginnersLuck(),
            new BulletEcho(),
            new Holdout(),
            new LethalAim(),

            // Blood
            new CrimsonPulse(),
            new Desolate(),
            new Contaminate(),
            new Flay(),
            new Defilement(),

            // Shotgun
            new SalvageExpert(),
            new PointBlankShot()
        };

        public static List<Passive> GetSelectedPassives(Settings settings)
        {
            var stringPassives = new List<string>
            {
                settings.Passive1,
                settings.Passive2,
                settings.Passive3,
                settings.Passive4,
                settings.Passive5
            };

            return stringPassives.Select(sPassive => AllPassives.Find(p => p.Name == sPassive))
                .Where(passive => passive != null).ToList();
        }
    }
}