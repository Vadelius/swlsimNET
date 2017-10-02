using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using swlsimNET.Models;

namespace swlsimNET.Controllers
{
        [Route("api/[controller]")]
        public class ReportController : Controller
        {
            private readonly ReportContext _context;

            public ReportController(ReportContext context)
            {
                _context = context;

                if (_context.ChartStuff.Any()) return;
                _context.ChartStuff.Add(new ChartStuff { Name = "Item1" });
                _context.SaveChanges();
            }

           [HttpGet]
            public IEnumerable<Report> GetAll()
            {
            return _context.ChartStuff.ToList();
            }

            [HttpGet("{id}", Name = "GetReport")]
            public IActionResult GetById(string id)
            {
            var item = _context.ChartStuff.FirstOrDefault(t => t.PieStuff == id);
                if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
            }

            [HttpPost]
            public IActionResult Create([FromBody] Report item)
            {
                if (item == null)
                {
                    return BadRequest();
                }

                _context.ChartStuff.Add(item);
                _context.SaveChanges();

                return CreatedAtRoute("GetTodo", new { id = item.PieStuff }, item);
            }
    }

    internal class ChartStuff : Report
    {
        public string Name { get; set; }
        public string Percentage { get; set; }
    }
}
