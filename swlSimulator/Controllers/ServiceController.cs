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
    [Route("api/Service")]
    public class ServiceController : Controller
    {
        private List<FightResult> _iterationFightResults;

        private Settings _settings = new Settings();

        // GET: api/Service
        [Produces("application/json")]
        [HttpGet]
        public List<FightResult> Get()
        {
            return new List<FightResult>();
        }

        // GET: api/Service/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {

            return "value";
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
