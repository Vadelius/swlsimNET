using Microsoft.AspNetCore.Mvc;
using swlSimulator.api;
using swlSimulator.api.Combat;
using swlSimulator.api.Utilities;
using swlSimulator.Models;
using System;
using System.Collections.Generic;

namespace swlSimulator.Controllers
{

    [Produces("application/json")]
    [Route("api/values")]
    public class ServiceController : Controller
    {
        private List<FightResult> _iterationFightResults;
        private Settings _settings = new Settings();
        private List<string> data;

        public ServiceController(List<string> data)
        {
            this.data = data;
        }

        // GET: api/values
        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }

        [HttpPost()]
        [Produces("application/json"), Consumes("application/json")]
        public IActionResult Post([FromBody] string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                return BadRequest(new { Message = "value is not valid" });
            }

            data.Add(item);
            return Ok(data);
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
