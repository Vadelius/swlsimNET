using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace swlsimNET.Controllers
{
    [Route("api")]
    public class PingController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("ping/secure")]
        public string PingSecured()
        {
            return "All good. You only get this message if you are authenticated.";
        }
    }
}
