using Newtonsoft.Json;
using swlSimulator.api.Combat;
using swlSimulator.api.Spells;
using swlSimulator.api.Weapons;

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
    }
}