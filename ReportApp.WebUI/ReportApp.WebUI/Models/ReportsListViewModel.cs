    using System.Collections.Generic;
    using ReportApp.Domain.Entities;

    namespace ReportApp.WebUI.Models
    {
        public class ReportsListViewModel
        {
            public IEnumerable<Report> Reports { get; set; }
            public PagingInfo PagingInfo { get; set; }
            public int? CurrentEvent { get; set; }
        }

    }
