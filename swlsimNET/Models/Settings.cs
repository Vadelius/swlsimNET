using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using swlsimNET.ServerApp.Weapons;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public String Apl { get; set; }

        public Boolean OpeningShot { get; set; }
        public Boolean Exposed { get; set; }
        public Boolean HeadSignetIsCdr { get; set; }

        public static IEnumerable<SelectListItem> WeaponList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Blade", Value = "Blade"},
                    new SelectListItem { Text = "Hammer", Value = "Hammer"},
                    new SelectListItem { Text = "Fist", Value = "Fist"},
                    new SelectListItem { Text = "Blood", Value = "Blood"},
                    new SelectListItem { Text = "Chaos", Value = "Chaos"},
                    new SelectListItem { Text = "Elemental", Value = "Elemental"},
                    new SelectListItem { Text = "Shotgun", Value = "Shotgun"},
                    new SelectListItem { Text = "Pistols", Value = "Pistols"},
                    new SelectListItem { Text = "Assault Rifle", Value = "Assault Rifle"}
                };
            }
        }
    }
}
