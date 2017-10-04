using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swlsimNET.Models;
using swlsimNET.ServerApp;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Utilities;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.Controllers
{
    public class HomeController : Controller
    {
        private Settings _settings = new Settings();
        private List<FightResult> _iterationFightResults;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Import()
        {
            return View(_settings);
        }

        public IActionResult Spellquery()
        {
            ViewData["Message"] = "Spellquery";

            return View();
        }

        // POST: Import
        [HttpPost]
        public async Task<ActionResult> Import(Settings settings)
        {
            if (ModelState.IsValid)
            {
                _settings = settings;

                // Simulation Async
                var result = await Task.Run(() => StartSimulation());
                if (!result)
                {
                    // Simulation failed
                    return View(settings);
                }                

                var report = new Report();

                result = await Task.Run(() => report.GenerateReportData(_iterationFightResults, settings));
                if (!result)
                {
                    // Report generation failed
                    return View(settings);
                }

                return View("Results", report);
            }

            // If we got this far, something failed. So, redisplay form
            return View(settings);
        }

        public IActionResult Export()
        {
            ViewData["Message"] = "Export";

            return View();
        }

        public IActionResult Results()
        {
            ViewData["Message"] = "Results";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult SetPresetHammerFist(Settings s)
        {
            ModelState.Clear();
            s.PrimaryWeapon = WeaponType.Hammer;
            s.SecondaryWeapon = WeaponType.Fist;
            s.Apl = "Fist.Savagery, Buff.UnstoppableForce.Active\r\n" +
                    "Hammer.Seethe, Buff.UnstoppableForce.Active\r\n" +
                    "Hammer.UnstoppableForce, Rage > 50 || Hammer.Energy > 8\r\n" +
                    "Hammer.Demolish, Buff.UnstoppableForce.Active\r\n" +
                    "Hammer.Demolish, Rage > 60 || Hammer.Energy > 13\r\n" +
                    "Hammer.Smash";

            s.Passive1 = "Outrage";
            s.Passive2 = "Obliterate";
            s.Passive3 = "Berserker";
            s.Passive4 = "FastAndFurious";
            s.Passive5 = "UnbridledWrath";

            return View("Import", s);
        }

        [HttpPost]
        public ActionResult SetPresetBloodFist(Settings s)
        {
            ModelState.Clear();
            s.PrimaryWeapon = WeaponType.Blood;
            s.SecondaryWeapon = WeaponType.Fist;
            s.Apl = "Fist.Savagery, Corruption >= 10 && EldritchScourge.Cooldown <= 0 && Desecrate.Cooldown <= 0\r\n" +
                    "Blood.EldritchScourge, Corruption > 0 && Buff.Savagery.Active\r\n" +
                    "Blood.Desecrate, Buff.Savagery.Active\r\n" +
                    "Blood.Maleficium, Blood.Energy > 10\r\n" +
                    "Blood.Torment";

            s.Passive1 = "CrimsonPulse";
            s.Passive2 = "Desolate";
            s.Passive3 = "Defilement";
            s.Passive4 = "Flay";
            s.Passive5 = "Contaminate"; // not used

            return View("Import", s);
        }

        [HttpPost]
        public ActionResult SetPresetHammerPistol(Settings s)
        {
            ModelState.Clear();
            s.PrimaryWeapon = WeaponType.Blood;
            s.SecondaryWeapon = WeaponType.Fist;
            s.Apl = "Hammer.Seethe, Rage < 38\r\n" +
                    "Hammer.Demolish, Enraged\r\n" +
                    "Hammer.Demolish\r\n" +
                    "Pistol.DualShot\r\n" +
                    "Hammer.Smash";

            s.Passive1 = "Outrage";
            s.Passive2 = "Obliterate";
            s.Passive3 = "Berserker";
            s.Passive4 = "FastAndFurious";
            s.Passive5 = "UnbridledWrath";

            return View("Import", s);
        }

        public ActionResult SetPresetHammerShotgun(Settings s)
        {
            ModelState.Clear();
            s.PrimaryWeapon = WeaponType.Blood;
            s.SecondaryWeapon = WeaponType.Fist;
            s.Apl = "Shotgun.ShellSalvage, Hammer.Energy<10 && Shotgun.Energy< 10\r\n" +
                    "Hammer.Seethe, Hammer.Energy<11\r\n" +
                    "Hammer.UnstoppableForce\r\n" +
                    "Hammer.Demolish\r\n" +
                    "Shotgun.RagingShot\r\n" +
                    "Shotgun.Reload, Shells == 0\r\n" +
                    "Hammer.Smash";

            s.Passive1 = "Outrage";
            s.Passive2 = "Obliterate";
            s.Passive3 = "UnbridledWrath";
            s.Passive4 = "FastAndFurious";
            s.Passive5 = "SalvageExpert";

            return View("Import", s);
        }

        private bool StartSimulation()
        {
            var res = false;

            try
            {
                var engine = new Engine(_settings);
                _iterationFightResults = engine.StartIterations();

                res = true;
            }
            catch (Exception e) when (!Helper.Env.Debugging)
            {
                // TODO: Log exception and show to user
                var exception = e;
                //MessageBox.Show(e.ToString());
                //Application.Current.Shutdown();
            }

            return res;
        }
    }
}
