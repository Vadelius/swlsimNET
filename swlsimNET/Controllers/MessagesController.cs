using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace swlsimNET.Controllers
{
    [Route("api/messages")]
    public class MessagesController : Controller
    {
        [Authorize("read:messages")]
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return the list of messages
            return null;
        }

        [Authorize("create:messages")]
        [HttpPost]
        public IActionResult Create([FromBody] Message message)
        {
            // Create a new message
            return null;
        }
    }
}