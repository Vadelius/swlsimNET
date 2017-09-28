using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using swlsimNET.Models;

namespace swlsimNET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Import()
        {
            ViewData["Message"] = "Import";

            return View();
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
