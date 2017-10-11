using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace swlSimulator.Controllers
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
