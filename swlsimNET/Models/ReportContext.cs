using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace swlsimNET.Models
{
public class ReportContext : DbContext
{
    public ReportContext(DbContextOptions<ReportContext> options)
        : base(options)
    {
    }

    public DbSet<Report> ChartStuff { get; set; }

}
}
