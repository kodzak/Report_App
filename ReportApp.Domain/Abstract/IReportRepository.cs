using ReportApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Domain.Abstract
{
    public interface IReportRepository
    {
        IQueryable<Report> Reports { get; }
        void Add(Report wydarzenie);
        void Save();
    }
}
