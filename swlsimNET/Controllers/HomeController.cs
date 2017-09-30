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
        public async Task<ActionResult> Import(Settings settings, Player player)
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
                var reportData = await Task.Run(() => report.GenerateReportData(_iterationFightResults));

                // TODO: The view should have report data but now it wants a results.cs

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
                //MessageBox.Show(e.ToString());
                //Application.Current.Shutdown();
            }

            return res;
        }
    }
}
