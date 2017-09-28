using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.Models
{
    public class Settings
    {
        public string PrimaryWeapon { get; set; }
        public string SecondaryWeapon { get; set; }
        public double CombatPower { get; set; }
        public double CriticalChance { get; set; }
        public double CritPower { get; set; }
        public double BasicSignet { get; set; }
        public double PowerSignet { get; set; }
        public double EliteSignet { get; set; }
    }
}
