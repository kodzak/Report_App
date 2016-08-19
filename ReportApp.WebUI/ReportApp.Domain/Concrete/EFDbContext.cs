using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ReportApp.Domain.Entities;

namespace ReportApp.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Lab_Group> EventforGroup { get; set; }
    }
}