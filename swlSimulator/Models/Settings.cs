using Microsoft.AspNetCore.Mvc.Rendering;
using swlSimulator.api.Combat;
using swlSimulator.api.Spells;
using swlSimulator.api.Spells.Blade;
using swlSimulator.api.Spells.Blood;
using swlSimulator.api.Spells.Hammer;
using swlSimulator.api.Spells.Pistol;
using swlSimulator.api.Spells.Shotgun;
using swlSimulator.api.Weapons;
using System;
using System.Collections.Generic;

namespace swlSimulator.Models
{
    public class Settings
    {
        public WeaponType? PrimaryWeapon { get; set; }
        public WeaponType? SecondaryWeapon { get; set; }
        public WeaponAffix PrimaryWeaponAffix { get; set; }
        public WeaponAffix SecondaryWeaponAffix { get; set; }
        public WeaponProc PrimaryWeaponProc { get; set; }
        public WeaponProc SecondaryWeaponProc { get; set; }
        public NeckTalisman Neck { get; set; }
        public LuckTalisman Luck { get; set; }
        public HeadTalisman Head { get; set; }
        public Gadget Gadget { get; set; }
        public double CombatPower { get; set; } = 1200;
        public double GlanceReduction { get; set; } = 30;
        public double CriticalChance { get; set; } = 25;
        public double CritPower { get; set; } = 100;
        public double BasicSignet { get; set; } = 75;
        public double PowerSignet { get; set; } = 17;
        public double EliteSignet { get; set; } = 56;
        public double WaistSignet { get; set; } = 90;
        public string Apl { get; set; }
        public string Passive1 { get; set; }
        public string Passive2 { get; set; }
        public string Passive3 { get; set; }
        public string Passive4 { get; set; }
        public string Passive5 { get; set; }
        public bool OpeningShot { get; set; }
        public bool Exposed { get; set; }
        public bool HeadSignetIsCdr { get; set; }
        public bool Savagery { get; set; }
        public int Iterations { get; set; } = 100;
        public int FightLength { get; set; } = 240;

        public TargetType TargetType { get; set; }

        public IEnumerable<SelectListItem> WeaponTypeList => new SelectList(Enum.GetValues(typeof(WeaponType)));
        public IEnumerable<SelectListItem> WeaponAffixesList => new SelectList(Enum.GetValues(typeof(WeaponAffix)));
        public IEnumerable<SelectListItem> WeaponProcList => new SelectList(Enum.GetValues(typeof(WeaponProc)));
        public IEnumerable<SelectListItem> TargetTypeList => new SelectList(Enum.GetValues(typeof(TargetType)));
        public IEnumerable<SelectListItem> NeckTalismanList => new SelectList(Enum.GetValues(typeof(NeckTalisman)));
        public IEnumerable<SelectListItem> LuckTalismanList => new SelectList(Enum.GetValues(typeof(LuckTalisman)));
        public IEnumerable<SelectListItem> HeadTalismanList => new SelectList(Enum.GetValues(typeof(HeadTalisman)));
        public IEnumerable<SelectListItem> GadgetList => new SelectList(Enum.GetValues(typeof(Gadget)));

        public IEnumerable<SelectListItem> Passives = _allPassives.ConvertAll(
            a => new SelectListItem
            {
                Text = a.ToString().Substring(a.ToString().LastIndexOf('.') + 1),
                Value = a.ToString().Substring(a.ToString().LastIndexOf('.') + 1),
                Selected = false
            });

        public List<Passive> AllPassives => _allPassives;

        private static List<Passive> _allPassives = new List<Passive>
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
            new PointBlankShot(),
        };
    }
}
