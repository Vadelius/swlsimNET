using Microsoft.AspNetCore.Mvc;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Utilities;
using swlSimulator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace swlSimulator.Controllers
{

    [Produces("application/json")]
    [Route("api/Service")]
    public class ServiceController : Controller
    {
        private Settings _settings = new Settings();
        private List<FightResult> _iterationFightResults;

        // GET: api/Service
        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Service/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [Produces("application/json")]
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
