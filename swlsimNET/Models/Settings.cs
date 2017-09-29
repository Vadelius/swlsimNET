using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using swlsimNET.ServerApp.Weapons;
using Microsoft.AspNetCore.Mvc.Rendering;
using swlsimNET.ServerApp.Combat;

namespace swlsimNET.Models
{
    public class Settings
    {
        [Display(Name = "Primary weapon *")]
        [Required(ErrorMessage = "required.")] // {0} is required. to display name also
        public WeaponType? PrimaryWeapon { get; set; }

        [Display(Name = "Secondary weapon *")]
        [Required(ErrorMessage = "required.")]
        public WeaponType? SecondaryWeapon { get; set; }

        [Display(Name = "Primary weapon affix")]
        public WeaponAffix PrimaryWeaponAffix { get; set; }

        [Display(Name = "Secondary weapon affix")]
        public WeaponAffix SecondaryWeaponAffix { get; set; }

        [Display(Name = "Combat power")]
        public double CombatPower { get; set; } = 1200;

        [Display(Name = "Glance reduction %")]
        public double GlanceReduction { get; set; } = 30;

        [Display(Name = "Critical chance %")]
        public double CriticalChance { get; set; } = 25;

        [Display(Name = "Crit power")]
        public double CritPower { get; set; } = 100;

        [Display(Name = "Basic signet")]
        public double BasicSignet { get; set; } = 75;

        [Display(Name = "Power signet")]
        public double PowerSignet { get; set; } = 17;

        [Display(Name = "Elite signet")]
        public double EliteSignet { get; set; } = 56;

        [Display(Name = "Action priority list *")]
        [Required(ErrorMessage = "required.")]
        public string Apl { get; set; } = "Fist.Savagery, Buff.UnstoppableForce.Active\r\n" +
                                          "Hammer.Seethe, Buff.UnstoppableForce.Active\r\n" +
                                          "Hammer.UnstoppableForce, Rage > 50 || Hammer.Energy > 8\r\n" +
                                          "Hammer.Demolish, Buff.UnstoppableForce.Active\r\n" +
                                          "Hammer.Demolish, Rage > 60 || Hammer.Energy > 13\r\n" +
                                          "Hammer.Smash";

        public bool OpeningShot { get; set; }
        public bool Exposed { get; set; }
        public bool HeadSignetIsCdr { get; set; }
        public bool Savagery { get; set; }

        public int Iterations { get; set; } = 10;
        public int FightLength { get; set; } = 240;

        public TargetType TargetType { get; set; }

        public IEnumerable<SelectListItem> WeaponTypeList => new SelectList(Enum.GetValues(typeof(WeaponType)));
        public IEnumerable<SelectListItem> WeaponAffixesList => new SelectList(Enum.GetValues(typeof(WeaponAffix)));
        public IEnumerable<SelectListItem> TargetTypeList => new SelectList(Enum.GetValues(typeof(TargetType)));

        public IEnumerable<SelectListItem> Passives
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Passive1", Value = "Passive1"},
                    new SelectListItem { Text = "Passive2", Value = "Passive2"},
                    new SelectListItem { Text = "Passive3", Value = "Passive3"},
                    new SelectListItem { Text = "Passive4", Value = "Passive4"},
                    new SelectListItem { Text = "Passive5", Value = "Passive5"},
                };
            }
        }
    }
}
