using Microsoft.AspNetCore.Mvc;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Utilities;
using swlSimulator.Models;
using System;
using System.Collections.Generic;

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
        public IActionResult Post([FromBody] Settings settings)
        {
            if (settings == null)
            {
                return BadRequest(new { Message = "value is not valid" });
            }
            StartSimulation();
            return Ok(settings);
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
