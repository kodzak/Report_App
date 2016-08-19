using System.Collections.Generic;
using ReportApp.Domain.Entities;
using System;

namespace ReportApp.WebUI.Models
{
    public class SubjectListViewModel
    {
        //public int PrzedmiotID { get; set; }
        //public string Nazwa { get; set; }
        //public PagingInfo PagingInfo { get; set; }
        public List<Subjects> Subjects { get; set; }
        public List<int> SelectedPrzedmiotIDs { get; set; }
    }

}
