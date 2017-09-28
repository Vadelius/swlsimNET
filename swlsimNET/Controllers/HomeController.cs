using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swlsimNET.Models;

namespace swlsimNET.Controllers
{
    public class HomeController : Controller
    {
        private Settings _settings = new Settings();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Import()
        {
            return View(_settings);
        }
 
        // POST: Import
        [HttpPost]
        public ActionResult Import(Settings setting)
        {
            try
            {
                return View("Import");
            }
            catch
            {
                return View();
            }
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
    }
}
