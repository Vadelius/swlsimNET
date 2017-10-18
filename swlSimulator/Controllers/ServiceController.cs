using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.Models;

namespace swlSimulator.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("/api/values")]
    public class ServiceController : Controller
    {
        private List<FightResult> _iterationFightResults;
        private Settings _settings = new Settings();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Settings settings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _settings = settings;

            // Simulation async
            var result = await Task.Run(() => StartSimulation());
            if (!result)
            {
                // Simulation failed
                throw new Exception("Grem has not yet fixed this...");
            }

            var report = new Report();

            result = await Task.Run(() => report.GenerateReportData(_iterationFightResults, settings));
            if (!result)
            {
                // Report generation failed
                throw new Exception("Grem has not yet fixed this...");
            }

            //return View("Results", report);
            return Ok(report);
        }

        private bool StartSimulation()
        {
            var engine = new Engine(_settings);
            _iterationFightResults = engine.StartIterations();

            return _iterationFightResults.Any();
        }
    }
}