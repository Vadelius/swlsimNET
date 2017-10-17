using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
        [JsonProperty("gadget")]
        public Gadget Gadget { get; set; }

        [JsonProperty("passive2")]
        public string Passive2 { get; set; }

        [JsonProperty("criticalChance")]
        public double CriticalChance { get; set; } = 25;

        [JsonProperty("basicSignet")]
        public double BasicSignet { get; set; }

        [JsonProperty("apl")]
        public string Apl { get; set; }

        [JsonProperty("combatPower")]
        public double CombatPower { get; set; } = 1200;

        [JsonProperty("eliteSignet")]
        public double EliteSignet { get; set; }

        [JsonProperty("criticalPower")]
        public double CritPower { get; set; } = 100;

        [JsonProperty("exposed")]
        public bool Exposed { get; set; }

        [JsonProperty("luckSignet")]
        public double LuckSignet { get; set; }

        [JsonProperty("headCdr")]
        public bool HeadSignetIsCdr { get; set; }

        [JsonProperty("head")]
        public HeadTalisman Head { get; set; }

        [JsonProperty("luck")]
        public LuckTalisman Luck { get; set; }

        [JsonProperty("openingShot")]
        public bool OpeningShot { get; set; }

        [JsonProperty("neck")]
        public NeckTalisman Neck { get; set; }

        [JsonProperty("passive1")]
        public string Passive1 { get; set; }

        [JsonProperty("powerSignet")]
        public double PowerSignet { get; set; }

        [JsonProperty("secondaryAffix")]
        public WeaponAffix SecondaryWeaponAffix { get; set; }

        [JsonProperty("passive4")]
        public string Passive4 { get; set; }

        [JsonProperty("passive3")]
        public string Passive3 { get; set; }

        [JsonProperty("passive5")]
        public string Passive5 { get; set; }

        [JsonProperty("primaryProc")]
        public WeaponProc PrimaryWeaponProc { get; set; }

        [JsonProperty("primaryAffix")]
        public WeaponAffix PrimaryWeaponAffix { get; set; }

        [JsonProperty("primaryWeapon")]
        public WeaponType PrimaryWeapon { get; set; }

        [JsonProperty("secondaryWeapon")]
        public WeaponType SecondaryWeapon { get; set; }

        [JsonProperty("secondaryProc")]
        public WeaponProc SecondaryWeaponProc { get; set; }

        [JsonProperty("waistCdr")]
        public bool WaistCdr { get; set; }

        [JsonProperty("wristSignet")]
        public object WristSignet { get; set; }


        // Unused atm.
        public double GlanceReduction { get; set; } = 30;
        public double Waist { get; set; }
        public double WaistSignet { get; set; }






        public bool Savagery { get; set; } = false;
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

        [JsonIgnore]
        public List<Passive> AllPassives => _allPassives;

        [JsonIgnore]
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
