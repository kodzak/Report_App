using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportApp.Domain.Abstract;
using ReportApp.Domain.Entities;

namespace ReportApp.Domain.Concrete
{

    public class EFReportRepository : IReportRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Report> Reports
        {
            get { return context.Reports; }
        }
        public void Add(Report wydarzenie)
        {
            context.Reports.Add(wydarzenie);

        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}